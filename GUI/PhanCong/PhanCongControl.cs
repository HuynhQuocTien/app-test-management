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


namespace GUI.PhanCong
{
    public partial class PhanCongControl : UserControl
    {
        public PhanCongControl()
        {
            InitializeComponent();
            LoadDataToGridView();
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
    }
}
