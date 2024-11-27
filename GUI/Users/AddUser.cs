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

namespace GUI.Users
{
    public partial class AddUser : Form
    {
        private UsersControl usersControl;

        MonHocBLL monHocBLL;
        DeThiBLL deThiBLL;
        DeThiBLL NguoiDungBLL;
        DeThiBLL TaiKhoanBLL;
        private string hanhDong;
        private DeThiDTO deThiUpdate;
        NhomQuyenBLL nhomQuyenBLL;
        public AddUser(UsersControl usersControl)
        {
            InitializeComponent();
            this.usersControl = usersControl;



            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
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
            MessageBox.Show("Meo1");

            //THem tu flie


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
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Meo");

            string selectedRBGender = string.Empty;
            string txtIDValue = textBoxID.Text;
            string txtNameValue = textBoxName.Text;
            string txtEmailValue = textBoxEmail.Text;

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
            if (!long.TryParse(txtIDValue, out long maNguoiDung))
            {
                MessageBox.Show("ID không hợp lệ. Vui lòng nhập một số nguyên.");
                return;
            }

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

            usersControl.AddNguoiDung(nguoiDungAdd, taikhoanAdd);







            //if (hanhDong.Equals("add"))
            //{
            //    try
            //    {
            //        string txtTendeValue = txtTenDeThi.Text;
            //        string nudValue = nud.Text;
            //        MonHocDTO cbMonHocValue = (MonHocDTO)cbMonHoc.SelectedItem;


            //        DeThiDTO deThiAdd = new DeThiDTO(deThiBLL.GetAutoIncrement(), cbMonHocValue.MaMonHoc,
            //            txtTendeValue, DateTime.Now, DateTime.Now, DateTime.Now.AddMinutes(Convert.ToInt32(nud.Text)),
            //            fDangNhap.nguoiDungDTO.MaNguoiDung, 1, 0, cbMonHocValue.TenMonHoc);
            //        deThiControl.AddDeThi(deThiAdd);
            //        this.Close();
            //        this.Dispose();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}


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
                    System.Windows.Forms.MessageBox.Show("import Thành công");
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("import Thất bại");
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            usersControl.renderAfterEdit();
        }
    }
}
