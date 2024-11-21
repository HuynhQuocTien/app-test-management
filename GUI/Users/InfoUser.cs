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
using BLL;

namespace GUI.Users
{
    public partial class InfoUser : Form
    {
        private long userID;
        private string currentImagePath;
        private string avatarFolderPath;
        private fLayout layoutF;
        public InfoUser(fLayout f)
        {
            InitializeComponent();
            this.userID = fDangNhap.nguoiDungDTO.MaNguoiDung;
            layoutF = f;

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
            LoadUserInfo();
        }
        public InfoUser()
        {
            InitializeComponent();
            this.userID = fDangNhap.nguoiDungDTO.MaNguoiDung;

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
            LoadUserInfo();
        }
        private void LoadUserInfo()
        {
            try
            {
                if (fDangNhap.nguoiDungDTO != null)                    // Hiển thị thông tin người dùng lên form nếu có dữ liệu
                {
                    // Lấy tên file ảnh từ cơ sở dữ liệu
                    string tenAnh = fDangNhap.nguoiDungDTO.Avatar;

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
                    textBoxID.Text = fDangNhap.nguoiDungDTO.MaNguoiDung.ToString();
                    textBoxEmail.Text = fDangNhap.taiKhoanDTO.Email.ToString();
                    textBoxName.Text = fDangNhap.nguoiDungDTO.HoTen.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(fDangNhap.nguoiDungDTO.NgaySinh);
                    if (fDangNhap.nguoiDungDTO.GioiTinh.ToString() == "1")
                    {
                        rbNam.Checked = true;
                        RbNu.Checked = false;
                    }
                    else
                    {
                        rbNam.Checked = false;
                        RbNu.Checked = true;
                    }
                    txtPass.Text = fDangNhap.taiKhoanDTO.Password.ToString();
                    textBox2.Text = fDangNhap.nguoiDungDTO.SDT.ToString();
                    int maQuyen = fDangNhap.taiKhoanDTO.MaNhomQuyen;
                    switch (maQuyen)
                    {
                        case 1:
                            comboBox1.SelectedIndex = 0; // Chọn mục đầu tiên trong ComboBox
                            break;

                        case 2:
                            comboBox1.SelectedIndex = 1; // Chọn mục thứ hai trong ComboBox
                            break;

                        case 3:
                            comboBox1.SelectedIndex = 2; // Chọn mục thứ ba trong ComboBox
                            break;
                        case 5:
                            comboBox1.SelectedIndex = 3; // Chọn mục thứ ba trong ComboBox
                            break;
                        default:
                            comboBox1.SelectedIndex = -1; // Không chọn mục nào (nếu giá trị không phù hợp)
                            break;
                    }

                    comboBox1.Enabled = false;
                    if (fDangNhap.taiKhoanDTO.TrangThai.ToString() == "1")
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
        private void button2_Click_1(object sender, EventArgs e)
        {
            buttonUpImg.Visible = true;
            textBoxName.Enabled = true;
            textBoxEmail.Enabled = true;
            textBox2.Enabled = true;

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Update Email in TaiKhoan table
                TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
                string thongbao = taiKhoanBLL.updateEmail(fDangNhap.taiKhoanDTO.Email.ToString(), textBoxEmail.Text, fDangNhap.taiKhoanDTO.Username);
                if (thongbao == "1")
                {
                    fDangNhap.taiKhoanDTO.Email = textBoxEmail.Text;
                    string thongbaoInfo = "";
                    if (!string.IsNullOrEmpty(currentImagePath))
                    {
                        string newFileName = $"Avatar_{userID}{Path.GetExtension(currentImagePath)}";
                        string newAvatarPath = Path.Combine(avatarFolderPath, newFileName);
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
                        layoutF.updateAvatar(newAvatarPath);
                        // Update Ten and SDT in NguoiDung table
                        NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
                        thongbaoInfo = nguoiDungBLL.updateInfo(textBoxName.Text, textBox2.Text, newFileName, fDangNhap.nguoiDungDTO.MaNguoiDung.ToString());
                        if(thongbaoInfo=="1")
                        {
                            fDangNhap.nguoiDungDTO.Avatar = newFileName;
                            fDangNhap.nguoiDungDTO.HoTen = textBoxName.Text;
                            fDangNhap.nguoiDungDTO.SDT = textBox2.Text;
                            MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Lỗi thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } 
                            
                    }
                    else
                    {
                        NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
                        thongbaoInfo = nguoiDungBLL.updateInfo(textBoxName.Text, textBox2.Text, fDangNhap.nguoiDungDTO.Avatar.ToString(), fDangNhap.nguoiDungDTO.MaNguoiDung.ToString());
                        if (thongbaoInfo == "1")
                        {
                            fDangNhap.nguoiDungDTO.HoTen = textBoxName.Text;
                            fDangNhap.nguoiDungDTO.SDT = textBox2.Text;
                            MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Lỗi thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (thongbao == "2")
                {
                    MessageBox.Show("Lỗi Update Email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(thongbao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
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