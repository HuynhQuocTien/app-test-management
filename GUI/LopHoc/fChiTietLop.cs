using BLL;
using DAL;
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
using System.Windows.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
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

        List<KetQuaDTO> listKetQua;


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
            //Tạo Đề thi giả để xem
            //DeThiDTO temp = new DeThiDTO();
            //temp.MaDe = 1;
            //DeThiDTO dt =deThiBLL.GetById(temp);
            //CreatePanel(dt);
            //CreatePanelSV();
            //End
            RenderDeThi();
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
            if(fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Học sinh"))
            {
                btnThem.Visible = false;
                btnImport.Visible = false;
            }
            else
            {
                btnThem.Visible = true;
                btnImport.Visible = true;
            }
            lblMaMoi.Text = lopDTO.MaMoi;
            lblTenLop.Text = lopDTO.TenLop + "   ";
            lblTenGV.Text = nguoiDungBLL.getUserLoginById(lopDTO.MaGV).HoTen;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            fDanhSachDeThi f = new fDanhSachDeThi(this, lopDTO);
            f.ShowDialog();
        }
        private void lblTenLop_Click(object sender, EventArgs e)
        {
            fThemLop f = new fThemLop(lopHocControl, "edit", lopDTO.MaMoi, lopDTO);
            f.ShowDialog();
        }
        private void btnXemDSSV_Click(object sender, EventArgs e)
        {
            fDanhSachSV fdsv = new fDanhSachSV(lopDTO);
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
            GiaoDeThiDTO request = new GiaoDeThiDTO
            {
                MaDe = deThi.MaDe,
                MaLop = lopDTO.MaLop
            };
            GiaoDeThiDTO giaoDeThiDTO = giaoDeThiBLL.GetByMaDeAndMaLop(request) ?? null;
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
            int soPhutLamBai = deThi.ThoiGianLamBai;
            Label lblThoiGianLamBai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 220),
                Name = "lblThoiGianLamBai" + counter,
                Size = new System.Drawing.Size(140, 13),
                TabIndex = 2,
                Text = "Thời gian làm bài: " + soPhutLamBai + " phút",
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
				deThi.TrangThai = -1;
			}
            if (deThi.TrangThai == 0 )
            {
                lblTrangThai.Text = $"Trạng thái: Đang mở ({thoiGianText})";
            }
            else if(deThi.TrangThai == 1 && giaoDeThiDTO.TrangThai == 1 || giaoDeThiDTO.TrangThai == 0)
            {
                lblTrangThai.Text = "Trạng thái: Đã đóng - Xem đáp án: Đóng";
            } else if (deThi.TrangThai == 1 && giaoDeThiDTO.TrangThai == 2)
            {
                lblTrangThai.Text = "Trạng thái: Đã đóng - Xem đáp án: Mở";
            }
            else if (deThi.TrangThai == -1)
            {
                lblTrangThai.Text = "Trạng thái: Đã đóng - Hết hạn";
            }
                //lblTrangThai.Text = deThi.TrangThai == 1 ? $"Trạng thái: Đang mở ({thoiGianText})" : "Trạng thái: Đã đóng";
			KetQuaDTO ketQua = ketQuaBLL.Get(deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);
            System.Windows.Forms.Button btnLamBai = new System.Windows.Forms.Button
            {
                Location = new Point(10, 300),
                Name = "button2" + counter,
                Size = new System.Drawing.Size(120, 41),
                TabIndex = 2,
                Text = fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Học sinh") ? "Làm bài thi" : "Mở để thi",
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
                Enabled = (giaoDeThiDTO.TrangThai == 0 || lopDTO.is_delete == 0) ? true : false, //Trang thai = 0 là được làm bài
            };




            System.Windows.Forms.Button btnXemKq = new System.Windows.Forms.Button
            {
                Location = new Point(145, 300),
                Name = "button2" + counter,
                Size = new System.Drawing.Size(120, 41),
                TabIndex = 2,
                Text = fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Học sinh") ? "Kết quả" : (giaoDeThiDTO.TrangThai == 2 ? "Đóng đáp án" : "Mở đáp án") ,
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter, // Đặt văn bản ở giữa theo cả hai chiều
                Visible = true
            };

            System.Windows.Forms.Button btnDong = new System.Windows.Forms.Button
            {
                Location = new Point(280, 300),
                Name = "button2" + counter,
                Size = new System.Drawing.Size(100, 41),
                TabIndex = 2,
                Text = fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Học sinh") ? "Xem lại" : (ketQuaBLL.checkDeThiInKetQua(deThi) ? "Đóng" : "Xoá"),
                UseVisualStyleBackColor = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                TextAlign = ContentAlignment.MiddleCenter // Đặt văn bản ở giữa theo cả hai chiều
            };
              

            btnXemKq.Click += (s, ev) =>
            {
                if (btnXemKq.Text.Contains("Kết quả"))
                {
                    KetQuaBLL ketQuaBLL = new KetQuaBLL();
                    KetQuaDTO ketQuaDTO = ketQuaBLL.Get(deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);
                    if (ketQuaDTO == null)
                    {
                        MessageBox.Show("Bạn chưa làm bài thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        fKetQua f = new fKetQua(deThi, lopDTO, ketQuaDTO);
                        f.ShowDialog();
                    }
                    
                }
                if (btnXemKq.Text.Contains("Mở đáp án") || btnXemKq.Text.Contains("Đóng đáp án"))
                {
                    if (giaoDeThiDTO == null)
                    {
                        MessageBox.Show("Lớp này chưa được phân công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    } else if(giaoDeThiDTO.TrangThai == 1 || giaoDeThiDTO.TrangThai == 0)
                    {   
                        giaoDeThiDTO.TrangThai = 2;
                        btnDongMoKQ_Click(s, ev, giaoDeThiDTO);
                        MessageBox.Show("Đề thi được mở đáp án cho lớp này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    } else if (giaoDeThiDTO.TrangThai == 2)
                    {
                        giaoDeThiDTO.TrangThai = 1;
                        btnDongMoKQ_Click(s, ev, giaoDeThiDTO);
                        MessageBox.Show("Đề thi đã được đóng đáp án cho lớp này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //btnXemKq_Click(s, ev,  deThi);
            };

            //DONE
            btnLamBai.Click += (s, ev) =>
            {
                btnLamBai_Click(s, ev, deThi, lopDTO);
            };


            btnDong.Click += (s, ev) =>
            {
                if (btnDong.Text.Contains("Xem lại"))
                {
                    if (giaoDeThiDTO == null)
                    {
                        MessageBox.Show("Lớp này chưa được phân công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (giaoDeThiDTO.TrangThai == 1)
                    {
                        MessageBox.Show("Bạn chưa được phép xem lại đáp án của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (giaoDeThiDTO.TrangThai == 2)
                    {
                        btnXemlai_Click(s, ev, deThi, lopDTO);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa được phép xem lại đáp án của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    btnDong_Click(s, ev, deThi, lopDTO);
                    return;
                }
            };
            panelHead.Controls.AddRange(new Control[] { lblThoiGianLamBai, lblMonHoc, lblTenDeThi, lblTrangThai });

            panelContain.Location = new Point(20, flowLayoutPanel1.Controls.Count * 150);
            flowLayoutPanel1.Controls.Add(panelContain);
            panelContain.Controls.AddRange(new Control[] { btnDong, panelHead, btnLamBai, btnXemKq });
            flowLayoutPanel1.AutoScroll = true;
            //btnLamBai.Enabled = true;        
            panelHead.BackColor = GetRandomColor();
            panelHead.Enabled = true;
            counter++;
        }


      


        private void btnDongMoKQ_Click(object s, EventArgs ev, GiaoDeThiDTO req)
        {
            bool isUpdated = false;
            isUpdated = giaoDeThiBLL.Update(req);
            RenderDeThi();
        }

        private void btnLamBai_Click(object s, EventArgs ev, DeThiDTO obj, LopDTO lop)
        {
            GiaoDeThiDTO request = new GiaoDeThiDTO
            {
                MaDe = obj.MaDe,
                MaLop = lopDTO.MaLop
            };
            GiaoDeThiDTO giaoDeThiDTO = giaoDeThiBLL.GetByMaDeAndMaLop(request) ?? null;
            if(giaoDeThiDTO.TrangThai == -1)
            {
                MessageBox.Show("Đề thi đã được đóng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // thực hiện chức năng mở đề thi khi de thi dang dong
            if (!fDangNhap.nhomQuyenDTO.TenQuyen.Equals("Học sinh"))
            {
                fSetThoiGianDeThi f = new fSetThoiGianDeThi(obj, lop, this, "edit");
                f.ShowDialog();
            }
            else // thực hiện chức năng làm bài
            {
                KetQuaDTO ketQuaDTO = ketQuaBLL.GetByMaDeAndMaND(obj.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);
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
                        if (kq != null && fDangNhap.nhomQuyenDTO.TenQuyen.Equals("Học sinh"))
                        {
                            if (kq.TrangThai > 0)
                            {
                                MessageBox.Show("Bạn đã làm bài thi này rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (kq.TrangThai < 0)
                            {
                                deThiBLL.DeleteByMaDeThi(lop, obj);
                                MessageBox.Show("Đã quá hạn làm bài thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RenderDeThi();
                            }
                            else if (kq.TrangThai == 0)
                            {
                                fBaiThi formBaithi = new fBaiThi(obj, lop, this);
                                formBaithi.ShowDialog();
                            }
                            
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
            Button button = (Button)s;
            if(button.Text.Contains("Đóng"))
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn đóng đề này không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dialogResult)
                {
                    bool isUpdated = deThiBLL.DeleteByMaDeThi(lop, obj);
                    if (isUpdated)
                    {
                        MessageBox.Show("Đã đóng đề thi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Đóng đề thi không thành công. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                 
            }
            else if (button.Text.Contains("Xoá"))
            {
                GiaoDeThiDTO giaoDTDelete = new GiaoDeThiDTO
                {
                    MaDe = obj.MaDe,
                    MaLop = lop.MaLop

                };
                DialogResult dialogResult =  MessageBox.Show("Bạn có chắc muốn xoá đề này không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(DialogResult.OK == dialogResult)
                {
                    bool isDeleted = giaoDeThiBLL.Delete(giaoDTDelete);
                    if (isDeleted)
                    {
                        MessageBox.Show("Đã xoá đề thi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xoá đề thi không thành công. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
            }
            else if (button.Text.Contains("Xem lại"))
            {

            }
                //donng
            RenderDeThi();
        }

        private void btnMoDapAn_Click(object s, EventArgs ev, DeThiDTO obj, LopDTO lop)
        {
            //donng
            //deThiBLL.DeleteByMaDeThi(lop, obj);
            //RenderDeThi();
        }


        private void btnXemlai_Click(object s, EventArgs ev, DeThiDTO obj, LopDTO lop)
        {

            //xemkq
            KetQuaDTO kq = ketQuaBLL.Get(obj.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);
            if (kq == null)
            {
                MessageBox.Show("Bạn chưa làm bài thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (kq.TrangThai != 2 && kq != null)
            //{
            //    MessageBox.Show("Không được quyền xem đáp án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (kq != null)
            {
                fXemdapan f = new fXemdapan(kq);
                f.ShowDialog();
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            fThemSVvaoLop fAddSV = new fThemSVvaoLop(lopHocControl,lopDTO);
            fAddSV.ShowDialog();
        }
    }
}
