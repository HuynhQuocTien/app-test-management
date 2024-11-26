using BLL;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace GUI
{
    public partial class fKetQua : Form
    {
        private DeThiDTO deThi;
        private LopDTO lop;
        private KetQuaDTO ketQua;

        private NguoiDungBLL nguoiDungBLL;
        private MonHocBLL monHocBLL;
        private ChiTietDeBLL chiTietDeBLL;
        private int socauchuachon;

        public fKetQua(DeThiDTO deThi, LopDTO lop,KetQuaDTO ketQua)
        {
            InitializeComponent();
            this.deThi = deThi;
            this.lop = lop;
            this.ketQua = ketQua;
            nguoiDungBLL = new NguoiDungBLL();
            monHocBLL = new MonHocBLL();
            chiTietDeBLL = new ChiTietDeBLL();
            this.socauchuachon= chiTietDeBLL.CountSoCauHoi(deThi) -  (ketQua.SoCauDung + ketQua.SoCauSai);
            load();
        }

        void load()
        {
            lblTTenBaiThi.Text = deThi.TenDe;
            lblTenThiSinh.Text = fDangNhap.nguoiDungDTO.HoTen;
            lblTenLop.Text = lop.TenLop;
            lblTenMonHoc.Text = monHocBLL.GetMonHocById(deThi.MaMonHoc).TenMonHoc;
            lblBoQua.Text = socauchuachon.ToString();
            lblDung.Text = ketQua.SoCauDung.ToString();
            lblSai.Text = ketQua.SoCauSai.ToString();
            lblDiem.Text = ketQua.Diem.ToString();
            if (ketQua.Diem == 0) lblDiem.Text = "0.00";
            if (ketQua.Diem == 1) lblDiem.Text = "1.00";
            if (ketQua.Diem == 2) lblDiem.Text = "2.00";
            if (ketQua.Diem == 3) lblDiem.Text = "3.00";
            if (ketQua.Diem == 4) lblDiem.Text = "4.00";
            if (ketQua.Diem == 5) lblDiem.Text = "5.00";
            if (ketQua.Diem == 6) lblDiem.Text = "6.00";
            if (ketQua.Diem == 7) lblDiem.Text = "7.00";
            if (ketQua.Diem == 8) lblDiem.Text = "8.00";
            if (ketQua.Diem == 9) lblDiem.Text = "9.00";
            if (ketQua.Diem == 10) lblDiem.Text = "10.00";

        }

        private void lblTTenBaiThi_Click(object sender, EventArgs e)
        {

        }
    }
}
