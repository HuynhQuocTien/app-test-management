using BLL;
using DAL;
using DocumentFormat.OpenXml.Spreadsheet;
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
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Excell = Microsoft.Office.Interop.Excel;

namespace GUI.Users
{
    public partial class UsersControl : UserControl
    {
        private Panel[] panelUser;
        private PictureBox[] avatarImg;
        private Button[] buttonCT;
        private Button[] buttonDELETE;
        private TextBox[] textBoxDate;
        private TextBox[] textBoxRole;
        private TextBox[] textBoxName;
        NguoiDungBLL nguoiDungBLL;
        TaiKhoanDAL taiKhoanDAL;
        private List<NguoiDungDTO> getListNguoiDung()
        {
            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            return nguoiDungBLL.GetAllNguoiDung(); 
        }


        private List<TaiKhoanDTO> getListTaiKhoan()
        {
            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            return taiKhoanDAL.GetAll();
        }

        public UsersControl()
        {
            InitializeComponent();
            renderUsers();
        }
        private List<NguoiDungDTO> getListNguoiDung()
        {
            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            return nguoiDungBLL.GetAllNguoiDung();
        }
        private void Delete_MouseClick(object sender, MouseEventArgs e, NguoiDungDTO nguoiDung)
        {
            if (MessageBox.Show("Bạn có muốn xóa người dùng không?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
                if (nguoiDungBLL.Delete(nguoiDung))
                {
                    MessageBox.Show("Xóa người dùng thành công.");
                    renderUsers();
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thất bại.");
                }
            }
        }
        private void Detail_MouseClick(object sender, MouseEventArgs e, NguoiDungDTO nguoiDung)
        {
            // InfoUser infoNguoiDung = new InfoUser(nguoiDung);
            //InfoUser infoNguoiDung = new InfoUser(nguoiDung);
            InfoUser infoNguoiDung = new InfoUser();
            infoNguoiDung.getNguoiDungId(nguoiDung);
            infoNguoiDung.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser(this);
            addUser.ShowDialog();
        }
        public void AddNguoiDung(NguoiDungDTO objND, TaiKhoanDTO objTK)
        {
            //MessageBox.Show("Meo");
            MessageBox.Show(objND.ToString());
            MessageBox.Show(objTK.ToString());
            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            nguoiDungBLL.Add(objND);
            taiKhoanDAL.Add(objTK);
            renderUsers();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        public void AddNguoiDung(NguoiDungDTO objND, TaiKhoanDTO objTK)
        {

            //MessageBox.Show("Meo");
            //MessageBox.Show(objND.ToString());

            //MessageBox.Show(objTK.Username.ToString);

            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            nguoiDungBLL.Add(objND);
            taiKhoanDAL.Add(objTK);
            renderUsers();
        }




        private void renderUsers()
        {
            flowLayoutContainer.Controls.Clear();

            var nguoiDungs = getListNguoiDung();
            int nguoiDungCount = nguoiDungs.Count;


            panelUser = new Panel[nguoiDungCount];
            buttonCT = new Button[nguoiDungCount];
            buttonDELETE = new Button[nguoiDungCount];
            textBoxDate = new TextBox[nguoiDungCount];
            textBoxRole = new TextBox[nguoiDungCount];
            textBoxName = new TextBox[nguoiDungCount];
            avatarImg = new PictureBox[nguoiDungCount];

            for (int i = 0; i < nguoiDungCount; i++)
            {
                var nguoiDung = nguoiDungs[i];

                panelUser[i] = new Panel();
                panelUser[i].Name = "panelUser" + i;
                panelUser[i].Size = new Size(385, 150);
                panelUser[i].BorderStyle = BorderStyle.FixedSingle;
                // 
                // buttonCT
                // 
                buttonCT[i] = new Button();
                buttonCT[i].Location = new Point(202, 105);
                buttonCT[i].Name = "buttonCT" + i;
                buttonCT[i].Size = new Size(75, 30);
                buttonCT[i].Tag = "id1";
                buttonCT[i].Text = "Chi tiết";
                buttonCT[i].UseVisualStyleBackColor = true;
                //buttonCT[i].MouseClick += Detail_MouseClick;
                buttonCT[i].MouseClick += (sender, e) => Detail_MouseClick(sender, e, nguoiDung);

                buttonCT[i].Cursor = Cursors.Hand;
                // 
                // buttonDELETE
                // 
                buttonDELETE[i] = new Button();
                buttonDELETE[i].Location = new Point(283, 105);
                buttonDELETE[i].Name = "buttonDELETE" + i;
                buttonDELETE[i].Size = new Size(75, 30);
                buttonDELETE[i].Text = "Xóa";
                buttonDELETE[i].Tag = "id1";
                buttonDELETE[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonDELETE[i].UseVisualStyleBackColor = true;
                //buttonDELETE[i].MouseClick += Delete_MouseClick;
                buttonDELETE[i].MouseClick += (sender, e) => Delete_MouseClick(sender, e, nguoiDung);
                buttonDELETE[i].Cursor = Cursors.Hand;



                // 
                // textBoxDate
                // 
                textBoxDate[i] = new TextBox();
                textBoxDate[i].BackColor = SystemColors.Control;
                textBoxDate[i].Location = new Point(149, 76);
                textBoxDate[i].Name = "textBoxDate" + i;
                textBoxDate[i].Size = new Size(209, 23);
                textBoxDate[i].Text = nguoiDung.NgaySinh.ToString("dd/MM/yyyy");
                textBoxDate[i].Enabled = false;
                // 
                // textBoxRole
                // 
                textBoxRole[i] = new TextBox();
                textBoxRole[i].BackColor = SystemColors.Control;
                textBoxRole[i].Location = new Point(149, 47);
                textBoxRole[i].Name = "textBoxRole" + i;
                textBoxRole[i].Size = new Size(209, 23);
                textBoxRole[i].Enabled = false;
                textBoxRole[i].Text += "Admin";


                // 
                // textBoxName
                // 
                textBoxName[i] = new TextBox();
                textBoxName[i].BackColor = SystemColors.Control;
                textBoxName[i].Location = new Point(149, 18);
                textBoxName[i].Name = "textBoxName" + i;
                textBoxName[i].Size = new Size(209, 23);
                textBoxName[i].Text = nguoiDung.HoTen;
                textBoxName[i].Enabled = false;
                // 
                // avatarImg
                // 
                avatarImg[i] = new PictureBox();
                //avatarImg[i].ImageLocation = "Link avt";
                avatarImg[i].Location = new Point(22, 18);
                avatarImg[i].Name = "avatarImg" + i;
                avatarImg[i].Size = new Size(100, 113);
                avatarImg[i].TabStop = false;
                avatarImg[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                avatarImg[i].ImageLocation = nguoiDung.Avatar;


                panelUser[i].Controls.Add(buttonCT[i]);
                panelUser[i].Controls.Add(buttonDELETE[i]);
                panelUser[i].Controls.Add(textBoxDate[i]);
                panelUser[i].Controls.Add(textBoxRole[i]);
                panelUser[i].Controls.Add(textBoxName[i]);
                panelUser[i].Controls.Add(avatarImg[i]);
                flowLayoutContainer.Controls.Add(panelUser[i]);

            }



        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void exportExcell(string path)
        {
            Excell.Application application = new Excell.Application();
            application.Application.Workbooks.Add(Type.Missing);
            // Tiêu đề cột

            // Create the header row
            application.Cells[1, 1] = "Mã người dùng";
            application.Cells[1, 2] = "Tên";
            application.Cells[1, 3] = "Giới Tính";
            application.Cells[1, 4] = "Ngày Sinh";
            application.Cells[1, 5] = "Avatar";
            application.Cells[1, 6] = "SDT";
            application.Cells[1, 7] = "Ngày Tạo";
            application.Cells[1, 8] = "Trạng Thái";
            application.Cells[1, 9] = "Trạng Thái Xóa";
            application.Cells[1, 10] = "Username";
            application.Cells[1, 11] = "Password";
            application.Cells[1, 12] = "Email";
            application.Cells[1, 13] = "Mã quyền";
            application.Cells[1, 14] = "Trạng thái";

            // Get the list of users
            var nguoiDungs = getListNguoiDung();
            var taiKhoans = getListTaiKhoan();

            // Fill the data rows

            for (int i = 0; i < nguoiDungs.Count; i++)
            {
                var nguoiDung = nguoiDungs[i];
                var taiKhoan = taiKhoans.FirstOrDefault(tk => tk.Username == nguoiDung.MaNguoiDung);


                application.Cells[i + 2, 1] = nguoiDung.MaNguoiDung;
                application.Cells[i + 2, 2] = nguoiDung.HoTen;
                application.Cells[i + 2, 3] = nguoiDung.GioiTinh;
                application.Cells[i + 2, 4] = nguoiDung.NgaySinh;
                application.Cells[i + 2, 5] = nguoiDung.Avatar;
                application.Cells[i + 2, 6] = nguoiDung.SDT;
                application.Cells[i + 2, 7] = nguoiDung.NgayTao;
                application.Cells[i + 2, 8] = nguoiDung.TrangThai;
                application.Cells[i + 2, 9] = nguoiDung.is_delete;

                if (taiKhoan != null)
                {
                    application.Cells[i+ 2, 10] = taiKhoan.Username; 
                    application.Cells[i+2, 11] = taiKhoan.Password;
                    application.Cells[i + 2, 12] = taiKhoan.Email; 
                    application.Cells[i + 2, 13] = taiKhoan.MaNhomQuyen;
                    application.Cells[i + 2, 14] = taiKhoan.TrangThai;
        
                }
                else
                {
                    application.Cells[i +2, 10] = "N/A";
                    application.Cells[i +2, 11] = "N/A";
                    application.Cells[i + 2, 12] = "N/A";
                    application.Cells[i + 2, 13] = "N/A";
                    application.Cells[i + 2, 14] = "N/A";
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

        private void button3_Click(object sender, EventArgs e)
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
            renderUsers();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Export
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Người Dùng";
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
    }
}
