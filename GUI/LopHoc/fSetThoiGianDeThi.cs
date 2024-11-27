using BLL;
using DAL;
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
    public partial class fSetThoiGianDeThi : Form
    {
        private DeThiDTO deThi;
        private LopDTO lop;
        private fChiTietLop fCTL;
        private fDanhSachDeThi fDSDT;
        private string hanhDong;
        private DeThiBLL deThiBLL;
        private GiaoDeThiBLL giaoDeThiBLL;
        public fSetThoiGianDeThi(DeThiDTO deThi, LopDTO lop,fChiTietLop fCTL, fDanhSachDeThi fDSDT = null, string hanhDong = null)
        {
            InitializeComponent();
            deThiBLL = new DeThiBLL();
            giaoDeThiBLL = new GiaoDeThiBLL();
            this.deThi = deThi;
            this.lop = lop;
            this.fCTL = fCTL;
            this.fDSDT = fDSDT;
            this.hanhDong = hanhDong;
            dtpThoiGianBatDau.Format = DateTimePickerFormat.Custom;
            dtpThoiGianBatDau.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpThoiGianKetThuc.Format = DateTimePickerFormat.Custom;
            dtpThoiGianKetThuc.CustomFormat = "dd/MM/yyyy HH:mm";
        }
        public fSetThoiGianDeThi(DeThiDTO deThi, LopDTO lop, fChiTietLop fCTL, string hd = null)
        {
            InitializeComponent();
            deThiBLL = new DeThiBLL();
            this.deThi = deThi;
            this.lop = lop;
            this.fCTL = fCTL;
            this.hanhDong = hd;
            dtpThoiGianBatDau.Value = DateTime.Now;
            dtpThoiGianBatDau.Format = DateTimePickerFormat.Custom;
            dtpThoiGianBatDau.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpThoiGianKetThuc.Format = DateTimePickerFormat.Custom;
            dtpThoiGianKetThuc.CustomFormat = "dd/MM/yyyy HH:mm";
        }
        bool checkValidate()
        {
            if (dtpThoiGianBatDau.Value == null)
            {
                MessageBox.Show("Bạn chưa chọn thời gian bắt đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpThoiGianKetThuc.Value == null)
            {
                MessageBox.Show("Bạn chưa chọn thời gian kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra xem dtpThoiGianKetThuc phải lớn hơn dtpThoiGianBatDau
            if (dtpThoiGianKetThuc.Value.CompareTo(dtpThoiGianBatDau.Value) <= 0)
            {
                MessageBox.Show("Thời gian kết thúc phải lớn hơn thời gian bất đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Trả về false nếu dtpThoiGianKetThuc nhỏ hơn hoặc bằng dtpThoiGianBatDau
                return false;
            }

            //// Kiểm tra xem dtpThoiGianBatDau phải lớn hơn thời gian hiện tại
            //if (dtpThoiGianBatDau.Value.CompareTo(DateTime.Now) <= 0)
            //{
            //	MessageBox.Show("Thời gian bất đầu phải lớn hơn thời gian hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //	// Trả về false nếu dtpThoiGianBatDau nhỏ hơn hoặc bằng thời gian hiện tại
            //	return false;
            //}
            if (deThiBLL.checkDeThiCoTrongLop(deThi.MaDe, lop.MaLop) && hanhDong.Equals("add"))
            {
                MessageBox.Show("Đề thi đã có trong lớp rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (hanhDong.Equals("add"))
            {
                if (checkValidate())
                {
                    try
                    {
                        //DeThi obj = new DeThi(deThiBLL.GetAutoIncrement(), deThiDTO.MaDeThi, lopDTO.MaLop, dtpThoiGianBatDau.Value, dtpThoiGianKetThuc.Value, 1);
                        deThi.ThoiGianBatDau = dtpThoiGianBatDau.Value;
                        deThi.ThoiGianKetThuc = dtpThoiGianKetThuc.Value;
                        deThi.TrangThai = 0;
                        if (deThiBLL.Update(deThi))
                        {
                            GiaoDeThiDTO giaoDeThi = new GiaoDeThiDTO(lop.MaLop, deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, 0);
                            giaoDeThiBLL.Add(giaoDeThi);
                        }
                        MessageBox.Show("Thêm đề thi vào lớp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fCTL.RenderDeThi();
                        this.Dispose();
                        this.Close();
                        fDSDT.Dispose();
                        fDSDT.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        MessageBox.Show("Thêm đề thi vào lớp thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else if (hanhDong.Equals("edit"))
            {
                if (checkValidate())
                {
                    try
                    {
                        deThi.ThoiGianBatDau = dtpThoiGianBatDau.Value;
                        deThi.ThoiGianKetThuc = dtpThoiGianKetThuc.Value;
                        deThi.TrangThai = 0;
                        if (deThiBLL.Update(deThi))
                        {
                            deThiBLL.UpdateTrangThaiKQByMaDe(deThi);
                            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        fCTL.RenderDeThi();
                        this.Dispose();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
