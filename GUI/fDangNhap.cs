using BLL;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using GUI.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
using Font = System.Drawing.Font;
using Formatting = Newtonsoft.Json.Formatting;

namespace GUI
{
    public partial class fDangNhap : Form
    {
        public static NguoiDungDTO nguoiDungDTO;
        public static TaiKhoanDTO taiKhoanDTO;
        public static NhomQuyenDTO nhomQuyenDTO;
        public static DateTime LoginTime = DateTime.Now;

        private NguoiDungBLL nguoiDungBLL;
        private NhomQuyenBLL nhomQuyenBLL;
        private DeThiBLL deThiBLL;

        private Size formOriginalSize;
        private Rectangle recLab1;
        private Rectangle recLab2;
        private Rectangle recLab3;
        private Rectangle recLab4;
        private Rectangle recBut1;
        private Rectangle recTxt1;
        private Rectangle recTxt2;
        private Rectangle recCBox1;

        TaiKhoanBLL taiKhoanBLL;
        public fDangNhap()
        {
            taiKhoanBLL = new TaiKhoanBLL();
            InitializeComponent();
            this.Resize += Form1_Resiz;
            formOriginalSize = this.Size;
            recLab1 = new Rectangle(label1.Location, label1.Size);
            recLab2 = new Rectangle(label2.Location, label2.Size);
            recLab3 = new Rectangle(label3.Location, label3.Size);
            recLab4 = new Rectangle(label4.Location, label4.Size);
            recBut1 = new Rectangle(button1.Location, button1.Size);
            recTxt1 = new Rectangle(textBox1.Location, textBox1.Size);
            recTxt2 = new Rectangle(textBox2.Location, textBox2.Size);
            recCBox1 = new Rectangle(checkBox1.Location, checkBox1.Size);
            textBox1.Multiline = true;
            textBox2.Multiline = true;
            nguoiDungBLL = new NguoiDungBLL();
            nhomQuyenBLL = new NhomQuyenBLL();
        }

        private void Form1_Resiz(object sender, EventArgs e)
        {
            resize_Control(button1, recBut1);
            resize_Control(textBox1, recTxt1);
            resize_Control(textBox2, recTxt2);
            resize_Control(label1, recLab1);
            resize_Control(label2, recLab2);
            resize_Control(label3, recLab3);
            resize_Control(label4, recLab4);
            resize_Control(checkBox1, recCBox1);
        }
        private void resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
            float newFontSize = c.Font.Size * Math.Min(xRatio, yRatio);
            if (newFontSize > 20)
            {
                newFontSize = 20; // Keep the maximum font size
            }
            else if (newFontSize < 6)
            {
                newFontSize = 6; // Keep the minimum font size
            }
            c.Font = new Font(c.Font.FontFamily, newFontSize);
        }

        // Đăng nhập btn
        private void button1_Click_1(object sender, EventArgs e)
        {
            string taiKhoan = textBox1.Text.Trim();
            string matKhau = textBox2.Text.Trim();
            if (String.IsNullOrEmpty(taiKhoan)){
                MessageBox.Show("Vui lòng nhập tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TaiKhoanDTO taiKhoanTest= taiKhoanBLL.getTaiKhoanById(Convert.ToInt64(taiKhoan)) ?? null;
            if(taiKhoanTest == null)
            {
                MessageBox.Show("Tài khoản không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (taiKhoanTest.TrangThai == 0)
            {
                MessageBox.Show("Tài khoản đã bị khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string thongbBao = taiKhoanBLL.kiemTraTaiKhoan(taiKhoan, matKhau);
            if (thongbBao.Equals("Đăng nhập thành công!"))
            {
                Session.UserID = taiKhoan;
                nguoiDungDTO = nguoiDungBLL.getUserLoginById(Convert.ToInt64(taiKhoan));
                taiKhoanDTO = taiKhoanTest;
                nhomQuyenDTO = nhomQuyenBLL.getNhomQuyenById(taiKhoanDTO.MaNhomQuyen);
                fLayout formLayout = new fLayout();
                formLayout.Show();
                this.Visible = false;
                SaveLoginHistory(fDangNhap.nguoiDungDTO.MaNguoiDung.ToString() + "_" + LoginTime.ToString());
            }
            else
            {
                MessageBox.Show(thongbBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*' && checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }
        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }
        private void lblQuenMatKhau_Click(object sender, EventArgs e)
        {
            fNhapInfo form = new fNhapInfo();
            form.Show();
        }

        // Ẩn hiện mk
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click_1(sender, e);
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click_1(sender, e);
            }
        }

        private void fDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public void SaveLoginHistory(string idLogin)
        {
            List<NguoiDungDTO> loginHistories = new List<NguoiDungDTO>();

            // Đọc dữ liệu từ tệp JSON nếu nó đã tồn tại
            if (File.Exists("loginHistory.json"))
            {
                string json = File.ReadAllText("loginHistory.json");
                loginHistories = JsonConvert.DeserializeObject<List<NguoiDungDTO>>(json);
            }

            NguoiDungDTO u = nguoiDungBLL.getUserLoginById(fDangNhap.nguoiDungDTO.MaNguoiDung);

            // Check if loginHistories is null
            if (loginHistories == null)
            {
                loginHistories = new List<NguoiDungDTO>();
            }
            u.TimeIn = LoginTime;
            u.IdLogin = idLogin;
            NguoiDungDTO userHistory = u;

            loginHistories.Add(userHistory);
            // Ghi lại danh sách vào tệp JSON
            string updatedJson = JsonConvert.SerializeObject(loginHistories, Formatting.Indented);
            File.WriteAllText("loginHistory.json", updatedJson);
        }

    }
}
