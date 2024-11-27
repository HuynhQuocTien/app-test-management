using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
namespace GUI.LopHoc
{
    public partial class fThemDeThiCuaLop : Form
    {
        private int maMonHoc = 1, maLop = 1;
        private long maNguoiDung = 3121410497;
        GiaoDeThiBLL giaoDeThiBLL = new GiaoDeThiBLL();
        DeThiBLL deThiBLL = new DeThiBLL();
        List<int> maMonHocList = new List<int>();
        public fThemDeThiCuaLop(int MaMonHoc, int MaLop, int MaNguoiDung)
        {
            //maMonHoc= MaMonHoc;
            //maLop= MaLop;
            //maNguoiDung = MaNguoiDung;
        }
        public fThemDeThiCuaLop()
        {
            InitializeComponent();
            xemCbbMonHoc();
            //DeThiChuaThem();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeThiChuaThem();
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            // Kiểm tra nếu có một mục được chọn
            if (listBox1.SelectedItem is DeThiDTO selectedItem)
            {
                //MessageBox.Show("Tên: " + selectedItem.MaDe + "\nMô tả: " + selectedItem.TenDe );
                MessageBox.Show("Mục được chọn: " + selectedItem);
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            themDeThi();
        }

        public void xemCbbMonHoc()
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

        public void DeThiChuaThem()
        {
            DeThiDTO de = new DeThiDTO();
            if (cbMonHoc.SelectedIndex == 0)
            {
                de.MaMonHoc = 0;
            }
            else
                de.MaMonHoc = maMonHocList[cbMonHoc.SelectedIndex - 1];
            //de.MaMonHoc = cbMonHoc.SelectedIndex;
            de.TenDe = txtTenDe.Text;
            listBox1.Items.Clear();
            List<DeThiDTO> DeThiList = giaoDeThiBLL.GetDeThiChuaThem(de);
            foreach (DeThiDTO item in DeThiList)
            {
                //listBox1.Items.Add($"Mã: {item.MaDe} | Tên: {item.TenDe} | TG Tạo: {item.ThoiGianTao}");
                listBox1.Items.Add(item);
            }
        }

        public void themDeThi()
        {
            GiaoDeThiDTO giaoDeThiDTO = new GiaoDeThiDTO();
            DeThiDTO selectedItem = listBox1.SelectedItem as DeThiDTO;

            if (selectedItem != null)
            {
                giaoDeThiDTO.MaDe = selectedItem.MaDe;
                giaoDeThiDTO.MaLop = maLop;
                giaoDeThiDTO.NguoiGiao = maNguoiDung;
                giaoDeThiDTO.IsDelete = 0;
                giaoDeThiDTO.TrangThai = 0;
                if (giaoDeThiBLL.Add(giaoDeThiDTO) == true)
                {
                    MessageBox.Show("Đã Giao. Mã Đề: " + selectedItem.MaDe + " Tên Đề: " + selectedItem.TenDe + " Thời Gian Bắt Đầu: " + selectedItem.ThoiGianBatDau);
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
        //public void DeThiDaThem()
        //{
        //    DeThiDTO de = new DeThiDTO;
        //}

    }
}
