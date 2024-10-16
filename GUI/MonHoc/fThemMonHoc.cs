using BLL;
using DocumentFormat.OpenXml.Drawing.Charts;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.MonHoc
{
    public partial class fThemMonHoc : Form
    {
        private MonHocControl monHocControl;
        private string chucNang;
        private MonHocDTO monHocDTO;

        public fThemMonHoc(MonHocControl form, MonHocDTO monHocDTO, string chucNang)
        {
            InitializeComponent();
            this.monHocControl = form;
            this.monHocDTO = monHocDTO;
            this.chucNang = chucNang;
            displayRowMonHoc();
        }
        public fThemMonHoc(MonHocControl form, string chucNang)
        {
            InitializeComponent();
            this.monHocControl = form;
            this.chucNang = chucNang;
        }
        public fThemMonHoc()
        {
            InitializeComponent();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(this.chucNang == "Add")
            {
                themMonHoc();
                clearForm();
                monHocControl.render();
            }else if(this.chucNang == "Update")
            {
                suaMonHoc();
                clearForm();
                monHocControl.render();
            }
        }

        private MonHocDTO getInfo()
        {
            int maMonHoc = 0;
            string tenMonHoc = txtTenMonHoc.Text;
            int soTinChi = int.Parse(textBox1.Text);
            int soTietLT = int.Parse(textBox2.Text);
            int soTietTH = int.Parse(textBox3.Text);
            int trangThai = checkBox1.Checked ? 1 : 0;
            int trangThaiXoa = 0;
            
            return new MonHocDTO(maMonHoc, tenMonHoc, soTinChi, soTietLT, soTietTH, trangThai, trangThaiXoa);
        }
        private void displayRowMonHoc()
        {
            txtTenMonHoc.Text = this.monHocDTO.TenMonHoc;
            textBox1.Text = this.monHocDTO.SoTC.ToString();
            textBox2.Text = this.monHocDTO.SoTietLT.ToString();
            textBox3.Text = this.monHocDTO.SoTietLT.ToString();
            checkBox1.Checked = this.monHocDTO.TrangThai == 1;
        }
        private void suaMonHoc()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            string thongBao = monHocBLL.Update(this.getInfo());
            MessageBox.Show(thongBao, "Thông báo sửa", MessageBoxButtons.OK);
        }
        private void themMonHoc()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            string thongBao = monHocBLL.Add(this.getInfo());
            MessageBox.Show(thongBao, "Thông báo thêm", MessageBoxButtons.OK);
        }
        private void clearForm()
        {
            txtTenMonHoc.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox1.Checked = false;
        }
    }
}
