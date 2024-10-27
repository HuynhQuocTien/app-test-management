using BLL;
using DAL;
using DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excell = Microsoft.Office.Interop.Excel;

namespace GUI.CauHoi
{
    public partial class CauHoiControl : UserControl
    {
        private CauHoiDTO cauHoiDTO;

        public CauHoiControl()
        {
            InitializeComponent();
            render();
        }

        private void render()
        {
            loadDataComboBoxMHView();
            loadDataGridView();
            styleDataGridView();
        }

        private void styleDataGridView()
        {
            dataGridView1.Columns["MaCauHoi"].HeaderText = "Mã câu hỏi";
            dataGridView1.Columns["LoaiCauHoi"].HeaderText = "Loại câu hỏi";
            dataGridView1.Columns["NoiDung"].HeaderText = "Nội dung";
            dataGridView1.Columns["MaMonHoc"].HeaderText = "Mã môn học";
            dataGridView1.Columns["MaNguoiTao"].HeaderText = "Mã người tạo";
            dataGridView1.Columns["DoKho"].HeaderText = "Độ khó";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";
            dataGridView1.Columns["is_delete"].HeaderText = "Trạng thái xóa";
        }
        private void loadDataGridView()
        {
            CauHoiBLL cauhoiBLL = new CauHoiBLL();
            dataGridView1.DataSource = cauhoiBLL.GetAll();
        }

        private void loadDataComboBoxMHView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            comboBox1.DataSource = monHocBLL.GetAll();
            comboBox1.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBox1.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)
        }


        private void exportExcell(string path)
        {
            Excell.Application application = new Excell.Application();
            application.Application.Workbooks.Add(Type.Missing);
            // Tiêu đề cột
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                application.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
            }
            // Dòng
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
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
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Câu Hỏi";
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

        private void importExcell(string path)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Thiết lập context phi thương mại
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                CauHoiBLL cauhoiBLL = new CauHoiBLL();
                // Thêm các cột vào DataTable từ dòng đầu tiên của Excel (tên cột)
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString());
                }

                // Duyệt qua các hàng trong Excel, bắt đầu từ hàng thứ 2
                for (int row = excelWorksheet.Dimension.Start.Row + 1; row <= excelWorksheet.Dimension.End.Row; row++)
                {
                    List<string> listRow = new List<string>();
                    CauHoiDTO cauhoi = new CauHoiDTO(); // Tạo đối tượng mới cho mỗi hàng

                    // Lấy thuộc tính của MonHocDTO bằng Reflection
                    PropertyInfo[] properties = typeof(CauHoiDTO).GetProperties();

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
                                property.SetValue(cauhoi, valueToSet); // Gán giá trị cho thuộc tính của MonHocDTO
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
                    string tb = cauhoiBLL.Import(cauhoi);

                }

                // Gán DataTable cho DataGridView
                dataGridView1.DataSource = dt;
            }
        }

        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Câu Hỏi";
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
                    // Hiển thị thông báo lỗi chi tiết để dễ dàng xác định nguyên nhân
                    System.Windows.Forms.MessageBox.Show("Import thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            render();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fThemCauHoi fThemCauHoi = new fThemCauHoi();
            fThemCauHoi.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string LoaiCauHoi = this.cauHoiDTO.LoaiCauHoi;

            switch (LoaiCauHoi)
            {
                case "Trắc nghiệm":
                    fCauHoiTN fCauHoiTN = new fCauHoiTN(this.cauHoiDTO);
                    fCauHoiTN.Show();
                    break;

                case "Điền từ":
                    fCauHoiDCT fCauHoiDCT = new fCauHoiDCT(this.cauHoiDTO);
                    fCauHoiDCT.Show();
                    break;
                case "Nối câu":
                     fCauHoiNoiCau fCauHoiNoiCau=new fCauHoiNoiCau(this.cauHoiDTO);
                    fCauHoiNoiCau.Show();
                    break;
                default:
                    MessageBox.Show("Loại câu hỏi không hợp lệ!");
                    break;
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn xóa câu hỏi này ?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                CauHoiBLL cauHoiBLL = new CauHoiBLL();
                string thongBao = cauHoiBLL.Delete(this.cauHoiDTO);
                System.Windows.Forms.MessageBox.Show(thongBao, "Thông báo");
                render();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int MaCauHoi = int.Parse(selectedRow.Cells["MaCauHoi"].Value.ToString());
                string NoiDung = selectedRow.Cells["NoiDung"].Value.ToString();
                string LoaiCauHoi = selectedRow.Cells["LoaiCauHoi"].Value.ToString();
                int MaMonHoc = int.Parse(selectedRow.Cells["MaMonHoc"].Value.ToString());
                long MaNguoiTao = long.Parse(selectedRow.Cells["MaNguoiTao"].Value.ToString());
                string DoKho = selectedRow.Cells["DoKho"].Value.ToString();
                int trangThai = int.Parse(selectedRow.Cells["TrangThai"].Value.ToString());
                int trangThaiXoa = int.Parse(selectedRow.Cells["is_delete"].Value.ToString());
                this.cauHoiDTO = new CauHoiDTO(MaCauHoi, NoiDung, LoaiCauHoi, MaMonHoc, MaNguoiTao, DoKho, trangThai, trangThaiXoa);
            }
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
            render();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
