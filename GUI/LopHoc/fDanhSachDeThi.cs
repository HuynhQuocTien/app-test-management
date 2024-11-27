using BLL;
using DAL;
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

namespace GUI.LopHoc
{
    public partial class fDanhSachDeThi : Form
    {
        private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        fChiTietLop fctl;
        LopDTO lop;
        DeThiBLL deThiBLL;
        List<DeThiDTO> listDeThi;
        GiaoDeThiBLL giaoDeThiBLL;
        MonHocBLL monHocBLL;
   
        public fDanhSachDeThi(fChiTietLop fctl,LopDTO lop)
        {
            InitializeComponent();
            deThiBLL = new DeThiBLL();
            monHocBLL = new MonHocBLL();
            listDeThi = new List<DeThiDTO>();
            this.fctl = fctl;
            this.lop = lop;
            LoadMonHoc();
            renderDeThiDTO(deThiBLL.GetAllDeThiByCondition(0, ""));
        }

        private void LoadMonHoc()
        {
            try
            {
                var monHocs = monHocBLL.GetAll();
                var subjectsList = new List<MonHocDTO>();
                subjectsList.Add(new MonHocDTO { MaMonHoc = 0, TenMonHoc = "Tất cả" }); 
                subjectsList.AddRange(monHocs);
                cbMonHoc.DataSource = subjectsList;
                cbMonHoc.DisplayMember = "TenMonHoc";
                cbMonHoc.ValueMember = "MaMonHoc";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subjects: " + ex.Message);
            }
        }



        public void renderDeThiDTO(List<DeThiDTO> list)
        {
            listDeThi = list;
            // Xóa tất cả các panel được tạo trước đó
            flowLayoutPanel1.Controls.Clear();
            foreach (var l in listDeThi)
            {
                if (!deThiBLL.checkDeThiCoTrongLop(l.MaDe, lop.MaLop)) {
                    CreatePanel(l);
                }

            }
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

            return Color.FromArgb(r, g, b);
        }


        private void buttonThem_Click(object sender, EventArgs e, DeThiDTO obj)
        {
            int maDe = Convert.ToInt32(obj.MaDe);
            int maLop = Convert.ToInt32(lop.MaLop);
            long maNguoiDung = Convert.ToInt64(fDangNhap.nguoiDungDTO.MaNguoiDung);
            GiaoDeThiDTO giaoDeThiAdd = new GiaoDeThiDTO(maDe, maLop, maNguoiDung, 0,0);
            GiaoDeThiBLL giaoDeThiBLL = new GiaoDeThiBLL();
            System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;
            Panel panelContain = (Panel)clickedButton.Parent;
            DialogResult result = MessageBox.Show("Xác nhận thêm đề thi vào lớp?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                giaoDeThiBLL.Add(giaoDeThiAdd);
                renderDeThiDTO(deThiBLL.getDeThiByMaGV(fDangNhap.nguoiDungDTO.MaNguoiDung));
                fctl.RenderDeThi();
                this.Close();
                this.Dispose();
            }
        }

        private void CreatePanel(DeThiDTO deThi)
        {
            Panel panelContain = new Panel
            {
                Location = new Point(3, 3),
                Name = "panelContain",
                Size = new Size(390, 350),
                TabIndex = 0,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 10, 10, 10),
            };

            Panel panelHead = new Panel
            {
                Location = new Point(0, 0),
                Name = "panelHead",
                Size = new Size(390, 290),
                TabIndex = 1,
                BackColor = GetRandomColor()
            };

            Label lblTenDeThi = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(10, 9),
                Name = "lblTenDeThi",
                Size = new Size(300, 200),
                TabIndex = 0,
                Text = deThi.TenDe,
                AutoEllipsis = true
            };
            toolTip.SetToolTip(lblTenDeThi, lblTenDeThi.Text);



            Label lblMonHoc = new Label
            {
                AutoSize = true,
                Location = new Point(20, 220),
                Name = "lblMonHoc1",
                Size = new Size(110, 13),
                TabIndex = 1,
                Text = $"Môn học: {deThi.TenMonHoc}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)

            };

            Label lblThoiGianLamBai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 250),
                Name = "lblThoiGianLamBai",
                Size = new Size(140, 13),
                TabIndex = 2,
                Text = $"Thời gian làm bài: {deThi.ThoiGianLamBai} phút",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)

            };

            System.Windows.Forms.Button btnThem = new System.Windows.Forms.Button
            {
                Location = new Point(10, 300),
                Name = "button2",
                Size = new Size(200, 41),
                TabIndex = 2,
                Text = "Thêm đề thi vào lớp học",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter // Đặt văn bản ở giữa theo cả hai chiều
            };



            
           

            System.Windows.Forms.Button btnXem = new System.Windows.Forms.Button
            {
                Location = new Point(250, 300),
                Name = "button2",
                Size = new Size(100, 41),
                TabIndex = 2,
                Text = "Xem",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter // Đặt văn bản ở giữa theo cả hai chiều
            };

            btnThem.Click += (s, ev) =>
            {
                buttonThem_Click(s, ev, deThi);
            };

            btnXem.Click += (s, ev) =>
            {
                btnXem_Click(s, ev, deThi);
            };

            panelHead.Controls.AddRange(new Control[] { lblThoiGianLamBai, lblMonHoc, lblTenDeThi, });

            panelContain.Location = new Point(20, flowLayoutPanel1.Controls.Count * 150);
            flowLayoutPanel1.Controls.Add(panelContain);
            panelContain.Controls.AddRange(new Control[] { btnThem, panelHead, btnXem });

            flowLayoutPanel1.AutoScroll = true;
        }
        private void btnXem_Click(object s, EventArgs ev, DeThiDTO obj)
        {
            fXemDeThiCuaLop f = new fXemDeThiCuaLop(this, obj);
            f.ShowDialog();
        }

        private void btnThem_Click(object s, EventArgs ev, DeThiDTO obj)
        {

        }

        private void cbMonHoc_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            MonHocDTO cbMonHocValue = (MonHocDTO)cbMonHoc.SelectedItem;
            string txtDeThiValue = txtDeThi.Text;
            var monHocs = getListDeThiByCondition(cbMonHocValue.MaMonHoc, txtDeThiValue);
            renderDeThiDTO(monHocs);
        }

        private List<DeThiDTO> getListDeThiByCondition(int MaMonHoc, string txtDeThiValue)
        {
            DeThiBLL deThiBLL = new DeThiBLL();
            return deThiBLL.GetAllDeThiByCondition(MaMonHoc, txtDeThiValue);
        }
       


        private void button1_Click_1(object sender, EventArgs e)
        {
            txtDeThi.Text = "";
            cbMonHoc.SelectedValue = 0;
            renderDeThiDTO(getListDeThiByCondition(0, ""));
        }

        private void txtDeThi_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }
    }
}
