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
            lblOwnerName.Text = u.HoTen;
            lblOwnerRule.Text = fDangNhap.nhomQuyenDTO.TenQuyen;
            if (!string.IsNullOrWhiteSpace(u.Avatar) && System.IO.File.Exists(u.Avatar))
            {
                pictureOwner.ImageLocation = u.Avatar;
            }
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
            ShowUserControl(fLandingPanel);
            this.Text = "Trang chủ";
        }

        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            ShowUserControl(lopHocPanel);
            this.Text = "Lớp học";
        }

        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            ShowUserControl(monHocPanel);
            this.Text = "Môn học";
        }

        private void btnCauHoi_Click(object sender, EventArgs e)
        {
            ShowUserControl(cauHoiPanel);
            this.Text = "Câu hỏi";
        }

        private void btnDeThi_Click(object sender, EventArgs e)
        {
            ShowUserControl(deThiPanel);
            this.Text = "Đề thi";
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
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
            InfoUser info = new InfoUser();
            info.Visible = true;

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
            if (tenquyen.Contains("Admin") || tenquyen.Contains("GV Phân công"))
            {
                containerBtnPanel.Controls.Add(btnNguoiDung, 0, 6);
                containerBtnPanel.RowStyles.Add(new RowStyle());
                containerBtnPanel.Controls.Add(this.btnPhanCong, 0, 7);
                containerBtnPanel.RowStyles.Add(new RowStyle());
                containerBtnPanel.Controls.Add(btnThongKe, 0, 8);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            if (tenquyen.Contains("Giảng viên") || tenquyen.Contains("Sinh viên") || tenquyen.Contains("GV Phân công"))
            {
                containerBtnPanel.Controls.Add(btnLopHoc, 0, 2);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            if (tenquyen.Contains("Giảng viên") || tenquyen.Contains("GV Phân công"))
            {
                containerBtnPanel.Controls.Add(btnCauHoi, 0, 4);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            if (tenquyen.Contains("Giảng viên") || tenquyen.Contains("GV Phân công"))
            {
                containerBtnPanel.Controls.Add(btnDeThi, 0, 5);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            if (tenquyen.Contains("Admin") || tenquyen.Contains("GV Phân công"))
            {
                containerBtnPanel.Controls.Add(btnMonHoc, 0, 3);
                containerBtnPanel.RowStyles.Add(new RowStyle());
            }

            containerBtnPanel.Controls.Add(btnThoat, 0, 9);
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
