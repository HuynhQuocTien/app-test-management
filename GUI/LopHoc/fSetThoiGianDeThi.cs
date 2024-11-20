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
    public partial class fSetThoiGianDeThi : Form
    {
        private DeThiDTO deThi;
        private LopDTO lop;
        private fChiTietLop fCTL;
        private fDanhSachDeThi fDSDT;
        private string hanhDong;
        private DeThiBLL deThiBLL;
        public fSetThoiGianDeThi(DeThiDTO deThi, LopDTO lop,fChiTietLop fCTL, fDanhSachDeThi fDSDT = null, string hanhDong = null)
        {
            InitializeComponent();
            deThiBLL = new DeThiBLL();
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
        private void btnLuu_Click(object sender, EventArgs e)
        {
            
        }

    }
}
