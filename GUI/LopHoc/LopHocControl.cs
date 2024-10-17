using BLL;
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
    public partial class LopHocControl : UserControl
    {
        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        LopBLL lopBLL;
        List<LopDTO> listlop;

        public LopHocControl()
        {
            InitializeComponent();
            lopBLL = new LopBLL();
            listlop = new List<LopDTO>();
            CreatePanel();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            fThemLop themLop = new fThemLop(this,GenerateRandomCode(10));

            themLop.ShowDialog();
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
                Size = new Size(360, 350),
                TabIndex = 0,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 10, 10, 10)
            };

            Panel panelHead = new Panel
            {
                Location = new Point(0, 0),
                Name = "panelHead",
                Size = new Size(360, 290),
                TabIndex = 1,
                BackColor = GetRandomColor()
            };

            Label labelMonhoc = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(10, 9),
                Name = "labelMonhoc",
                Size = new Size(300, 200),
                TabIndex = 0,
                Text = "Lop ABC",
                AutoEllipsis = true
            };
            toolTip.SetToolTip(labelMonhoc, labelMonhoc.Text);


            System.Windows.Forms.Button buttonThamGia = new System.Windows.Forms.Button
            {
                Location = new Point(60, 300),
                Name = "button2",
                Size = new Size(100, 40),
                TabIndex = 2,
                Text = "Vào lớp",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
            };

            System.Windows.Forms.Button buttonXoa = new System.Windows.Forms.Button
            {
                Location = new Point(200, 300),
                Name = "button3",
                Size = new Size(100, 40),
                TabIndex = 3,
                Text = "Xóa",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Visible = true
            };
            buttonThamGia.Click += (s, ev) =>
            {
                buttonThamGia_Click(s, ev);
            };
            panelHead.Controls.AddRange(new Control[] { labelMonhoc });
            panelContain.Controls.AddRange(new Control[] { buttonThamGia, buttonXoa, panelHead });

            panelContain.Location = new Point(20, flowLayoutPanel1.Controls.Count * 150);
            flowLayoutPanel1.Controls.Add(panelContain);

            flowLayoutPanel1.AutoScroll = true;

        }
        private void buttonThamGia_Click(object sender, EventArgs e)
        {
            fChiTietLop fct = new fChiTietLop();
            fct.ShowDialog();
        }
        private string GenerateRandomCode(int length)//randomMaMoi Còn lỗi
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghiklmnopqrstuvwxyz0123456789"; // Các ký tự và số có thể sử dụng
            Random random = new Random();
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                // sinh số ngẫu nhiên dựa theo độ dài của mảng ký tự
                int index = random.Next(chars.Length);
                code.Append(chars[index]);
            }

            return code.ToString();
        }

        public void AddLop(LopDTO obj)
        {
            listlop.Add(obj);
            lopBLL.Add(obj);
            CreatePanel();
        }
    }
}
