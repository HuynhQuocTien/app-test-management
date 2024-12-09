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
using DAL;
using DTO;
using OfficeOpenXml;
using System.Linq;
using System.Text.RegularExpressions;

namespace GUI.Users
{
    public partial class AddUser : Form
    {
        private UsersControl usersControl;

        MonHocBLL monHocBLL;
        DeThiBLL deThiBLL;
        NguoiDungBLL nguoiDungBLL;
        DeThiBLL TaiKhoanBLL;
        private string hanhDong;
        private DeThiDTO deThiUpdate;
        NhomQuyenBLL nhomQuyenBLL;
        public AddUser(UsersControl usersControl)
        {
            InitializeComponent();
            this.usersControl = usersControl;



            nguoiDungBLL = new NguoiDungBLL();
            TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();


            LoadQuyen();
            //LoadMonHoc();
            //deThiControl = fdethi;
            //this.hanhDong = hanhDong;
            //this.deThiUpdate = dethi;
            //deThiBLL = new DeThiBLL();
        }

        private void btnThemSl_Click(object sender, EventArgs e)
        {
           

        }
        private void LoadQuyen()
        {
            try
            {
                NhomQuyenDAL nhomQuyenDAL = new NhomQuyenDAL();
                var nhomQuyens = nhomQuyenDAL.GetAll();
                comboBox1.DataSource = nhomQuyens;
                comboBox1.DisplayMember = "TenQuyen";
                comboBox1.ValueMember = "MaNhomQuyen";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subjects: " + ex.Message);
            }
        }
        private void clear()
        {
            this.Dispose();
        }


  
        private void ButtonSubmit_Click(object sender, EventArgs e)
        { 
        }

        public bool checkIdValid(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng nhập ID.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (id.Length != 10)
            //{
            //    MessageBox.Show("Độ dài của ID là 10", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            if (id.Any(char.IsLetter))
            {
                MessageBox.Show("ID chỉ chứa kí tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;    
        }

        public bool checkHoTenValid()
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBoxName.Text.Length < 2)
            {
                MessageBox.Show("Độ dài tối thiểu của tên là 2", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBoxName.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Họ tên chỉ chứa kí tự chữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool checkEmailValid()
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                MessageBox.Show("Email không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(textBoxEmail.Text, pattern))
            {
                MessageBox.Show("Sai định dạng email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool checkPasswordValid(string pass)
        {
            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (pass.Length < 6)
            {
                MessageBox.Show("Độ dài tối thiểu của mật khẩu là 6 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool checkSdtValid()
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2.Text.Any(char.IsLetter))
            {
                MessageBox.Show("SDT chỉ chứa kí tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2.Text.Length < 10 || textBox2.Text.Length > 11)
            {
                MessageBox.Show("Độ dài của số điện thoại là 10 hoặc 11 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool checkNgaySinhValid()
        {
            DateTime selectedNgaySinh = dateTimePicker1.Value;
            DateTime currentDate = DateTime.Now;

            int age = currentDate.Year - selectedNgaySinh.Year;

            if (selectedNgaySinh > currentDate.AddYears(-age))
            {
                age--;
            }

            if (age < 3)
            {
                MessageBox.Show("Người dùng phải ít nhất 3 tuổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (checkIdValid(textBoxID.Text) && checkHoTenValid() && checkEmailValid() && checkPasswordValid(txtPass.Text) && checkSdtValid() && checkNgaySinhValid()) {
                string selectedRBGender = string.Empty;
                string txtIDValue = textBoxID.Text;
                string txtNameValue = textBoxName.Text;
                string txtEmailValue = textBoxEmail.Text;

               long.TryParse(txtIDValue, out long maNguoiDung);


                DateTime selectedNgaySinh = dateTimePicker1.Value;
                if (rbNam.Checked)
                {
                    selectedRBGender = "1";
                }
                else if (RbNu.Checked)
                {
                    selectedRBGender = "0";
                }
                string txtPassValue = txtPass.Text;
                string txtSdtValue = textBox2.Text;
                NhomQuyenDTO cbNhomQuyenValue = (NhomQuyenDTO)comboBox1.SelectedItem;

                // Attempt to parse the ID value safely


                //if (long.TryParse(txtIDValue, out long maNguoiDung))
                //{
                //    MessageBox.Show("ID không hợp lệ. Vui lòng nhập một số nguyên.");
                //    return;
                //}

                if (!int.TryParse(selectedRBGender, out int gioiTinh))
                {
                    MessageBox.Show("Giới tính không hợp lệ.");
                    return;
                }

                if (cbNhomQuyenValue == null)
                {
                    MessageBox.Show("Chọn vai trò.");
                    return;
                }
                //string selectedStatus = radioButtonStatus.Text;




                NguoiDungDTO nguoiDungAdd = new NguoiDungDTO(maNguoiDung, txtNameValue, gioiTinh, selectedNgaySinh, "avatar", txtSdtValue, DateTime.Now, 1, 0);
                TaiKhoanDTO taikhoanAdd = new TaiKhoanDTO(Convert.ToInt64(txtIDValue), txtPassValue, txtEmailValue, cbNhomQuyenValue.MaNhomQuyen, 1);

                if(nguoiDungBLL.getUserLoginById(nguoiDungAdd.MaNguoiDung) != null)
                {
                    MessageBox.Show("ID đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                usersControl.AddNguoiDung(nguoiDungAdd, taikhoanAdd);


                this.Close();
                this.Dispose();
            }


        }
        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void buttonUpImg_Click(object sender, EventArgs e)
        {

        }


        private void importExcell(string path)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;



            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];

                NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
                TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();

                // Start reading from the second row (assuming the first row is headers)
                for (int row = 2; row <= excelWorksheet.Dimension.End.Row; row++)
                {
                    NguoiDungDTO nguoiDung = new NguoiDungDTO();
                    TaiKhoanDTO taiKhoan = new TaiKhoanDTO();

                    try
                    {
                        // Read NguoiDung data


                        //MessageBox.Show(excelWorksheet.Cells[row, 1].Value?.ToString());

                        nguoiDung.MaNguoiDung = Convert.ToInt64(excelWorksheet.Cells[row, 1].Value?.ToString());
                        nguoiDung.HoTen = excelWorksheet.Cells[row, 2].Value?.ToString();
                        nguoiDung.GioiTinh = Convert.ToInt32(excelWorksheet.Cells[row, 3].Value?.ToString());
                        nguoiDung.NgaySinh = DateTime.Parse(excelWorksheet.Cells[row, 4].Value?.ToString());
                        nguoiDung.Avatar = excelWorksheet.Cells[row, 5].Value?.ToString();
                        nguoiDung.SDT = excelWorksheet.Cells[row, 6].Value?.ToString();
                        nguoiDung.NgayTao = DateTime.Parse(excelWorksheet.Cells[row, 7].Value?.ToString());
                        nguoiDung.TrangThai = Convert.ToInt32(excelWorksheet.Cells[row, 8].Value?.ToString());
                        nguoiDung.is_delete = Convert.ToInt32(excelWorksheet.Cells[row, 9].Value?.ToString());

                        // Read TaiKhoan data
                        taiKhoan.Username = Convert.ToInt64(excelWorksheet.Cells[row, 10].Value?.ToString());
                        taiKhoan.Password = excelWorksheet.Cells[row, 11].Value?.ToString();
                        taiKhoan.Email = excelWorksheet.Cells[row, 12].Value?.ToString();
                        taiKhoan.MaNhomQuyen = Convert.ToInt32(excelWorksheet.Cells[row, 13].Value?.ToString());
                        taiKhoan.TrangThai = Convert.ToInt32(excelWorksheet.Cells[row, 14].Value?.ToString());

                        // Add NguoiDung and TaiKhoan to the database
                        nguoiDungBLL.Add(nguoiDung);
                        taiKhoanDAL.Add(taiKhoan);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error importing row {row}: {ex.Message}");
                    }
                }
            }
        }
        private void buttonImportUsers_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonImportUsers_Click_1(object sender, EventArgs e)
        {
            if (checkIdValid(txtTkSL.Text) && checkPasswordValid(txtMkSl.Text))
            {
                //kt tài khoản hiện tại
                TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
                TaiKhoanDTO taiKhoanLogged = taiKhoanBLL.getTaiKhoanById(fDangNhap.nguoiDungDTO.MaNguoiDung);
                long txtTKValue = Convert.ToInt64(txtTkSL.Text);
                string txtMKValue = txtMkSl.Text;

                if (taiKhoanLogged.Username != txtTKValue || taiKhoanLogged.Password != txtMKValue)
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu, vui lòng thử lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                //Import
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Import Người Dùng";
                // Đuôi file
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        importExcell(openFileDialog.FileName);
                        System.Windows.Forms.MessageBox.Show("Import Thành công");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Import Thất bại");
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
                this.Close();
                this.Dispose();
                usersControl.renderAfterEdit();         
            }
        }
    }
}
