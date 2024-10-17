using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using GUI.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GUI
{
    public partial class fLayout : Form
    {   
        private NguoiDungDTO nguoiDungDTO = new NguoiDungDTO();
        private TaiKhoanDTO taiKhoanDTO = new TaiKhoanDTO();
        private NhomQuyenDTO nhomQuyenDTO = new NhomQuyenDTO();
        public fLayout(NguoiDungDTO nguoiDungDTO, TaiKhoanDTO taiKhoanDTO, NhomQuyenDTO nhomQuyenDTO)
        {
            InitializeComponent();
            this.containerBtnPanel.Controls.Add(this.btnLopHoc, 0, 2);
            this.containerBtnPanel.Controls.Add(this.btnMonHoc, 0, 3);
            this.containerBtnPanel.Controls.Add(this.btnCauHoi, 0, 4);
            this.containerBtnPanel.Controls.Add(this.btnDeThi, 0, 5);
            this.containerBtnPanel.Controls.Add(this.btnNguoiDung, 0, 6);
            this.containerBtnPanel.Controls.Add(this.btnPhanCong, 0, 7);
            //this.containerBtnPanel.Controls.Add(this.btnPhanQuyen, 0, 8);
            this.containerBtnPanel.Controls.Add(this.btnThongKe, 0, 8);
            this.containerBtnPanel.Controls.Add(this.btnThoat, 0, 9);
            AddColorChange(btnHome, hoverColor, hoverColor);
            AddColorChange(btnCauHoi, hoverColor, hoverColor);
            AddColorChange(btnMonHoc, hoverColor, hoverColor);
            AddColorChange(btnLopHoc, hoverColor, hoverColor);
            AddColorChange(btnDeThi, hoverColor, hoverColor);
            AddColorChange(btnThongKe, hoverColor, hoverColor);
            AddColorChange(btnThoat, hoverColor, hoverColor);
            AddColorChange(btnPhanCong, hoverColor, hoverColor);
            this.nguoiDungDTO = nguoiDungDTO;            
            this.taiKhoanDTO = taiKhoanDTO;
            this.nhomQuyenDTO = nhomQuyenDTO;
            lblOwnerName.Text = nguoiDungDTO.HoTen;
            lblOwnerRule.Text = nhomQuyenDTO.TenQuyen;
            //AddColorChange(btnPhanQuyen, hoverColor, hoverColor);

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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        //private void btnPhanQuyen_Click(object sender, EventArgs e)
        //{
        //    ShowUserControl(nhomQuyenPanel);
        //    this.Text = "Phân quyền";

        //}

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            ShowUserControl(phanCongPanel);
            this.Text = "Phân công";


        }

        private void UserForm_Load_1(object sender, EventArgs e)
        {
            

        }
        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }        
        
        

    }
}
