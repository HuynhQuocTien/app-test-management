using BLL;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using GUI.CauHoi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace GUI.DeThi
{
    public partial class fThemChiTietDeThi : Form
    {
        //private int maMonHoc = 1, maLop = 1;
        private int maDeThi = 1;
        private long maNguoiDung;
        GiaoDeThiBLL giaoDeThiBLL = new GiaoDeThiBLL();
        MonHocBLL monHocBLL = new MonHocBLL();
        ChiTietDeBLL chiTietDeThiBLL = new ChiTietDeBLL();
        List<int> maMonHocList = new List<int>();
        DeThiDTO deThi;
        public fThemChiTietDeThi(int maDe)
        {
            maDeThi = maDe;
        }
        public fThemChiTietDeThi(DeThiDTO deThi)
        {
            InitializeComponent();
            this.deThi = deThi;
            maDeThi = deThi.MaDe;
            maNguoiDung = fDangNhap.nguoiDungDTO.MaNguoiDung;
            xemCbMonHoc();
            xemCbDoKho();
            xemlbCauHoi();
            lbCauHoi.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;  // Hoặc MultiSimple
            lbDeThi.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;  // Hoặc MultiSimple
            xemldDeThi();
        }

        private void btnRightToLeft_Click_1(object sender, EventArgs e)
        {
            xoa1CauHoi();
            xemlbCauHoi();
            xemldDeThi();
        }

        private void btnLeftToRightAll_Click_1(object sender, EventArgs e)
        {
            themNhieuCauHoiVaoDe();
            xemlbCauHoi();
            xemldDeThi();
        }

        private void btnRightToLeftAll_Click_1(object sender, EventArgs e)
        {
            xoaNhieuCauHoi();
            xemlbCauHoi();
            xemldDeThi();
        }

        private void btnLeftToRight_Click_1(object sender, EventArgs e)
        {
            them1CauHoiVaoDe();
            xemlbCauHoi();
            xemldDeThi();
        }
        private void cbDoKho_SelectedValueChanged(object sender, EventArgs e)
        {
            xemlbCauHoi();
        }

        private void cbMonHoc_SelectedValueChanged(object sender, EventArgs e)
        {
            //COMBOBOX MON HOC
            xemlbCauHoi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            xemlbCauHoi();
        }
        private void lbCauHoi_DoubleClick(object sender, EventArgs e)
        {
            if (lbCauHoi.SelectedItem is CauHoiDTO selectedItem)
            {
                MessageBox.Show("Mục được chọn: " + selectedItem);
            }
        }
        private void lbDeThi_DoubleClick(object sender, EventArgs e)
        {

        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            xemCbDoKho();
            xemCbMonHoc();
            xemlbCauHoi();
            xemldDeThi();
        }
        public void xemCbMonHoc()
        {
            cbMonHoc.Items.Clear();
            cbMonHoc.Items.Add("Không Chọn");
            //List<MonHocDTO> danhSachMonHoc = themdethibll.TenMonHoc();
            List<MonHocDTO> danhSachMonHoc = giaoDeThiBLL.GetMonHoc(maNguoiDung);
            foreach (MonHocDTO monhoc in danhSachMonHoc)
            {
                maMonHocList.Add(monhoc.MaMonHoc);
                cbMonHoc.Items.Add(monhoc.TenMonHoc);
            }
            cbMonHoc.SelectedIndex = 0;
        }

        public void xemCbDoKho()
        {
            cbDoKho.Items.Clear();
            cbDoKho.Items.Add("Không Chọn");
            List<CauHoiDTO> dsDoKho = chiTietDeThiBLL.LayDoKho();
            foreach (CauHoiDTO monhoc in dsDoKho)
            {
                //maMonHocList.Add(monhoc.DoKho);
                cbDoKho.Items.Add(monhoc.DoKho);
            }
            cbDoKho.SelectedIndex = 0;
        }


        public void xemlbCauHoi()
        {
            lbCauHoi.Items.Clear();
            lbCauHoi.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            CauHoiDTO cauHoi = new CauHoiDTO();
            cbMonHoc.SelectedItem = monHocBLL.GetMonHocById(deThi.MaMonHoc).TenMonHoc;
            if (cbMonHoc.SelectedIndex == 0)
            {
                cauHoi.MaMonHoc = 0;
            }
            else
                cauHoi.MaMonHoc = maMonHocList[cbMonHoc.SelectedIndex - 1];
                
            cauHoi.NoiDung = txtNoiDung.Text;
            cauHoi.DoKho = cbDoKho.SelectedIndex;
            List<CauHoiDTO> dsCauHoi = chiTietDeThiBLL.layCauHoiChuaThem(cauHoi, maDeThi);
            if (dsCauHoi != null)
            {
               foreach (CauHoiDTO cauhoi in dsCauHoi)
               {
                   lbCauHoi.Items.Add(cauhoi);
               }
            }
            else
            {
               lbCauHoi.Items.Add("khong co du lieu");
            }
        }

        public void xemldDeThi()
        {
            lbDeThi.Items.Clear();
            List<CauHoiDTO> dsCauHoiDaThem = chiTietDeThiBLL.GetCauHoiByMaDe(maDeThi);
            if (dsCauHoiDaThem != null)
            {
                foreach (CauHoiDTO cauhoi in dsCauHoiDaThem)
                {
                    lbDeThi.Items.Add(cauhoi);
                }
            }
            else
            {
                lbDeThi.Items.Add("khong co du lieu");
            }
        }

        public void them1CauHoiVaoDe()
        {
            ChiTietDeDTO chiTietDeDTO = new ChiTietDeDTO();
            CauHoiDTO selectedItem = lbCauHoi.SelectedItem as CauHoiDTO;
            if (selectedItem != null)
            {
                chiTietDeDTO.MaCauHoi = selectedItem.MaCauHoi;
                chiTietDeDTO.MaDe = maDeThi;
                if (chiTietDeThiBLL.Add(chiTietDeDTO) == true)
                {
                    Console.WriteLine("Đã thêm câu hỏi");
                }
                else
                {
                    MessageBox.Show("Không thành công");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đề thi hợp lệ.");
            }
        }

        public void themNhieuCauHoiVaoDe()
        {
            //if (lbCauHoi.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show("Vui lòng chọn ít nhất một câu hỏi.");
            //    return;
            //}

            foreach (CauHoiDTO selectedItem in lbCauHoi.SelectedItems)
            {
                ChiTietDeDTO chiTietDeDTO = new ChiTietDeDTO
                {
                    MaCauHoi = selectedItem.MaCauHoi,
                    MaDe = maDeThi
                };

                if (chiTietDeThiBLL.Add(chiTietDeDTO))
                {
                    Console.WriteLine($"Đã thêm câu hỏi {selectedItem.MaCauHoi}");
                }
                else
                {
                    MessageBox.Show($"Không thêm được câu hỏi {selectedItem.MaCauHoi}");
                }
            }
        }

        public void xoa1CauHoi()
        {
            ChiTietDeDTO chiTietDeDTO = new ChiTietDeDTO();
            CauHoiDTO selectedItem = lbDeThi.SelectedItem as CauHoiDTO;
            if (selectedItem != null)
            {
                chiTietDeDTO.MaCauHoi = selectedItem.MaCauHoi;
                chiTietDeDTO.MaDe = maDeThi;
                if (chiTietDeThiBLL.Delete(chiTietDeDTO) == true)
                {
                    Console.WriteLine("Đã xóa Câu hỏi ra khỏi đề");
                }
                else
                {
                    MessageBox.Show("Không thành công");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đề thi hợp lệ.");
            }
        }

        public void xoaNhieuCauHoi()
        {
            foreach (CauHoiDTO selectedItem in lbDeThi.SelectedItems)
            {
                ChiTietDeDTO chiTietDeDTO = new ChiTietDeDTO
                {
                    MaCauHoi = selectedItem.MaCauHoi,
                    MaDe = maDeThi
                };
                if (chiTietDeThiBLL.Delete(chiTietDeDTO))
                {
                    Console.WriteLine($"Đã xóa câu hỏi {selectedItem.MaCauHoi}");
                }
                else
                {
                    MessageBox.Show($"Không xóa được câu hỏi {selectedItem.MaCauHoi}");
                }
            }
        }

    }

    //public class CauHoi : CauHoiDTO
    //{
    //    // Ghi đè ToString() để thay đổi cách hiển thị trong ListBox
    //    public override string ToString()
    //    {
    //        return $"Mã: " + MaCauHoi + " Độ khó: " + DoKho + " Tên:" + NoiDung + " Mã MH: " + MaMonHoc;
    //    }
    //}
}
