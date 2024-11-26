using DAL;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Data.SqlClient;
using BLL;
using System.Text.RegularExpressions;


namespace GUI.Users
{
    public partial class UsersDetail : Form
    {
        private long userID;
        private string currentImagePath;
        private string avatarFolderPath;
        private UsersControl usersControl;

        public UsersDetail(NguoiDungDTO nguoiDungDTO, UsersControl usersControl)
        {
            InitializeComponent();
            this.userID = nguoiDungDTO.MaNguoiDung;
            this.usersControl = usersControl;

            // Lấy đường dẫn của thư mục chứa file thực thi của ứng dụng
            string exePath = Application.StartupPath;

            for (int i = 0; i < 5; i++)
            {
                DirectoryInfo parent = Directory.GetParent(exePath);

                if (parent == null)
                {
                    throw new DirectoryNotFoundException($"Không thể đi ngược 5 cấp thư mục từ {Application.StartupPath}");
                }
                exePath = parent.FullName;
            }
            //Tạo đường dẫn đến thư mục Avatar
            avatarFolderPath = Path.Combine(exePath, "GUI", "Users", "Avatar");

            //Kiểm tra xem thư mục có tồn tại không, nếu không thì tạo mới
            if (!Directory.Exists(avatarFolderPath))
            {
                Directory.CreateDirectory(avatarFolderPath);
            }


            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {

                    string query = "SELECT * FROM TaiKhoan INNER JOIN NguoiDung ON TaiKhoan.Username = NguoiDung.MaNguoiDung INNER JOIN NhomQuyen ON NhomQuyen.MaNhomQuyen = TaiKhoan.MaNhomQuyen  WHERE TaiKhoan.Username=@UserID;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị thông tin người dùng lên form nếu có dữ liệu
                    if (dt.Rows.Count > 0)
                    {
                        // Lấy tên file ảnh từ cơ sở dữ liệu
                        string tenAnh = dt.Rows[0]["Avatar"].ToString();

                        // Tạo đường dẫn đầy đủ tới file ảnh
                        string imagePath = Path.Combine(avatarFolderPath, tenAnh);


                        // Kiểm tra xem file ảnh có tồn tại hay không
                        if (File.Exists(imagePath))
                        {
                            // Gán ảnh vào PictureBox nếu file tồn tại
                            pictureBox1.Image = Image.FromFile(imagePath);

                        }
                        else
                        {
                            // Nếu không tìm thấy ảnh, bạn có thể hiển thị ảnh mặc định
                            pictureBox1.Image = null; // Hoặc gán ảnh mặc định
                        }
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
                        txtPass.Text = dt.Rows[0]["Password"].ToString();
                        textBox2.Text = dt.Rows[0]["SDT"].ToString();
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
                        comboBox1.Enabled = false;
                        if (dt.Rows[0]["TrangThai"].ToString() == "1")
                        {
                            radioButtonStatus.Checked = true;
                        }
                        else
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



        public bool checkHoTenValid()
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBoxName.Text.Length < 2)
            {
                MessageBox.Show("Độ dài tối thiểu của tên là 2", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBoxName.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Họ tên chỉ chứa kí tự chữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool checkEmailValid()
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                MessageBox.Show("Email không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(textBoxEmail.Text, pattern))
            {
                MessageBox.Show("Sai định dạng email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

 
        public bool checkSdtValid()
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2.Text.Any(char.IsLetter))
            {
                MessageBox.Show("SDT chỉ chứa kí tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2.Text.Length < 10 || textBox2.Text.Length > 11)
            {
                MessageBox.Show("Độ dài của số điện thoại là 10 hoặc 11 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

   

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {


                if (checkHoTenValid() && checkSdtValid() && checkEmailValid()) { 
                
          

                try
                {
                    string queryEmail = "UPDATE TaiKhoan SET Email=@Email WHERE Username=@UserID;";
                    using (SqlCommand cmdEmail = new SqlCommand(queryEmail, conn))
                    {
                        cmdEmail.Parameters.AddWithValue("@Email", textBoxEmail.Text);
                        cmdEmail.Parameters.AddWithValue("@UserID", userID);
                        cmdEmail.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrEmpty(currentImagePath))
                    {

                        string newFileName = $"Avatar_{userID}{Path.GetExtension(currentImagePath)}";
                        string newAvatarPath = Path.Combine(avatarFolderPath, newFileName);
                        //MessageBox.Show("Image Path: " + newAvatarPath);

                        string fileName = Path.GetFileName(currentImagePath);
                        File.Copy(currentImagePath, newAvatarPath, true);

                        // Update Ten and SDT in NguoiDung table
                        string queryUser = "UPDATE NguoiDung SET Ten=@Ten, SDT=@SDT, Avatar=@Avatar WHERE MaNguoiDung=@UserID;";
                        using (SqlCommand cmdUser = new SqlCommand(queryUser, conn))
                        {
                            cmdUser.Parameters.AddWithValue("@Ten", textBoxName.Text);
                            cmdUser.Parameters.AddWithValue("@SDT", textBox2.Text);
                            cmdUser.Parameters.AddWithValue("@Avatar", newFileName);

                            cmdUser.Parameters.AddWithValue("@UserID", userID);
                            cmdUser.ExecuteNonQuery();
                        }
                    }
                    else

                    {
                        // If no new image is selected, update the other fields without copying an image
                        string queryUser = "UPDATE NguoiDung SET Ten=@Ten, SDT=@SDT WHERE MaNguoiDung=@UserID;";
                        using (SqlCommand cmdUser = new SqlCommand(queryUser, conn))
                        {
                            cmdUser.Parameters.AddWithValue("@Ten", textBoxName.Text);
                            cmdUser.Parameters.AddWithValue("@SDT", textBox2.Text);
                            cmdUser.Parameters.AddWithValue("@UserID", userID);
                            cmdUser.ExecuteNonQuery();
                        }
                    }

                    usersControl.renderAfterEdit();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                    this.Close();
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Sua
            buttonUpImg.Visible = true;
            textBoxName.Enabled = true;
            textBoxEmail.Enabled = true;
            textBox2.Enabled = true;
        }

        private void buttonUpImg_Click(object sender, EventArgs e)
        {
            // Tạo hộp thoại mở file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Tùy chọn bộ lọc file (chỉ cho phép các định dạng ảnh)
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

            // Hiển thị hộp thoại và kiểm tra xem người dùng có chọn file hay không
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của file đã chọn
                currentImagePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(currentImagePath);
                pictureBox1.Image = Image.FromFile(currentImagePath);
            }
        }
    }
}
