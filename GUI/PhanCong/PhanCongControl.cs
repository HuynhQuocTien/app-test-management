using DAL;
using DocumentFormat.OpenXml.Spreadsheet;
using GUI.MonHoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GUI.PhanCong
{
    public partial class PhanCongControl : UserControl
    {
        public PhanCongControl()
        {
            InitializeComponent();
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    string query = "SELECT PC.MaPhanCong as 'Mã Phân Công', MH.TenMonHoc as 'Tên Môn Học', ND.Ten as 'Tên Giáo Viên' FROM PhanCong PC INNER JOIN MonHoc MH ON MH.MaMonHoc = PC.MaMonHoc INNER JOIN NguoiDung ND ON  ND.MaNguoiDung=PC.MaGV;";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    da.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;
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
            fthemPhanCong.ShowDialog();
        }
    }
}
