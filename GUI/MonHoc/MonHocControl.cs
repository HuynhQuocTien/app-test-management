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
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                MonHocBLL monHocBLL = new MonHocBLL();
                // Thêm các cột vào DataTable từ dòng đầu tiên của Excel (tên cột)
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString());
                }

                // Duyệt qua các hàng trong Excel, bắt đầu từ hàng thứ 2
                for (int row = excelWorksheet.Dimension.Start.Row + 1; row <= excelWorksheet.Dimension.End.Row; row++)
                {
                    List<string> listRow = new List<string>();
                    MonHocDTO monHoc = new MonHocDTO(); // Tạo đối tượng mới cho mỗi hàng

                    // Lấy thuộc tính của MonHocDTO bằng Reflection
                    PropertyInfo[] properties = typeof(MonHocDTO).GetProperties();

                    // Duyệt qua từng cột trong một hàng
                    for (int col = excelWorksheet.Dimension.Start.Column; col <= excelWorksheet.Dimension.End.Column; col++)
                    {
                        try
                        {
                            string cellValue = excelWorksheet.Cells[row, col].Value?.ToString();
                            if (cellValue != null && col - 1 < properties.Length)
                            {
                                // Lấy thuộc tính tương ứng với cột hiện tại
                                PropertyInfo property = properties[col - 1]; // `col - 1` để đồng bộ cột với thuộc tính
                                object valueToSet = Convert.ChangeType(cellValue, property.PropertyType);
                                property.SetValue(monHoc, valueToSet); // Gán giá trị cho thuộc tính của MonHocDTO
                            }
                        }
                        catch (InvalidCastException)
                        {
                            Console.WriteLine($"Không thể chuyển đổi giá trị '{excelWorksheet.Cells[row, col].Value}' sang kiểu '{properties[col - 1].PropertyType.Name}'.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi gán giá trị cho {properties[col - 1].Name}: {ex.Message}");
                        }
                    }

                    // Thêm đối tượng monHoc vào DataTable
                    //dt.Rows.Add(monHoc.MaMonHoc, monHoc.TenMonHoc, monHoc.SoTC, monHoc.SoTietLT, monHoc.SoTietTH, monHoc.TrangThai, monHoc.is_delete);

                    // Add vào database
                    string tb = monHocBLL.Import(monHoc);

                }

                // Gán DataTable cho DataGridView
                dataGridView1.DataSource = dt;
            }
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
            application.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
            application.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
            Excell.Range headerRange = application.Rows[1];
            headerRange.Font.Bold = true;
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
                    System.Windows.Forms.MessageBox.Show("Export Thành công");
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Export Thất bại");
                }
            }
        }
        private void textBoxTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Môn học";
            // Đuôi file
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    importExcell(openFileDialog.FileName);
                    System.Windows.Forms.MessageBox.Show("import Thành công");
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("import Thất bại");
                }
            }
            render();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn xóa môn học này ?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                MonHocBLL monHocBLL = new MonHocBLL();
                string thongBao = monHocBLL.Delete(this.monHocDTO);
                System.Windows.Forms.MessageBox.Show(thongBao, "Thông báo");
                render();
            }
        }
    }
}
