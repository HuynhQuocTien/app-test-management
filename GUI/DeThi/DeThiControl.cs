using BLL;
using DocumentFormat.OpenXml.Drawing.Charts;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = System.Drawing.Size;

namespace GUI.DeThi
{
    public partial class DeThiControl : UserControl
    {
        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        DeThiBLL deThiBLL;
        List<DeThiDTO> listDeThi;
        public DeThiControl()
        {
            InitializeComponent();
            deThiBLL = new DeThiBLL();
            listDeThi = new List<DeThiDTO>();
            renderDeThiDTO(deThiBLL.getDeThiByMaGV(fDangNhap.nguoiDungDTO.MaNguoiDung));
        }
        public void renderDeThiDTO(List<DeThiDTO> list)
        {
            listDeThi = list;
            // Xóa tất cả các panel được tạo trước đó
            flowLayoutPanel1.Controls.Clear();
            foreach (var l in listDeThi)
            {
                CreatePanel(l);
            }
        }
        //private void btnThem_Click(object sender, EventArgs e)
        //{
        //    fThemDeThi themLop = new fThemDeThi();
        //    themLop.ShowDialog();
        //}
        private void CreatePanel(DeThiDTO deThi)
        {
            Panel panelContain = new Panel
            {
                Location = new Point(3, 3),
                Name = "panelContain",
                Size = new Size(360, 350),
                TabIndex = 0,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 10, 10, 10),
            };

            Panel panelHead = new Panel
            {
                Location = new Point(0, 0),
                Name = "panelHead",
                Size = new Size(360, 290),
                TabIndex = 1,
                BackColor = GetRandomColor(),
            };

            Label lblTenDeThi = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(10, 9),
                Name = "lblTenDeThi",
                Size = new Size(300, 200),
                TabIndex = 0,
                Text = Text = deThi.TenDe,
                AutoEllipsis = true
            };
            toolTip.SetToolTip(lblTenDeThi, lblTenDeThi.Text);

            Label lblMonHoc = new Label
            {
                AutoSize = true,
                Location = new Point(20, 220),
                Name = "lblMonHoc1",
                Size = new Size(110, 13),
                TabIndex = 2,
                Text = $"Môn học: {deThi.TenMonHoc}",

            };

            Label lblThoiGianLamBai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 250),
                Name = "lblThoiGianLamBai",
                Size = new Size(140, 13),
                TabIndex = 1,
                Text = $"Thời gian làm bài: {(int)(deThi.ThoiGianKetThuc - deThi.ThoiGianBatDau).TotalMinutes} phút"
            };

            System.Windows.Forms.Button btnThemCauHoiVaoDe = new System.Windows.Forms.Button
            {
                Location = new Point(10, 300),
                Name = "button2",
                Size = new Size(200, 40),
                TabIndex = 2,
                Text = "Thêm câu hỏi vào đề",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
            };

            System.Windows.Forms.Button buttonXoa = new System.Windows.Forms.Button
            {
                Location = new Point(250, 300),
                Name = "button3",
                Size = new Size(100, 40),
                TabIndex = 3,
                Text = "Xóa",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
            };
            lblTenDeThi.Click += (s, ev) =>
            {
                labelDeThi_Click(s, ev, deThi);
            };
            buttonXoa.Click += (s, ev) =>
            {
                buttonXoa_Click(s, ev, deThi);
            };
            btnThemCauHoiVaoDe.Click += (s, ev) =>
            {
                btnThemCauHoiVaoDe_Click(s, ev, deThi);
            };
            panelHead.Controls.AddRange(new Control[] { lblMonHoc, lblThoiGianLamBai, lblTenDeThi });
            panelContain.Controls.AddRange(new Control[] { btnThemCauHoiVaoDe, buttonXoa, panelHead });

            panelContain.Location = new Point(20, flowLayoutPanel1.Controls.Count * 150);
            flowLayoutPanel1.Controls.Add(panelContain);
            flowLayoutPanel1.AutoScroll = true;

        }

        // Ramdom mau nhat
        private Color GetRandomColor()
        {
            Random random = new Random();
            int r = random.Next(256);
            int g = random.Next(256);
            int b = random.Next(256);

            // Làm cho màu sắc nhạt hơn bằng cách thêm 128 vào mỗi thành phần màu
            r += 128;
            g += 128;
            b += 128;

            // Đảm bảo rằng các thành phần màu không vượt quá 255
            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;

            return Color.FromArgb(r, g, b);
        }
        private void labelDeThi_Click(object sender, EventArgs e, DeThiDTO obj)
        {
            fThemDeThi suaDeThi = new fThemDeThi(this, "edit", obj);
            suaDeThi.ShowDialog();
        }
        private void buttonXoa_Click(object sender, EventArgs e, DeThiDTO obj)
        {
            deThiBLL.Delete(obj);
            System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;
            Panel panelContain = (Panel)clickedButton.Parent;
            DialogResult result = MessageBox.Show("Xác nhận xóa đề thi?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                flowLayoutPanel1.Controls.Remove(panelContain);
            }
        }
        private void btnThemCauHoiVaoDe_Click(object sender, EventArgs e, DeThiDTO obj)
        {
            fThemChiTietDeThi f = new fThemChiTietDeThi(obj);
            f.ShowDialog();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Giáo viên") || fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Admin"))
            {
                fThemDeThi themDeThi = new fThemDeThi(this, "add");
                themDeThi.ShowDialog();
            }
        }
        public void AddDeThi(DeThiDTO obj)
        {
            listDeThi.Add(obj);
            deThiBLL.Add(obj);
            CreatePanel(obj);
            renderDeThiDTO(deThiBLL.getDeThiByMaGV(fDangNhap.nguoiDungDTO.MaNguoiDung));
        }
        public void UpdateDeThi(DeThiDTO obj)
        {
            deThiBLL.Update(obj);
            DeThiBLL deThiBLLnew = new DeThiBLL();
            renderDeThiDTO(deThiBLL.getDeThiByMaGV(fDangNhap.nguoiDungDTO.MaNguoiDung));

        }
    }
}
