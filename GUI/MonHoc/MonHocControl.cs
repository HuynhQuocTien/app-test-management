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

namespace GUI.MonHoc
{
    public partial class MonHocControl : UserControl
    {
        public MonHocControl()
        {
            InitializeComponent();
            render();
        }
        public void render()
        {
            loadDataGridView();
            styleDataGridView();
        }
        private void styleDataGridView()
        {
            dataGridView1.Columns["MaMonHoc"].HeaderText = "Mã môn học";
            dataGridView1.Columns["TenMonHoc"].HeaderText = "Tên môn học";
            dataGridView1.Columns["SoTC"].HeaderText = "Số tín chỉ";
            dataGridView1.Columns["SoTietLT"].HeaderText = "Số tiết lý thuyết";
            dataGridView1.Columns["SoTietTH"].HeaderText = "Số tiết thực hành";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";
            dataGridView1.Columns["is_delete"].HeaderText = "Trạng thái xóa";

        }
        private void loadDataGridView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            dataGridView1.DataSource = monHocBLL.GetAll();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string chucNang = "Add";
            fThemMonHoc form = new fThemMonHoc(this, chucNang);
            form.Show();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
        
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
           
        }
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            
        }
        private void textBoxTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
