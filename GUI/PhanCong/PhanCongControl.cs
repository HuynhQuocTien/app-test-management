using DAL;
using GUI.MonHoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BLL;
using DocumentFormat.OpenXml.Office.Word;


namespace GUI.PhanCong
{
    public partial class PhanCongControl : UserControl
    {
        public PhanCongControl()
        {
            InitializeComponent();
            phanTrang();
            LoadDataToGridView();
        }
        public void phanTrang()
        {
            // Đặt giới hạn số trang cho NumericUpDown
            int totalRecords = countPC();  // Tổng số bản ghi

            int recordsPerPage = 10; // Số bản ghi trên mỗi trang
            int totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = totalPages;
            this.label2.Text = "Trên tổng " + totalPages + " trang";
            // Sự kiện khi thay đổi trang
            this.numericUpDown1.ValueChanged += (sender, e) =>
            {
                int selectedPage = (int)numericUpDown1.Value;
                LoadPage(selectedPage, recordsPerPage);
            };
        }

        private void LoadPage(int pageNumber, int recordsPerPage)
        {
            int startRecord = (pageNumber - 1) * recordsPerPage;

            // Tải dữ liệu từ cơ sở dữ liệu hoặc danh sách, lấy các bản ghi từ startRecord đến startRecord + recordsPerPage
            // Ví dụ:
            DataTable pageData = GetDataForPage(startRecord, recordsPerPage);

            dataGridView1.DataSource = pageData;
        }

        private DataTable GetDataForPage(int startRecord, int recordsPerPage)
        {
            DataTable dt = new DataTable();

            // Chuỗi kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                // Truy vấn với OFFSET và FETCH NEXT để lấy dữ liệu theo trang
                string query = @"
            SELECT ND.Ten as 'Tên Giáo viên', MH.TenMonHoc as 'Môn Học Phân Công' 
            FROM PhanCong PC 
            INNER JOIN NguoiDung ND ON ND.MaNguoiDung = PC.MaGV 
            INNER JOIN MonHoC MH ON MH.MaMonHoc = PC.MaMonHoc 
            ORDER BY ND.Ten 
            OFFSET @StartRecord ROWS 
            FETCH NEXT @RecordsPerPage ROWS ONLY";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm các tham số cho OFFSET và FETCH NEXT
                    cmd.Parameters.AddWithValue("@StartRecord", startRecord);
                    cmd.Parameters.AddWithValue("@RecordsPerPage", recordsPerPage);

                    // Khởi tạo SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public int countPC()
        {
            // Kết nối đến cơ sở dữ liệu SQL Server
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    // Truy vấn dữ liệu từ SQL Server
                    string query = "SELECT COUNT(*) as 'all' FROM PhanCong PC INNER JOIN NguoiDung ND ON ND.MaNguoiDung=PC.MaGV  INNER JOIN MonHoC MH ON MH.MaMonHoc=PC.MaMonHoc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                    return 0;
                }
            }
        }

        private void LoadDataToGridView()
        {
            // Kết nối đến cơ sở dữ liệu SQL Server
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    // Truy vấn dữ liệu từ SQL Server
                    string query = "SELECT ND.Ten as 'Tên Giáo viên', MH.TenMonHoc as 'Môn Học Phân Công' FROM PhanCong PC INNER JOIN NguoiDung ND ON ND.MaNguoiDung=PC.MaGV INNER JOIN MonHoC MH ON MH.MaMonHoc=PC.MaMonHoc"; 
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    da.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;

                    // Đóng kết nối
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fAddPhanCong fthemPhanCong = new fAddPhanCong();
            fthemPhanCong.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    // Truy vấn dữ liệu từ SQL Server
                    string query = "SELECT ND.Ten as 'Tên Giáo viên', MH.TenMonHoc as 'Môn Học Phân Công' FROM PhanCong PC INNER JOIN NguoiDung ND ON ND.MaNguoiDung=PC.MaGV INNER JOIN MonHoC MH ON MH.MaMonHoc=PC.MaMonHoc WHERE ND.Ten LIKE @Ten OR MH.TenMonHoc LIKE @TenMonHoc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ten", "%" + textBoxTimKiem.Text + "%");
                    cmd.Parameters.AddWithValue("@TenMonHoc", "%" + textBoxTimKiem.Text + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    da.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;

                    // Đóng kết nối
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            }

        }
    }
}
