using DAL;
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
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Control = System.Windows.Forms.Control;
using Formatting = Newtonsoft.Json.Formatting;

namespace GUI
{
    public partial class fLayout : Form
    {   
        public fLayout()
        {
            InitializeComponent();
            //this.containerBtnPanel.Controls.Add(this.btnLopHoc, 0, 2);
            //this.containerBtnPanel.Controls.Add(this.btnMonHoc, 0, 3);
            //this.containerBtnPanel.Controls.Add(this.btnCauHoi, 0, 4);
            //this.containerBtnPanel.Controls.Add(this.btnDeThi, 0, 5);
            //this.containerBtnPanel.Controls.Add(this.btnNguoiDung, 0, 6);
            //this.containerBtnPanel.Controls.Add(this.btnPhanCong, 0, 7);
            ////this.containerBtnPanel.Controls.Add(this.btnPhanQuyen, 0, 8);
            //this.containerBtnPanel.Controls.Add(this.btnThongKe, 0, 8);
            //this.containerBtnPanel.Controls.Add(this.btnThoat, 0, 9);
            AddColorChange(btnHome, hoverColor, hoverColor);
            AddColorChange(btnCauHoi, hoverColor, hoverColor);
            AddColorChange(btnMonHoc, hoverColor, hoverColor);
            AddColorChange(btnLopHoc, hoverColor, hoverColor);
            AddColorChange(btnDeThi, hoverColor, hoverColor);
            AddColorChange(btnThongKe, hoverColor, hoverColor);
            AddColorChange(btnThoat, hoverColor, hoverColor);
            AddColorChange(btnPhanCong, hoverColor, hoverColor);
            AddColorChange(btnLichSuDangNhap, hoverColor, hoverColor);
            LoadInfoOwner();
            //AddColorChange(btnPhanQuyen, hoverColor, hoverColor);
            infoPanelBox.Paint += (sender, e) =>
            {
                Control control = (Control)sender;
                int borderWidth = 2; // Độ rộng của đường viền

                using (Pen borderPen = new Pen(borderColor, borderWidth))
                {
                    // Vẽ đường viền bên dưới của TableLayoutPanel
                    e.Graphics.DrawLine(borderPen, 0, control.Height - borderWidth, control.Width, control.Height - borderWidth);
                }
            };
            containerBtnPanel.Paint += (sender, e) =>
            {
                Control control = (Control)sender;
                int borderWidth = 2; // Độ rộng của đường viền

                using (Pen borderPen = new Pen(borderColor, borderWidth))
                {
                    // Vẽ đường viền bên phải của TableLayoutPanel
                    e.Graphics.DrawLine(borderPen, control.Width - borderWidth, 0, control.Width - borderWidth, control.Height);
                }
            };
            HideAllUserControls();
            fLandingPanel.Visible = true;
        }
        public void LoadInfoOwner()
        {
            NguoiDungDTO u = fDangNhap.nguoiDungDTO;

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
            String avatarFolderPath = Path.Combine(exePath, "GUI", "Users", "Avatar");

            // Tạo đường dẫn đầy đủ tới file ảnh
            string imagePath = Path.Combine(avatarFolderPath, u.Avatar);

            string defaultImagePath = Path.Combine(avatarFolderPath, "images.png");
            // Kiểm tra xem file ảnh có tồn tại hay không
            if (File.Exists(imagePath))
            {
                // Gán ảnh vào PictureBox nếu file tồn tại
                pictureOwner.Image = Image.FromFile(imagePath);
            }
            else
            {
                // Nếu không tìm thấy ảnh, bạn có thể hiển thị ảnh mặc định
                pictureOwner.Image = Image.FromFile(defaultImagePath); ; // Hoặc gán ảnh mặc định
            }
            lblOwnerName.Text = u.HoTen;
            lblOwnerRule.Text = fDangNhap.nhomQuyenDTO.TenQuyen;
        }
        private void HideAllUserControls()
        {
            fLandingPanel.Visible = false;
            lopHocPanel.Visible = false;
            monHocPanel.Visible = false;
            cauHoiPanel.Visible = false;
            deThiPanel.Visible = false;
            thongKePanel.Visible = false;
            userPanel.Visible = false;
            phanCongPanel.Visible = false;
            nhomQuyenPanel.Visible = false;
            fLichSuDangNhapPanel.Visible = false;

        }
        private void ShowUserControl(UserControl showControl)
        {
            HideAllUserControls();
            showControl.Visible = true;
        }
        private void AddColorChange(System.Windows.Forms.Button button, System.Drawing.Color hoverColor, System.Drawing.Color clickColor)
        {
            System.Drawing.Color defaultBackColor = button.BackColor;
            System.Drawing.Color defaultForeColor = button.ForeColor;

            button.MouseEnter += (sender, e) =>
            {
                button.BackColor = hoverColor;
                button.ForeColor = System.Drawing.Color.Black;
            };

            button.MouseHover += (sender, e) =>
            {
                button.BackColor = hoverColor;
                button.ForeColor = System.Drawing.Color.Black;
            };

            button.MouseLeave += (sender, e) =>
            {
                button.BackColor = defaultBackColor;
                button.ForeColor = defaultForeColor;
            };

            button.Click += (sender, e) =>
            {
                button.BackColor = clickColor;
                button.ForeColor = System.Drawing.Color.Black;
            };
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            //fLandingPanel = new fLanding();
            ShowUserControl(fLandingPanel);
            this.Text = "Trang chủ";
        }
        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            //lopHocPanel = new LopHoc.LopHocControl();
            ShowUserControl(lopHocPanel);
            this.Text = "Lớp học";
        }

        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            //monHocPanel = new MonHoc.MonHocControl();
            ShowUserControl(monHocPanel);
            this.Text = "Môn học";
        }

        private void btnCauHoi_Click(object sender, EventArgs e)
        {
            //cauHoiPanel = new CauHoi.CauHoiControl();
            ShowUserControl(cauHoiPanel);
            this.Text = "Câu hỏi";
        }

        private void btnDeThi_Click(object sender, EventArgs e)
        {
            //deThiPanel = new DeThi.DeThiControl();
            ShowUserControl(deThiPanel);
            this.Text = "Đề thi";
        }
        private void btnLichSuDangNhap_Click(object sender, EventArgs e)
        {
            //fLichSuDangNhapPanel = new fLichSuDangNhap();
            ShowUserControl(fLichSuDangNhapPanel);
            this.Text = "Lịch sử đăng nhập";
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            //thongKePanel = new fThongKe();
            ShowUserControl(thongKePanel);
            this.Text = "Thống kê";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                fDangNhap formDangNhap = new fDangNhap();
                formDangNhap.Show();
                this.Close();
            }
        }

        private void btnNguoiDung_Click(object sender, EventArgs e)
        {
            ShowUserControl(userPanel);
            this.Text = "Người dùng";
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            InfoUser info = new InfoUser(this);
            info.Visible = true;

        }
        public void updateAvatar(string ava)
        {
            pictureOwner.Image = Image.FromFile(ava); ;

        }
        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            ShowUserControl(phanCongPanel);
            this.Text = "Phân công";


        }

        private void fLayout_Load(object sender, EventArgs e)
        {
            string tenquyen = fDangNhap.nhomQuyenDTO.TenQuyen;
            //this.containerBtnPanel.Controls.Add(this.btnLopHoc, 0, 2);
            //this.containerBtnPanel.Controls.Add(this.btnMonHoc, 0, 3);
            //this.containerBtnPanel.Controls.Add(this.btnCauHoi, 0, 4);
            //this.containerBtnPanel.Controls.Add(this.btnDeThi, 0, 5);
            //this.containerBtnPanel.Controls.Add(this.btnNguoiDung, 0, 6);
            //this.containerBtnPanel.Controls.Add(this.btnPhanCong, 0, 7);
            ////this.containerBtnPanel.Controls.Add(this.btnPhanQuyen, 0, 8);
            //this.containerBtnPanel.Controls.Add(this.btnThongKe, 0, 8);
            //this.containerBtnPanel.Controls.Add(this.btnThoat, 0, 9);
            if (tenquyen.Contains("Admin"))
            {
                containerBtnPanel.Controls.Add(btnNguoiDung, 0, 6);
                containerBtnPanel.RowStyles.Add(new RowStyle());
                containerBtnPanel.Controls.Add(btnThongKe, 0, 8);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }
            if(tenquyen.Contains("GV Phân công") || tenquyen.Contains("Admin"))
            {
                containerBtnPanel.Controls.Add(this.btnPhanCong, 0, 7);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }
            if (tenquyen.Contains("Giáo viên") || tenquyen.Contains("Học sinh") || tenquyen.Contains("GV Phân công"))
            {
                containerBtnPanel.Controls.Add(btnLopHoc, 0, 2);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            if (tenquyen.Contains("Giáo viên") || tenquyen.Contains("Admin"))
            {
                containerBtnPanel.Controls.Add(btnCauHoi, 0, 4);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            if (tenquyen.Contains("Giáo viên") )
            {
                containerBtnPanel.Controls.Add(btnDeThi, 0, 5);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            if (tenquyen.Contains("Admin") || tenquyen.Contains("GV Phân công"))
            {
                containerBtnPanel.Controls.Add(btnMonHoc, 0, 3);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            containerBtnPanel.Controls.Add(btnLichSuDangNhap, 0, 10);
            containerBtnPanel.RowStyles.Add(new RowStyle());
            containerBtnPanel.Controls.Add(btnThoat, 0,11);
            containerBtnPanel.RowStyles.Add(new RowStyle());

        }
        private void fLayout_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateLogoutTime(fDangNhap.nguoiDungDTO.HoTen.ToString() + "_" + fDangNhap.LoginTime.ToString());
        }
        private void UpdateLogoutTime(string loginId)
        {
            List<NguoiDungDTO> loginHistories = new List<NguoiDungDTO>();

            // Đọc dữ liệu từ tệp JSON nếu nó đã tồn tại
            if (File.Exists("loginHistory.json"))
            {
                string json = File.ReadAllText("loginHistory.json");
                loginHistories = JsonConvert.DeserializeObject<List<NguoiDungDTO>>(json);
            }

            // Tìm thông tin đăng nhập của người dùng và cập nhật thời gian đăng xuất
            NguoiDungDTO userHistory = loginHistories.FirstOrDefault(history => history.IdLogin == loginId);
            if (userHistory != null)
            {
                userHistory.TimeOut = DateTime.Now;
            }

            // Ghi lại danh sách vào tệp JSON
            string updatedJson = JsonConvert.SerializeObject(loginHistories, Formatting.Indented);
            File.WriteAllText("loginHistory.json", updatedJson);
        }


    }
}
