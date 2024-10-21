using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.CauHoi
{
    public partial class CauHoiControl : UserControl
    {
        public CauHoiControl()
        {
            InitializeComponent();
            render();
        }

        private void render()
        {
            loadDataComboBoxMHView();
        }

        private void loadDataComboBoxMHView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            comboBox1.DataSource = monHocBLL.GetAll();
            comboBox1.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBox1.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
           
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fThemCauHoi fThemCauHoi = new fThemCauHoi();
            fThemCauHoi.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
           
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void comboBoxMonHoc_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxDoKho_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
