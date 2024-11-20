using DAL;
using DTO;
using System.IO;
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
using DocumentFormat.OpenXml.Spreadsheet;

namespace GUI.Users
{
    public partial class InfoUser : Form
    {
        private string userID;
        private string currentImagePath;
        private string avatarFolderPath;
        private fLayout layoutF;
        public InfoUser(fLayout f)
        {
            InitializeComponent();
            this.userID = Session.UserID;
            layoutF = f;
            SqlConnection conn = GetConnectionDb.GetConnection();
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
            // Tạo đường dẫn đến thư mục Avatar
            avatarFolderPath = Path.Combine(exePath, "GUI", "Users", "Avatar");

            // Kiểm tra xem thư mục có tồn tại không, nếu không thì tạo mới
            if (!Directory.Exists(avatarFolderPath))
            {
                Directory.CreateDirectory(avatarFolderPath);
            }
            reader.Close();
            LoadUserInfo();
        }
        public InfoUser()
        {
            InitializeComponent();
            this.userID = Session.UserID;
            SqlConnection conn = GetConnectionDb.GetConnection();
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
            // Tạo đường dẫn đến thư mục Avatar
            avatarFolderPath = Path.Combine(exePath, "GUI", "Users", "Avatar");

            // Kiểm tra xem thư mục có tồn tại không, nếu không thì tạo mới
            if (!Directory.Exists(avatarFolderPath))
            {
                Directory.CreateDirectory(avatarFolderPath);
            }
            reader.Close();
            LoadUserInfo();
        }
        private void LoadUserInfo()
        {
            // Sử dụng lớp từ DAL để lấy thông tin người dùng từ database
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
        private void button2_Click_1(object sender, EventArgs e)
        {
            buttonUpImg.Visible = true;
            textBoxName.Enabled = true;
            textBoxEmail.Enabled = true;
            textBox2.Enabled = true;

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    // Update Email in TaiKhoan table
                    string queryEmail = "UPDATE TaiKhoan SET Email=@Email WHERE Username=@UserID;";
                    using (SqlCommand cmdEmail = new SqlCommand(queryEmail, conn))
                    {
                        cmdEmail.Parameters.AddWithValue("@Email", textBoxEmail.Text);
                        cmdEmail.Parameters.AddWithValue("@UserID", userID);
                        cmdEmail.ExecuteNonQuery();
                    }

                    string newFileName = $"Avatar_{userID}{Path.GetExtension(currentImagePath)}";
                    string newAvatarPath = Path.Combine(avatarFolderPath, newFileName);
                    //MessageBox.Show("Image Path: " + newAvatarPath + "Current: " + currentImagePath);

                    string fileName = Path.GetFileName(currentImagePath);
                    if (File.Exists(newAvatarPath))
                    {

                        try
                        {
                            // Giải phóng tài nguyên trước khi xóa
                            GC.Collect();   // Thu gom rác
                            GC.WaitForPendingFinalizers(); // Đảm bảo thu gom hoàn tất

                            // Mở file stream để chắc chắn tệp không bị khóa
                            using (FileStream fs = new FileStream(newAvatarPath, FileMode.Open, FileAccess.Read, FileShare.None))
                            {
                                fs.Close();
                            }

                            // Xóa file cũ
                            File.Delete(newAvatarPath);
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"Tệp đang được sử dụng bởi một tiến trình khác: {ioEx.Message}");
                            return;  // Kết thúc việc sao chép nếu không xóa được file
                        }
                    }
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
                    layoutF.updateAvatar(newAvatarPath);
                    MessageBox.Show("Cập nhật thông tin thành công!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            }
        }
        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }
        private void buttonUpImg_Click_1(object sender, EventArgs e)
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