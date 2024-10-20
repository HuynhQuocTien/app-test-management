﻿using BLL;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
using Size = System.Drawing.Size;

namespace GUI.LopHoc
{
    public partial class fChiTietLop : Form
    {
        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        private LopHocControl lopHocControl;
        private LopDTO lopDTO;
        private int counter = 1;
        DeThiBLL deThiBLL;
        List<DeThiDTO> listDeThi;
        MonHocBLL monHocBLL;
        GiaoDeThiBLL giaoDeThiBLL;
        PhanCongBLL phanCongBLL;
        KetQuaBLL ketQuaBLL;
        NguoiDungBLL nguoiDungBLL;
        ChiTietDeBLL chiTietDeBLL;

        public fChiTietLop(LopHocControl lopHocControl, LopDTO lopDTO)
        {
            InitializeComponent();            
            this.lopHocControl = lopHocControl;
            this.lopDTO = lopDTO;
            deThiBLL = new DeThiBLL();
            monHocBLL = new MonHocBLL();
            giaoDeThiBLL = new GiaoDeThiBLL();
            phanCongBLL = new PhanCongBLL();
            ketQuaBLL = new KetQuaBLL();
            nguoiDungBLL = new NguoiDungBLL();
            chiTietDeBLL = new ChiTietDeBLL();
            RenderInfoLop();
        }
        public void RenderDeThi()
        {
            listDeThi = deThiBLL.GetAllDeThiCuaLop(lopDTO);
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in listDeThi)
            {
                DeThiDTO dt = deThiBLL.GetById(item);
                CreatePanel(dt);
            }
        }
        public void RenderInfoLop()
        {
            lblMaMoi.Text = lopDTO.MaMoi;
            lblTenLop.Text = lopDTO.TenLop + "   ";
            lblTenGV.Text = nguoiDungBLL.getUserLoginById(lopDTO.MaGV).HoTen;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            fDanhSachDeThi f = new fDanhSachDeThi(this, lopDTO);
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
            Label clickedLabel = (Label)sender;
            string labelText = clickedLabel.Text;

            // Sao chép nội dung của Label vào Clipboard
            Clipboard.SetText(labelText);
            MessageBox.Show("Đã sao chép: " + labelText);
        }
        private System.Drawing.Color GetRandomColor()
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
            return System.Drawing.Color.FromArgb(r, g, b);
        }

        private void CreatePanel(DeThiDTO deThi)
        {
            Panel panelContain = new Panel
            {
                Location = new Point(3, 3),
                Name = "panelContain" +counter.ToString(),
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
                Font = new System.Drawing.Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(10, 9),
                Name = "lblTenDeThi" +counter.ToString(),
                Size = new System.Drawing.Size(300, 200),
                TabIndex = 0,
                Text = deThi.TenDe,
                AutoEllipsis = true
            };
            toolTip.SetToolTip(lblTenDeThi, lblTenDeThi.Text);




            Label lblMonHoc = new Label
            {
                AutoSize = true,
                Location = new Point(20, 190),
                Name = "lblMonHoc1" + counter,
                Size = new System.Drawing.Size(110, 13),
                TabIndex = 1,
                Text = "Môn học: " + monHocBLL.GetMonHocById(deThi.MaMonHoc).TenMonHoc,
                Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular)

            };
            TimeSpan thoiGiamLamBai = deThi.ThoiGianKetThuc.Subtract(deThi.ThoiGianBatDau);
            Label lblThoiGianLamBai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 220),
                Name = "lblThoiGianLamBai" + counter,
                Size = new System.Drawing.Size(140, 13),
                TabIndex = 2,
                Text = "Thời gian làm bài: " + thoiGiamLamBai + " phút",
                Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular)

            };

            Label lblTrangThai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 250),
                Name = "lblMonHoc1" + counter,
                Size = new System.Drawing.Size(110, 13),
                TabIndex = 1,
                Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular)

            };
            TimeSpan khoangThoiGian = deThi.ThoiGianKetThuc - DateTime.Now;
			string thoiGianText = "";

			if (khoangThoiGian.Days > 0)
			{
				thoiGianText = $"Còn {khoangThoiGian.Days} ngày";
			}
			else if (khoangThoiGian.Hours > 0)
			{
				thoiGianText = $"Còn {khoangThoiGian.Hours} giờ {khoangThoiGian.Minutes} phút";
			}
			else if (khoangThoiGian.Minutes > 0)
			{
				thoiGianText = $"Còn {khoangThoiGian.Minutes} phút";
			}
			else
			{
				thoiGianText = "Hết hạn";
				//dtclBus.DeleteByMaLopAndMaDeThi(lop.MaLop, baiThi.MaDeThi);
				deThi.TrangThai = 0;
			}
			lblTrangThai.Text = deThi.TrangThai == 1 ? $"Trạng thái: Đang mở ({thoiGianText})" : "Trạng thái: Đã đóng";
			KetQuaDTO ketQua = ketQuaBLL.Get(deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);

            System.Windows.Forms.Button btnLamBai = new System.Windows.Forms.Button
            {
                Location = new Point(10, 300),
                Name = "button2" + counter,
                Size = new System.Drawing.Size(120, 41),
                TabIndex = 2,
                Text = "Làm bài thi",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
                Enabled = deThi.TrangThai == 0 ? false : true,
            };

            
            System.Windows.Forms.Button btnXemKq = new System.Windows.Forms.Button
            {
                Location = new Point(145, 300),
                Name = "button2" + counter,
                Size = new System.Drawing.Size(120, 41),
                TabIndex = 2,
                Text = "Mở đáp án",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
                Visible = fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Sinh viên") ? true : false,
            };

            System.Windows.Forms.Button btnDong = new System.Windows.Forms.Button
            {
                Location = new Point(280, 300),
                Name = "button2" + counter,
                Size = new System.Drawing.Size(100, 41),
                TabIndex = 2,
                Text = "Đóng",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter // Đặt văn bản ở giữa theo cả hai chiều
            };
            btnXemKq.Click += (s, ev) =>
            {
                btnXemKq_Click(s, ev,  deThi);
            };
            btnDong.Click += (s, ev) =>
            {
                btnDong_Click(s, ev, deThi, lopDTO);
            };
            btnLamBai.Click += (s, ev) =>
            {
                btnLamBai_Click(s, ev, deThi, lopDTO);
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
        private void btnXemKq_Click(object s, EventArgs ev, DeThiDTO obj)
        {
            //xemkq
            KetQuaDTO kq = ketQuaBLL.Get(obj.MaDe,fDangNhap.nguoiDungDTO.MaNguoiDung);
            if (kq != null)
            {
                fKetQua f = new fKetQua(obj, lopDTO, kq);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa làm bài thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamBai_Click(object s, EventArgs ev, DeThiDTO obj, LopDTO lop)
        {
            // thực hiện chức năng mở đề thi khi de thi dang dong
            if (obj.TrangThai == 0)
            {
                fThemDeThiCuaLop f = new fThemDeThiCuaLop(obj, lop, this, "edit");
                f.ShowDialog();
            }
            else // thực hiện chức năng làm bài
            {
                List<CauHoiDTO> listCauHoi = chiTietDeBLL.GetAllCauHoiOfDeThi(obj);
                // check xem trong đề thi có câu hỏi không
                if (listCauHoi.Count > 0)
                {
                    TimeSpan khoangThoiGian = obj.ThoiGianKetThuc - DateTime.Now;

                    // check xem đề thi có đang trong thời gian làm bài không
                    if (khoangThoiGian <= TimeSpan.Zero)
                    {
                        deThiBLL.DeleteByMaDeThi(lop, obj);
                        MessageBox.Show("Đã quá hạn làm bài thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RenderDeThi();
                    }
                    else
                    {
                        KetQuaDTO kq = ketQuaBLL.Get(obj.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);
                        // Check xem người dùng đã làm bài thi này chưa (chỉ check trong trường hợp người dùng là học sinh)
                        if (kq != null && fDangNhap.nhomQuyenDTO.TenQuyen.Equals("Sinh viên"))
                        {
                            MessageBox.Show("Bạn đã làm bài thi này rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            fBaiThi formBaithi = new fBaiThi(obj, lop, this);
                            formBaithi.ShowDialog();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Không thể làm bài (Giáo viên chưa thêm câu hỏi vào đề)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDong_Click(object s, EventArgs ev, DeThiDTO obj,LopDTO lop)
        {
            //donng
            deThiBLL.DeleteByMaDeThi(lop, obj);
            RenderDeThi();
        }
        //private void CreatePanelSV()
        //{
        //    Panel panelContain = new Panel
        //    {
        //        Location = new Point(3, 3),
        //        Name = "panelContain",
        //        Size = new Size(390, 350),
        //        TabIndex = 0,
        //        BorderStyle = BorderStyle.FixedSingle,
        //        Margin = new Padding(10, 10, 10, 10),
        //    };

        //    Panel panelHead = new Panel
        //    {
        //        Location = new Point(0, 0),
        //        Name = "panelHead",
        //        Size = new Size(390, 290),
        //        TabIndex = 1,
        //        BackColor = GetRandomColor()
        //    };

        //    Label lblTenDeThi = new Label
        //    {
        //        AutoSize = false,
        //        Font = new System.Drawing.Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
        //        Location = new Point(10, 9),
        //        Name = "lblTenDeThi",
        //        Size = new Size(300, 200),
        //        TabIndex = 0,
        //        Text = "Đề thi môn ABCxzy",
        //        AutoEllipsis = true
        //    };
        //    toolTip.SetToolTip(lblTenDeThi, lblTenDeThi.Text);



        //    Label lblMonHoc = new Label
        //    {
        //        AutoSize = true,
        //        Location = new Point(20, 220),
        //        Name = "lblMonHoc1",
        //        Size = new Size(110, 13),
        //        TabIndex = 1,
        //        Text = "Môn học: AAA",
        //        Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular)

        //    };

        //    Label lblThoiGianLamBai = new Label
        //    {
        //        AutoSize = true,
        //        Location = new Point(20, 250),
        //        Name = "lblThoiGianLamBai",
        //        Size = new Size(140, 13),
        //        TabIndex = 2,
        //        Text = "Thời gian làm bài: 60 phút",
        //        Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular)

        //    };

        //    System.Windows.Forms.Button btnLamBai = new System.Windows.Forms.Button
        //    {
        //        Location = new Point(10, 300),
        //        Name = "button2",
        //        Size = new Size(120, 41),
        //        TabIndex = 2,
        //        Text = "Làm bài thi",
        //        UseVisualStyleBackColor = true,
        //        Cursor = System.Windows.Forms.Cursors.Hand,
        //        Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
        //        TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
        //        Enabled = true,
        //    };
        //    System.Windows.Forms.Button btnXemDa = new System.Windows.Forms.Button
        //    {
        //        Location = new Point(135, 300),
        //        Name = "button2",
        //        Size = new Size(120, 41),
        //        TabIndex = 2,
        //        Text = "Kết quả",
        //        UseVisualStyleBackColor = true,
        //        Cursor = System.Windows.Forms.Cursors.Hand,
        //        Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
        //        TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
        //        Visible = true,
        //    };
        //    System.Windows.Forms.Button btnXemLai = new System.Windows.Forms.Button
        //    {
        //        Location = new Point(260, 300),
        //        Name = "button2",
        //        Size = new Size(120, 41),
        //        TabIndex = 2,
        //        Text = "Xem lại",
        //        UseVisualStyleBackColor = true,
        //        Cursor = System.Windows.Forms.Cursors.Hand,
        //        Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
        //        TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
        //        Enabled = true,
        //        Visible = true,
        //    };
        //    panelHead.Controls.AddRange(new Control[] { lblThoiGianLamBai, lblMonHoc, lblTenDeThi, });

        //    panelContain.Location = new Point(20, flowLayoutPanel1.Controls.Count * 150);
        //    flowLayoutPanel1.Controls.Add(panelContain);
        //    panelContain.Controls.AddRange(new Control[] { btnLamBai, panelHead, btnXemDa, btnXemLai });

        //    flowLayoutPanel1.AutoScroll = true;
        //}
        private void btnImport_Click(object sender, EventArgs e)
        {
            fThemSVvaoLop fAddSV = new fThemSVvaoLop();
            fAddSV.ShowDialog();
        }
    }
}
