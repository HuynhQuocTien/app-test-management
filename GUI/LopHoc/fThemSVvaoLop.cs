using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace GUI.LopHoc
{
    public partial class fThemSVvaoLop : Form
    {
        private LopHocControl lopHocControl;

        private ErrorProvider errorProvider = new ErrorProvider();

        private LopDTO lop;

        private List<ChiTietLopDTO> chiTietLopDTOs;
        private List<NguoiDungDTO> usersOfLop;

        private ChiTietLopBLL chiTietLopBLL;
        private LopBLL lopBLL;
        private NguoiDungBLL nguoiDungBLL;
        private TaiKhoanBLL taiKhoanBLL;
        public fThemSVvaoLop(LopHocControl lopHocControl, LopDTO lop)
        {
            lopBLL = new LopBLL();
            chiTietLopBLL = new ChiTietLopBLL();
            nguoiDungBLL = new NguoiDungBLL();
            taiKhoanBLL = new TaiKhoanBLL();

            InitializeComponent();

            this.lopHocControl = lopHocControl;
            this.lop = lop;
            usersOfLop = chiTietLopBLL.GetSV(lop.MaLop);
        }
        private void ClearInput()
        {
            textBoxName.Text = string.Empty;
            textBoxID.Text = string.Empty;  
            textBoxEmail.Text = string.Empty;
            rbNam.Checked = false;
            RbNu.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
            textBoxSDT.Text = string.Empty;

        }
        private void textBoxID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                     errorProvider.SetError(textBoxID, string.Empty);
                    // Check if student exists
                    long maSV = long.Parse(textBoxID.Text.Trim());
                    // Check if student is already in class
                    bool IsStudentInClass = chiTietLopBLL.IsStudentInClass(maSV,lop.MaLop);
                    if (!IsStudentInClass)
                    {
                        NguoiDungDTO nguoiDungDTO = new NguoiDungDTO();
                        nguoiDungDTO = nguoiDungBLL.getUserLoginById(maSV);
                        TaiKhoanDTO taiKhoanDTO = new TaiKhoanDTO();
                        taiKhoanDTO = taiKhoanBLL.getTaiKhoanById(maSV);
                        textBoxID.Text = nguoiDungDTO.MaNguoiDung.ToString();
                        textBoxName.Text = nguoiDungDTO.HoTen;
                        textBoxEmail.Text = taiKhoanDTO.Email;
                        rbNam.Checked = nguoiDungDTO.GioiTinh == 1 ? true : false;
                        RbNu.Checked = nguoiDungDTO.GioiTinh == 0 ? true : false; ;
                        dateTimePicker1.Value = nguoiDungDTO.NgaySinh;
                        textBoxSDT.Text = nguoiDungDTO.SDT;
                    }
                    else
                    {
                        errorProvider.SetError(textBoxID, "Sinh viên này không tồn tại");
                        ClearInput();

                    }
                    // Add student to class
                }
                catch (FormatException)
                {
                    // Nếu lỗi định dạng
                    errorProvider.SetError(textBoxID, "Mã sinh viên phải là số.");
                    ClearInput();

                }
                catch (Exception ex)
                {
                    // Lỗi khác
                    errorProvider.SetError(textBoxID, "Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider.SetError(textBoxID, string.Empty);

                // Kiểm tra mã sinh viên
                if (string.IsNullOrWhiteSpace(textBoxID.Text))
                {
                    errorProvider.SetError(textBoxID, "Mã sinh viên không được để trống.");
                    return;
                }

                long maSV = long.Parse(textBoxID.Text.Trim());

                // Kiểm tra sinh viên có trong lớp chưa
                bool isStudentInClass = chiTietLopBLL.IsStudentInClass(maSV, lop.MaLop);
                if (isStudentInClass)
                {
                    MessageBox.Show("Sinh viên đã tồn tại trong lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm sinh viên vào lớp
                ChiTietLopDTO chiTietLop = new ChiTietLopDTO
                {
                    MaLop = lop.MaLop,
                    MaSV = maSV,
                    is_delete = 0
                };

                bool isAdded = chiTietLopBLL.Add(chiTietLop);
                if (isAdded)
                {
                    MessageBox.Show("Thêm sinh viên vào lớp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Xóa dữ liệu nhập sau khi thêm thành công
                    ClearInput();
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên vào lớp thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                errorProvider.SetError(textBoxID, "Mã sinh viên phải là số.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            string username = txtTkSL.Text.Trim();
            string password = txtMkSl.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation Error");
                return;
            }

            if (fDangNhap.taiKhoanDTO.Username.ToString().Equals(username) && fDangNhap.taiKhoanDTO.Password.Equals(password) )
            {
                if (string.IsNullOrEmpty(label1.Text))
                {
                    MessageBox.Show("Please select an Excel file first.", "File Error");
                    return;
                }

                try
                {
                    DataTable dataTable = ReadExcel(label1.Text);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        ImportStudentsToClass(dataTable);
                        MessageBox.Show("Students imported successfully!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("The Excel file is empty or invalid.", "Data Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while importing: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Authentication Failed");
            }
        }
        private void ImportStudentsToClass(DataTable dataTable)
        {
            int successCount = 0, failCount = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    // Giả sử file Excel có cột "MaSV"
                    long maSV = long.Parse(row["MaSV"].ToString().Trim());
                    int isDelete = int.Parse(row["is_Delete"].ToString().Trim());
                    // Kiểm tra nếu sinh viên đã tồn tại trong lớp
                    if (chiTietLopBLL.IsStudentInClass(maSV, lop.MaLop))
                    {
                        failCount++;
                        continue;
                    }

                    // Thêm sinh viên vào lớp
                    ChiTietLopDTO chiTietLop = new ChiTietLopDTO
                    {
                        MaLop = lop.MaLop,
                        MaSV = maSV,
                        is_delete = 0
                    };

                    bool isAdded = chiTietLopBLL.Add(chiTietLop);
                    if (isAdded) successCount++;
                    else failCount++;
                }
                catch
                {
                    failCount++;
                }
            }

            //MessageBox.Show($"Import hoàn tất.\nThành công: {successCount}\nThất bại: {failCount}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DataTable ReadExcel(string filePath)
        {
            // Set EPPlus license context (for EPPlus version 5+)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Open the Excel file using EPPlus
            using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(filePath)))
            {
                // Access the first worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                DataTable table = new DataTable();

                // Add columns to the DataTable from the worksheet's first row (headers)
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    table.Columns.Add(worksheet.Cells[1, col].Text.Trim()); // Ensure header is trimmed
                }

                // Iterate through the rows in the worksheet, starting from the second row (ignoring header)
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    // Skip empty rows (if all cells are empty)
                    bool isEmptyRow = true;
                    DataRow dataRow = table.NewRow();

                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Value;

                        // If the cell value is null, set it to an empty string
                        string cellText = cellValue != null ? cellValue.ToString().Trim() : string.Empty;

                        // If any cell in the row is not empty, mark the row as non-empty
                        if (!string.IsNullOrEmpty(cellText))
                        {
                            isEmptyRow = false;
                        }

                        // Set the value in the DataRow
                        dataRow[col - 1] = cellText;
                    }

                    // Only add non-empty rows
                    if (!isEmptyRow)
                    {
                        table.Rows.Add(dataRow);
                    }
                }

                return table;
            }
        }


        private void buttonImportUsers_Click(object sender, EventArgs e)
        {
            label1.Text = string.Empty;
            label1.Visible = false;

            // Chọn file Excel
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog.FileName;
                label1.Visible = true;
                //MessageBox.Show("File selected: " + selectedFilePath, "File Import");
            }
        }
    }
}
