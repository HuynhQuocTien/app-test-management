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

namespace GUI.LopHoc
{
    public partial class fChiTietLop : Form
    {
        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

        public fChiTietLop()
        {
            InitializeComponent();
            CreatePanel();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            fDanhSachDeThi f = new fDanhSachDeThi();
            f.ShowDialog();
        }
        private void lblTenLop_Click_1(object sender, EventArgs e)
        {

        }
        private void btnXemDSSV_Click(object sender, EventArgs e)
        {
            fDanhSachSV fdsv = new fDanhSachSV();
            fdsv.ShowDialog();
        }
        private void lblMaMoi_Click(object sender, EventArgs e)
        {

        }
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

            if (r == 134 && r == 142 && r == 150)
            {
                return GetRandomColor();
            }
            return Color.FromArgb(r, g, b);
        }

        private void CreatePanel()
        {
            Panel panelContain = new Panel
            {
                Location = new Point(3, 3),
                Name = "panelContain",
                Size = new System.Drawing.Size(390, 350),
                TabIndex = 0,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 10, 10, 10),

            };

            Panel panelHead = new Panel
            {
                Location = new Point(0, 0),
                Name = "panelHead",
                Size = new System.Drawing.Size(390, 290),
                TabIndex = 1,

            };

            Label lblTenDeThi = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(10, 9),
                Name = "lblTenDeThi",
                Size = new System.Drawing.Size(300, 200),
                TabIndex = 0,
                Text = "Đề thi môn toán",
                AutoEllipsis = true
            };
            toolTip.SetToolTip(lblTenDeThi, lblTenDeThi.Text);




            Label lblMonHoc = new Label
            {
                AutoSize = true,
                Location = new Point(20, 190),
                Name = "lblMonHoc1",
                Size = new System.Drawing.Size(110, 13),
                TabIndex = 1,
                Text = "Môn học: Lập trình C#",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)

            };

            Label lblThoiGianLamBai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 220),
                Name = "lblThoiGianLamBai",
                Size = new System.Drawing.Size(140, 13),
                TabIndex = 2,
                Text = "Thời gian làm bài: 120 phút",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)

            };

            Label lblTrangThai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 250),
                Name = "lblMonHoc1",
                Size = new System.Drawing.Size(110, 13),
                TabIndex = 1,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)

            };

            string thoiGianText = "Còn 12p";
            
            lblTrangThai.Text = "Trạng thái: Đang mở";

            System.Windows.Forms.Button btnLamBai = new System.Windows.Forms.Button
            {
                Location = new Point(10, 300),
                Name = "button2",
                Size = new System.Drawing.Size(120, 41),
                TabIndex = 2,
                Text = "Làm bài thi",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
                Enabled = true,
            };

            
            System.Windows.Forms.Button btnXemKq = new System.Windows.Forms.Button
            {
                Location = new Point(145, 300),
                Name = "button2",
                Size = new System.Drawing.Size(120, 41),
                TabIndex = 2,
                Text = "Kết quả",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
                Visible = true ,
            };

            System.Windows.Forms.Button btnDong = new System.Windows.Forms.Button
            {
                Location = new Point(280, 300),
                Name = "button2",
                Size = new System.Drawing.Size(100, 41),
                TabIndex = 2,
                Text = "Đóng",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter // Đặt văn bản ở giữa theo cả hai chiều
            };
            
            panelHead.Controls.AddRange(new Control[] { lblThoiGianLamBai, lblMonHoc, lblTenDeThi, lblTrangThai });

            panelContain.Location = new Point(20, flowLayoutPanel1.Controls.Count * 150);
            flowLayoutPanel1.Controls.Add(panelContain);
            panelContain.Controls.AddRange(new Control[] { btnDong, panelHead, btnLamBai, btnXemKq });
            flowLayoutPanel1.AutoScroll = true;        
            btnLamBai.Text = "Mở để thi";
            btnLamBai.Enabled = true;
            btnDong.Enabled = false;            
            panelHead.BackColor = GetRandomColor();
            panelHead.Enabled = true;
        }
    }
}
