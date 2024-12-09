using BLL;
using DAL;
using DocumentFormat.OpenXml.Wordprocessing;
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
        int currentPage = 1;
        int pageSize = 10;
        CauHoiBLL cauhoiBLL;

        List<CauHoiDTO> filteredData;

        public CauHoiControl()
        {
            cauhoiBLL = new CauHoiBLL();
            InitializeComponent();
            filteredData = cauhoiBLL.GetAll(fDangNhap.taiKhoanDTO.Username);
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
            int startRecord = (pageNumber - 1) * recordsPerPage > 0 ? (pageNumber - 1) * recordsPerPage : 0 ;

            // Tải dữ liệu từ cơ sở dữ liệu hoặc danh sách, lấy các bản ghi từ startRecord đến startRecord + recordsPerPage
            // Ví dụ:
            DataTable pageData = cauhoiBLL.GetDataForPage(startRecord, recordsPerPage, fDangNhap.taiKhoanDTO.Username) ?? null;

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
            dataGridView1.DataSource = cauhoiBLL.GetAll(fDangNhap.taiKhoanDTO.Username);
            Allrecord = dataGridView1.RowCount;
            LoadPage(1, 10);
        }

        private void loadDataComboBoxMHView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            var monHocList = monHocBLL.GetFromPhanCong(fDangNhap.nguoiDungDTO.MaNguoiDung);
            monHocList.Insert(0, new MonHocDTO{ MaMonHoc = -1, TenMonHoc = "Chọn môn học" });
            comboBox1.DataSource = monHocList;
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
            if((fDangNhap.nguoiDungDTO.MaNguoiDung != this.cauHoiDTO.MaNguoiTao) && !fDangNhap.nhomQuyenDTO.TenQuyen.Equals("Admin"))
            {
                MessageBox.Show($"Bạn không có quyền sửa câu hỏi của người {this.cauHoiDTO.MaNguoiTao} tạo");
                return;
            }
            switch (LoaiCauHoi)
            {
                case "Trắc nghiệm":
                    fCauHoiTN fCauHoiTN = new fCauHoiTN(this.cauHoiDTO, 0);
                    fCauHoiTN.Show();
                    fCauHoiTN.FormClosed += (s, args) => {
                        render();
                        phanTrang();
                        this.numericUpDown1.Value = 1;
                    };
                    break;

                case "Điền từ":
                    fCauHoiDCT fCauHoiDCT = new fCauHoiDCT(this.cauHoiDTO, 0);
                    fCauHoiDCT.Show();
                    fCauHoiDCT.FormClosed += (s, args) => {
                        render();
                        phanTrang();
                        this.numericUpDown1.Value = 1;
                    };
                    break;
                case "Nối câu":
                    fCauHoiNoiCau fCauHoiNoiCau = new fCauHoiNoiCau(this.cauHoiDTO, 0);
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
            string LoaiCauHoi = this.cauHoiDTO.LoaiCauHoi;

            switch (LoaiCauHoi)
            {
                case "Trắc nghiệm":
                    fCauHoiTN fCauHoiTN = new fCauHoiTN(this.cauHoiDTO, 1);
                    fCauHoiTN.Show();
                    break;

                case "Điền từ":
                    fCauHoiDCT fCauHoiDCT = new fCauHoiDCT(this.cauHoiDTO, 1);
                    fCauHoiDCT.Show();
                    break;
                case "Nối câu":
                    fCauHoiNoiCau fCauHoiNoiCau = new fCauHoiNoiCau(this.cauHoiDTO, 1);
                    fCauHoiNoiCau.Show();
                    break;
                default:
                    MessageBox.Show("Loại câu hỏi không hợp lệ!");
                    break;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(cauHoiDTO == null)
            {
                MessageBox.Show($"Vui lòng chọn dòng để xoá");
                return;
            }
            if ((fDangNhap.nguoiDungDTO.MaNguoiDung != this.cauHoiDTO.MaNguoiTao) && !fDangNhap.nhomQuyenDTO.TenQuyen.Equals("Admin"))
            {
                MessageBox.Show($"Bạn không có quyền xoá câu hỏi của người {this.cauHoiDTO.MaNguoiTao} tạo");
                return;
            }
            DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn xóa câu hỏi này ?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string thongBao = cauhoiBLL.Delete(this.cauHoiDTO);
                System.Windows.Forms.MessageBox.Show(thongBao, "Thông báo");
                render();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //int pageCurrent = (int) numericUpDown1.Value;
            //int offset = (pageCurrent - 1) * 10;
            //CauHoiBLL cauhoiBLL = new CauHoiBLL();
            //dataGridView1.DataSource = cauhoiBLL.GetTimKiem(textBoxTimKiem.Text,fDangNhap.taiKhoanDTO.Username, offset);
            //Allrecord = dataGridView1.RowCount;
            //int totalPages = (int)Math.Ceiling((double) Allrecord / 10);
            //this.numericUpDown1.Minimum = 1;
            //this.numericUpDown1.Maximum = totalPages;
            //this.label2.Text = "Trên tổng " + totalPages + " trang";
            ////LoadPage(1, 10);
            ////this.numericUpDown1.Enabled = false;
            ////this.label2.Text = "Trên tổng ... trang";
            

            //int currentPage = 1;
            //int pageSize = 10;
            CauHoiBLL cauhoiBLL = new CauHoiBLL();

            string searchText = textBoxTimKiem.Text.ToLower().Trim();

            int selectedMonHoc = ((MonHocDTO)comboBox1.SelectedItem).MaMonHoc;
            // Lấy giá trị (Value) của phần tử đã chọn
            int selectedValue = (int)comboBox2.SelectedValue;
            //// Lấy toàn bộ đối tượng KeyValuePair (Key và Value)
            //KeyValuePair<string, int> selectedItem = (KeyValuePair<string, int>)comboBox2.SelectedItem;

            //// Lấy tên (Key) của phần tử đã chọn
            //string selectedKey = selectedItem.Key;

            //string selectedDoKho = selectedValue.ToString();

            List<CauHoiDTO> allData = cauhoiBLL.GetAll(fDangNhap.taiKhoanDTO.Username);
            //if (string.IsNullOrEmpty(searchText))
            //{
            //    filteredData = allData; // Hiển thị toàn bộ dữ liệu
            //}
            //else
            //{
                // Lọc dữ liệu theo các điều kiện
                filteredData = allData
                    .Where(item =>
                        (string.IsNullOrEmpty(searchText) || item.NoiDung.ToLower().Contains(searchText)) &&
                        (selectedMonHoc == 0 || item.MaMonHoc == selectedMonHoc) &&
                        (selectedValue == 0 || item.DoKho == selectedValue)) // Điều chỉnh theo thuộc tính của bạn
                    .ToList();
            //}
            // Tính tổng số trang
            int totalPages = (int)Math.Ceiling((double)filteredData.Count / pageSize);

            // Cập nhật giá trị tối đa của NumericUpDown
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = Math.Max(1, totalPages);
            numericUpDown1.Value = 1; // Đặt về trang đầu tiên

            // Hiển thị dữ liệu trang đầu tiên
            DisplayPage(1);
        }
        private void DisplayPage(int pageNumber)
        {
            // Tính toán số lượng dữ liệu trên trang hiện tại
            int skip = (pageNumber - 1) * pageSize;
            var pageData = filteredData.Skip(skip).Take(pageSize).ToList();

            // Hiển thị dữ liệu trên DataGridView
            dataGridView1.DataSource = pageData;

            // Cập nhật trạng thái số trang
            label2.Text = $"Trên tổng {Math.Ceiling((double)filteredData.Count / pageSize)} trang";        
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
                this.cauHoiDTO = new CauHoiDTO(MaCauHoi, NoiDung, LoaiCauHoi, MaMonHoc, MaNguoiTao, DoKho, trangThai, trangThaiXoa);
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
                if(((MonHocDTO) comboBox1.SelectedItem).MaMonHoc == -1)
                {
                    MessageBox.Show("Vui lòng chọn môn học cần lọc");
                    comboBox2.SelectedIndex = 0;
                    return;
                }
                //dataGridView1.DataSource = cauhoiBLL.GetTimKiemSelect(DoKho, comboBox1.SelectedValue.ToString(), fDangNhap.taiKhoanDTO.Username);
                ////this.numericUpDown1.Enabled = false;
                //this.label2.Text = "Trên tổng ... trang";
                // Lấy giá trị trang hiện tại
                int selectedPage = (int)numericUpDown1.Value;

                // Hiển thị dữ liệu cho trang được chọn
                DisplayPage(selectedPage);
            }

        }
    }
}