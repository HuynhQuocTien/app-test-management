using DAL;
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
    public partial class fAddPhanCong : Form
    {
        public fAddPhanCong()
        {
            InitializeComponent();

            //Data giáo viên
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    string query = "SELECT ND.Ten as 'TenNguoiDung', ND.MaNguoiDung as 'MaNguoiDung' FROM NguoiDung ND INNER JOIN TaiKhoan TK ON ND.MaNguoiDung = TK.Username INNER JOIN NhomQuyen NQ ON NQ.MaNhomQuyen = TK.MaNhomQuyen WHERE NQ.MaNhomQuyen = 2;";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    da.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    cbMonHoc.DataSource = dt;
                    cbMonHoc.DisplayMember = "TenNguoiDung";  // Tên cột hiển thị
                    cbMonHoc.ValueMember = "MaNguoiDung";     // Giá trị ẩn (ID)

                    cbMonHoc.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            }
            //Data LB
        }
    }
}
