using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DTO;

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
    }
}
