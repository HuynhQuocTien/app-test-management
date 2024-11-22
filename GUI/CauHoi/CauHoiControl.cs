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
        private int Allrecord;
        public CauHoiControl()
        {
            InitializeComponent();
            List<KeyValuePair<string, int>> items = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Chọn độ khó", 0),
                new KeyValuePair<string, int>("Dễ", 1),
                new KeyValuePair<string, int>("Trung bình", 2),
                new KeyValuePair<string, int>("Khó", 3)
            };
            this.comboBox2.DataSource = items;        // Gán dữ liệu vào ComboBox
            this.comboBox2.DisplayMember = "Key";    // Thuộc tính hiển thị
            this.comboBox2.ValueMember = "Value";    // Thuộc tính giá trị
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            render();
            phanTrang();
        }
        public void phanTrang()
        {
            int totalRecords = Allrecord;  // Tổng số bản ghi

            int recordsPerPage = 10; // Số bản ghi trên mỗi trang
            int totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
            this.numericUpDown1.Enabled = true;
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = totalPages;
            this.label2.Text = "Trên tổng " + totalPages + " trang";
            // Sự kiện khi thay đổi trang
            this.numericUpDown1.ValueChanged += (sender, e) =>
            {
                int selectedPage = (int)numericUpDown1.Value;
                LoadPage(selectedPage, recordsPerPage);
            };
        }

        private void LoadPage(int pageNumber, int recordsPerPage)
        {
            int startRecord = (pageNumber - 1) * recordsPerPage;

            // Tải dữ liệu từ cơ sở dữ liệu hoặc danh sách, lấy các bản ghi từ startRecord đến startRecord + recordsPerPage
            // Ví dụ:
            CauHoiBLL cauhoiBLL = new CauHoiBLL();
            DataTable pageData = cauhoiBLL.GetDataForPage(startRecord, recordsPerPage, fDangNhap.taiKhoanDTO.Username);

            dataGridView1.DataSource = pageData;
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
            dataGridView1.DataSource = cauhoiBLL.GetAll(fDangNhap.taiKhoanDTO.Username);
            Allrecord = dataGridView1.RowCount;
            LoadPage(1, 10);
        }

        private void loadDataComboBoxMHView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            comboBox1.DataSource = monHocBLL.GetFromPhanCong(fDangNhap.nguoiDungDTO.MaNguoiDung);
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

        //private void importExcell(string path)
        //{
        //    // Định nghĩa ánh xạ cột - tên thuộc tính
        //    Dictionary<int, string> columnPropertyMapping = new Dictionary<int, string>
        //    {
        //        { 1, "NoiDung" },
        //        { 2, "MaMonHoc" },
        //        { 3, "DoKho" },
        //        {4, "LoaiCauHoi" }
        //    };
        //    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Thiết lập context phi thương mại
        //    using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
        //    {
        //        ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
        //        DataTable dt = new DataTable();
        //        CauHoiBLL cauhoiBLL = new CauHoiBLL();
        //        // Thêm các cột vào DataTable từ dòng đầu tiên của Excel (tên cột)
        //        for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
        //        {
        //            dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString());
        //        }

        //        // Duyệt qua các hàng trong Excel, bắt đầu từ hàng thứ 2
        //        for (int row = excelWorksheet.Dimension.Start.Row + 1; row <= excelWorksheet.Dimension.End.Row; row++)
        //        {
        //            List<string> listRow = new List<string>();
        //            CauHoiDTO cauhoi = new CauHoiDTO(); // Tạo đối tượng mới cho mỗi hàng

        //            // Lấy thuộc tính của MonHocDTO bằng Reflection
        //            PropertyInfo[] properties = typeof(CauHoiDTO).GetProperties();

        //            // Duyệt qua từng cột trong một hàng
        //            for (int col = excelWorksheet.Dimension.Start.Column; col <= excelWorksheet.Dimension.End.Column; col++)
        //            {
        //                try
        //                {
        //                    if (columnPropertyMapping.ContainsKey(col))
        //                    {
        //                        string propertyName = columnPropertyMapping[col];
        //                        PropertyInfo property = typeof(CauHoiDTO).GetProperty(propertyName);
        //                        //string cellValue = excelWorksheet.Cells[row, col].Value?.ToString();
        //                        if (property != null)
        //                        {
        //                            string cellValue = excelWorksheet.Cells[row, col].Value?.ToString();
        //                            if (cellValue != null)
        //                            {
        //                                object valueToSet = Convert.ChangeType(cellValue, property.PropertyType);
        //                                property.SetValue(cauhoi, valueToSet);
        //                            }
        //                        }
        //                    }
        //                }
        //                catch (InvalidCastException)
        //                {
        //                    Console.WriteLine($"Không thể chuyển đổi giá trị '{excelWorksheet.Cells[row, col].Value}' sang kiểu '{properties[col - 1].PropertyType.Name}'.");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"Lỗi khi gán giá trị cho {properties[col - 1].Name}: {ex.Message}");
        //                }
        //            }

        //            // Thêm đối tượng monHoc vào DataTable
        //            //dt.Rows.Add(monHoc.MaMonHoc, monHoc.TenMonHoc, monHoc.SoTC, monHoc.SoTietLT, monHoc.SoTietTH, monHoc.TrangThai, monHoc.is_delete);
        //            // Thiết lập các giá trị mặc định
        //            cauhoi.TrangThai = 1; // Mặc định trạng thái là 1
        //            cauhoi.is_delete = 0; // Mặc định is_delete là 0
        //            cauhoi.MaNguoiTao = fDangNhap.nguoiDungDTO.MaNguoiDung; // Mặc định người tạo là người đang đăng nhập
        //            // Add vào database
        //            if (String.IsNullOrEmpty(cauhoi.NoiDung))
        //            {
        //                importTracNghiem(cauhoi, excelPackage.Workbook.Worksheets[1]);
        //            }
        //        }

        //        render();
        //        phanTrang();
        //        this.numericUpDown1.Value = 1;
        //    }
        //}
        private void importExcell(string path)
        {
            // Định nghĩa ánh xạ cột - tên thuộc tính
            Dictionary<int, string> columnPropertyMapping = new Dictionary<int, string>
        {
            {1, "MaCauHoi"},
            { 2, "NoiDung" },
            { 3, "MaMonHoc" },
            { 4, "DoKho" },
            { 5, "LoaiCauHoi" }
        };
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Thiết lập context phi thương mại
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet mainSheet = excelPackage.Workbook.Worksheets[0];
                ExcelWorksheet tracNghiemSheet = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet dienTuSheet = excelPackage.Workbook.Worksheets[2];
                ExcelWorksheet noiTuSheet = excelPackage.Workbook.Worksheets[3];

                // Kiểm tra tiêu đề cột trong Excel
                for (int col = mainSheet.Dimension.Start.Column; col <= mainSheet.Dimension.End.Column; col++)
                {
                    string expectedColumn = columnPropertyMapping[col];
                    string actualColumn = mainSheet.Cells[1, col].Value?.ToString()?.Trim();

                    if (actualColumn == null || !actualColumn.Equals(expectedColumn, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"File Excel không hợp lệ. Cột '{expectedColumn}' tại vị trí {col} không khớp với dữ liệu trong Excel.", "Lỗi Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Dừng quá trình import
                    }
                }

                CauHoiBLL cauhoiBLL = new CauHoiBLL();

                // Duyệt qua các hàng trong sheet chính
                for (int row = mainSheet.Dimension.Start.Row + 1; row <= mainSheet.Dimension.End.Row; row++)
                {
                    CauHoiDTO cauhoi = new CauHoiDTO(); // Tạo đối tượng mới cho mỗi hàng

                    // Duyệt qua từng cột trong một hàng
                    for (int col = mainSheet.Dimension.Start.Column; col <= mainSheet.Dimension.End.Column; col++)
                    {
                        if (columnPropertyMapping.ContainsKey(col))
                        {
                            string propertyName = columnPropertyMapping[col];
                            PropertyInfo property = typeof(CauHoiDTO).GetProperty(propertyName);
                            if (property != null)
                            {
                                string cellValue = mainSheet.Cells[row, col].Value?.ToString();
                                if (cellValue != null)
                                {
                                    object valueToSet = Convert.ChangeType(cellValue, property.PropertyType);
                                    property.SetValue(cauhoi, valueToSet);
                                }
                            }
                        }
                    }
                    cauhoi.MaCauHoi = cauhoiBLL.GetAutoIncrement();
                    cauhoi.MaNguoiTao = fDangNhap.nguoiDungDTO.MaNguoiDung;
                    cauhoi.TrangThai = 1;
                    cauhoi.is_delete = 0;
                    string a = cauhoiBLL.Import(cauhoi);

                    // Xử lý thêm câu trả lời tùy theo loại câu hỏi
                    string loaiCauHoi = cauhoi.LoaiCauHoi?.ToLower();
                    switch (loaiCauHoi)
                    {
                        case "trắc nghiệm":
                            importTracNghiem(cauhoi, tracNghiemSheet);
                            break;
                        case "điền từ":
                            importDienTu(cauhoi, dienTuSheet);
                            break;
                        case "nối từ":
                            //importNoiTu(cauhoi, noiTuSheet, row);
                            break;
                        default:
                            Console.WriteLine($"Loại câu hỏi không hợp lệ: {cauhoi.LoaiCauHoi}");
                            break;
                    }
                }

                render();
                phanTrang();
                this.numericUpDown1.Value = 1;
            }
        }

        private void importTracNghiem(CauHoiDTO cauhoi, ExcelWorksheet sheet)
        {
            Dictionary<int, string> columnPropertyMapping = new Dictionary<int, string>
            {
                { 1, "MaCauHoi" },
                { 2, "NoiDung" },
                { 3, "IsDapAn" }
            };
            CauTraLoiBLL cauTraLoiBLL = new CauTraLoiBLL();
            CauHoiBLL cauHoiBLL = new CauHoiBLL();
            //for (int col = sheet.Dimension.Start.Column + 1; col < sheet.Dimension.End.Column; col++)
            //{
            //    string cauTraLoi = sheet.Cells[row, col].Value?.ToString();
            //    if (!string.IsNullOrEmpty(cauTraLoi))
            //    {
            //        string DapAn = sheet.Cells[row, col + 1].Value?.ToString();
            //        CauTraLoiDTO cauTraLoiDTO = new CauTraLoiDTO
            //        {
            //            MaCauHoi = cauhoi.MaCauHoi,
            //            NoiDung = cauTraLoi,
            //            IsDapAn = Convert.ToInt32(DapAn)
            //        };
            //        cauTraLoiBLL.Add(cauTraLoiDTO);
            //    }
            //}
            CauTraLoiDTO cauTraLoiDTO = new CauTraLoiDTO(); // Tạo đối tượng mới cho mỗi hàng
            for (int row = sheet.Dimension.Start.Row + 1; row <= sheet.Dimension.End.Row; row++)
            {

                // Duyệt qua từng cột trong một hàng
                for (int col = sheet.Dimension.Start.Column; col <= sheet.Dimension.End.Column; col++)
                {
                    if (columnPropertyMapping.ContainsKey(col))
                    {
                        string propertyName = columnPropertyMapping[col];
                        PropertyInfo property = typeof(CauTraLoiDTO).GetProperty(propertyName);
                        if (property != null)
                        {
                            string cellValue = sheet.Cells[row, col].Value?.ToString();
                            if (cellValue != null && cellValue.Equals(cauhoi.MaCauHoi))
                            {
                                object valueToSet = Convert.ChangeType(cellValue, property.PropertyType);
                                property.SetValue(cauTraLoiDTO, valueToSet);
                            }
                        }
                    }
                }
                if (cauTraLoiDTO != null)
                {
                    cauTraLoiDTO.MaCauHoi = cauhoi.MaCauHoi;
                    cauTraLoiBLL.Add(cauTraLoiDTO);

                }
            }
            

        }
        private void importDienTu(CauHoiDTO cauhoi, ExcelWorksheet sheet)
        {
            Dictionary<int, string> columnPropertyMapping = new Dictionary<int, string>
            {
                { 1, "MaCauHoi" },
                { 2, "ViTri" },
                { 3, "DapAnText" }
            };
            CauTraLoiDienChoTrongDTO cauTraLoiDTO = new CauTraLoiDienChoTrongDTO(); // Tạo đối tượng mới cho mỗi hàng
            CauTraLoiDienChoTrongBLL cauTraLoiDienChoTrongBLL = new CauTraLoiDienChoTrongBLL();

            for (int row = sheet.Dimension.Start.Row + 1; row <= sheet.Dimension.End.Row; row++)
            {
                string cauTraLoi = sheet.Cells[row, 1].Value?.ToString();
                for (int col = sheet.Dimension.Start.Column + 1; col < sheet.Dimension.End.Column; col++)
                {
                    if (columnPropertyMapping.ContainsKey(col))
                    {
                        string propertyName = columnPropertyMapping[col];
                        PropertyInfo property = typeof(CauTraLoiDienChoTrongDTO).GetProperty(propertyName);
                        if (property != null)
                        {
                            string cellValue = sheet.Cells[row, col].Value?.ToString();
                            if (cellValue != null)
                            {
                                object valueToSet = Convert.ChangeType(cellValue, property.PropertyType);
                                property.SetValue(cauTraLoiDTO, valueToSet);
                            }
                        }
                    }
                }
                if (cauTraLoiDTO != null)
                {
                    cauTraLoiDTO.MaCauHoi = cauhoi.MaCauHoi;
                    cauTraLoiDienChoTrongBLL.Add(cauTraLoiDTO);
                }
            }
        }

        //private void importNoiTu(CauHoiDTO cauhoi, ExcelWorksheet sheet, int row)
        //{
        //    NoiTuBLL noiTuBLL = new NoiTuBLL();
        //    for (int col = sheet.Dimension.Start.Column; col < sheet.Dimension.End.Column; col += 2)
        //    {
        //        string noiDungA = sheet.Cells[row, col].Value?.ToString();
        //        string noiDungB = sheet.Cells[row, col + 1].Value?.ToString();
        //        if (!string.IsNullOrEmpty(noiDungA) && !string.IsNullOrEmpty(noiDungB))
        //        {
        //            NoiTuDTO noiTuDTO = new NoiTuDTO
        //            {
        //                MaCauHoi = cauhoi.MaCauHoi,
        //                NoiDungA = noiDungA,
        //                NoiDungB = noiDungB
        //            };
        //            noiTuBLL.Import(noiTuDTO);
        //        }
        //    }
        //}
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
            fThemCauHoi.FormClosed += (s, args) => {
                render();
                phanTrang();
                this.numericUpDown1.Value = 1;
            };
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string LoaiCauHoi = this.cauHoiDTO.LoaiCauHoi;

            switch (LoaiCauHoi)
            {
                case "Trắc nghiệm":
                    fCauHoiTN fCauHoiTN = new fCauHoiTN(this.cauHoiDTO);
                    fCauHoiTN.Show();
                    fCauHoiTN.FormClosed += (s, args) => {
                        render();
                        phanTrang();
                        this.numericUpDown1.Value = 1;
                    };
                    break;

                case "Điền từ":
                    fCauHoiDCT fCauHoiDCT = new fCauHoiDCT(this.cauHoiDTO);
                    fCauHoiDCT.Show();
                    fCauHoiDCT.FormClosed += (s, args) => {
                        render();
                        phanTrang();
                        this.numericUpDown1.Value = 1;
                    };
                    break;
                case "Nối câu":
                     fCauHoiNoiCau fCauHoiNoiCau=new fCauHoiNoiCau(this.cauHoiDTO);
                    fCauHoiNoiCau.Show();
                    fCauHoiNoiCau.FormClosed += (s, args) => {
                        render();
                        phanTrang();
                        this.numericUpDown1.Value = 1;
                    };
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
            CauHoiBLL cauhoiBLL = new CauHoiBLL();
            dataGridView1.DataSource = cauhoiBLL.GetTimKiem(textBoxTimKiem.Text,fDangNhap.taiKhoanDTO.Username);
            this.numericUpDown1.Enabled = false;
            this.label2.Text = "Trên tổng ... trang";
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
                int DoKho = int.Parse(selectedRow.Cells["DoKho"].Value.ToString());
                int trangThai = int.Parse(selectedRow.Cells["TrangThai"].Value.ToString());
                int trangThaiXoa = int.Parse(selectedRow.Cells["is_delete"].Value.ToString());
                this.cauHoiDTO = new CauHoiDTO(MaCauHoi, NoiDung, LoaiCauHoi, MaMonHoc, MaNguoiTao,DoKho, trangThai, trangThaiXoa);
            }
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void textBoxTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            render(); 
            phanTrang();
            this.numericUpDown1.Value = 1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1 || comboBox2.SelectedValue == null)
                return; // Bỏ qua khi chưa chọn giá trị nào

            if (comboBox2.SelectedItem is KeyValuePair<string, int> selectedItem)
            {
                if (selectedItem.Value == 0)
                {
                    return;
                }

                int DoKho = selectedItem.Value;  // Lấy giá trị Value
                CauHoiBLL cauhoiBLL = new CauHoiBLL();
                dataGridView1.DataSource = cauhoiBLL.GetTimKiemSelect(DoKho, comboBox1.SelectedValue.ToString(), fDangNhap.taiKhoanDTO.Username);
                this.numericUpDown1.Enabled = false;
                this.label2.Text = "Trên tổng ... trang";
            }

        }
    }
}
