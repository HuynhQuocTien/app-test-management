using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Users
{
    public partial class InfoUser : Form
    {
        public InfoUser()
        {
            InitializeComponent();
            this.Load += new EventHandler(InfoUser_Load);
        }

        private void InfoUser_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            // Sử dụng lớp từ DAL để lấy thông tin người dùng từ database
            using (SqlConnection conn = GetConnectionDb.GetConnectionString())
            {
                try
                {
                    string query = "SELECT * FROM Users WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Giả sử bạn có cách để lấy userId từ một nguồn nào đó
                    string userId = "1";  // Thay thế bằng UserID thực tế
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị thông tin người dùng lên form nếu có dữ liệu
                    if (dt.Rows.Count > 0)
                    {
                        label1.Text = dt.Rows[0]["Name"].ToString();
                        textBoxEmail.Text = dt.Rows[0]["Email"].ToString();
                        // Và các trường khác tương tự
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin người dùng.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }
        private void buttonUpImg_Click_1(object sender, EventArgs e)
        {

        }

    }
}
