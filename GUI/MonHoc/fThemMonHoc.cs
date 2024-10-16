using BLL;
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

namespace GUI.MonHoc
{
    public partial class fThemMonHoc : Form
    {
        private MonHocControl monHocControl;
        private string chucNang;
        private MonHocDTO monHocDTO;
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
                monHocControl.render();
                clearForm();
            }
        }
        private void themMonHoc()
        {
            int maMonHoc = 0;
            string tenMonHoc = txtTenMonHoc.Text;
            int soTinChi = int.Parse(textBox1.Text);
            int soTietLT = int.Parse(textBox2.Text);
            int soTietTH = int.Parse(textBox3.Text);
            int trangThai = checkBox1.Checked ? 1: 0;
            int trangThaiXoa = 0;
            monHocDTO = new MonHocDTO(maMonHoc, tenMonHoc, soTinChi, soTietLT, soTietTH, trangThai, trangThaiXoa);
            MonHocBLL monHocBLL = new MonHocBLL();
            monHocBLL.Add(monHocDTO);
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
