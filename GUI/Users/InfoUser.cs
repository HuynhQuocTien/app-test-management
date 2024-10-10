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
            SqlConnection conn = GetConnectionDb.GetConnectionString();
            string query = "SELECT * FROM NhomQuyen";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            // Xóa các mục hiện có trong ComboBox (nếu cần)
            comboBox1.Items.Clear();

            // Thêm các mục mới từ SQL Server
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["TenQuyen"].ToString());
            }
            reader.Close();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            // Sử dụng lớp từ DAL để lấy thông tin người dùng từ database
            using (SqlConnection conn = GetConnectionDb.GetConnectionString())
            {
                try
                {
                    string query = "SELECT * FROM TaiKhoan INNER JOIN NguoiDung ON TaiKhoan.Username = NguoiDung.MaNguoiDung INNER JOIN NhomQuyen ON NhomQuyen.MaNhomQuyen = TaiKhoan.MaNhomQuyen  WHERE TaiKhoan.Username=@UserID;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    string userId = "3121410111";  //UserID tạm thời
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị thông tin người dùng lên form nếu có dữ liệu
                    if (dt.Rows.Count > 0)
                    {
                        textBoxID.Text = dt.Rows[0]["MaNguoiDung"].ToString();
                        textBoxEmail.Text = dt.Rows[0]["Email"].ToString();
                        textBoxName.Text = dt.Rows[0]["Ten"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                        if (dt.Rows[0]["GioiTinh"].ToString() == "1")
                        {
                            rbNam.Checked = true;
                            RbNu.Checked = false;
                        }
                        else
                        {
                            rbNam.Checked = false;
                            RbNu.Checked = true;
                        }
                        txtPass.Text= dt.Rows[0]["Password"].ToString();
                        textBox2.Text= dt.Rows[0]["SDT"].ToString();
                        string tenQuyen = dt.Rows[0]["TenQuyen"].ToString();
                        int index = comboBox1.FindStringExact(tenQuyen);

                        if (index != -1)
                        {
                            // Nếu tìm thấy, chọn item đó trong ComboBox
                            comboBox1.SelectedIndex = index;
                        }
                        else
                        {
                            comboBox1.SelectedIndex = -1;
                        }
                        if (dt.Rows[0]["TrangThai"].ToString() == "1")
                        {
                            radioButtonStatus.Checked = true;
                        } else
                        {
                            radioButtonStatus.Checked = false;
                        }
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
