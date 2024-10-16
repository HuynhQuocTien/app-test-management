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
using System.IO;
using OfficeOpenXml;
using Excell = Microsoft.Office.Interop.Excel;

namespace GUI.MonHoc
{
    public partial class MonHocControl : UserControl
    {
        private MonHocDTO monHocDTO;
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
            fThemMonHoc themMonHoc = new fThemMonHoc(this, chucNang);
            themMonHoc.Show();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string chungNang = "Update";
            fThemMonHoc suaMonHoc = new fThemMonHoc(this, monHocDTO, chungNang);
            suaMonHoc.Show();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int maMonHoc = int.Parse(selectedRow.Cells["MaMonHoc"].Value.ToString());
                string tenMonHoc = selectedRow.Cells["TenMonHoc"].Value.ToString();
                int soTC = int.Parse(selectedRow.Cells["SoTC"].Value.ToString());
                int soTietLT = int.Parse(selectedRow.Cells["SoTietLT"].Value.ToString());
                int soTietTH = int.Parse(selectedRow.Cells["SoTietTH"].Value.ToString());
                int trangThai = int.Parse(selectedRow.Cells["TrangThai"].Value.ToString());
                int trangThaiXoa = int.Parse(selectedRow.Cells["is_delete"].Value.ToString());
                this.monHocDTO = new MonHocDTO(maMonHoc, tenMonHoc, soTC, soTietLT, soTietTH, trangThai, trangThaiXoa);
            }
        }

        private void importExcell(string path)
        {

        }
        private void exportExcell(string path)
        {
            Excell.Application application = new Excell.Application();
            application.Application.Workbooks.Add(Type.Missing);
            // Tiêu đề cột
            for(int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                application.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
            }
            // Dòng
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for(int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    application.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            // style
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(path);
            application.ActiveWorkbook.Saved = true;

        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            render();
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
           
        }
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Môn học";
            // Đuôi file
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    exportExcell(saveFileDialog.FileName);
                    MessageBox.Show("Export Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export Thất bại");
                }
            }
        }
        private void textBoxTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
