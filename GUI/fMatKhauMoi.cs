using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class fMatKhauMoi : Form
    {
        private string email;
        TaiKhoanBLL taiKhoanBLL;
        public fMatKhauMoi(string email)
        {
            taiKhoanBLL = new TaiKhoanBLL();
            InitializeComponent();
            this.email = email;
            // display email
            lblEmail.Text = email;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //txtNhapMK.Text
            //txtXacNhan.Text
            TaiKhoanDTO taiKhoanDTO = taiKhoanBLL.getTaiKhoanByEmail(email) ?? new TaiKhoanDTO();
            if (txtNhapMK.Text.Equals(txtXacNhan.Text))
            {
                MessageBox.Show("Mật khẩu xác nhận không giống nhau!", "Cảnh báo", MessageBoxButtons.OK);
                return;
            }
            if (taiKhoanDTO.Password.Equals(txtXacNhan.Text)){
                MessageBox.Show("Mật khẩu trùng với mật khẩu cũ!", "Cảnh báo", MessageBoxButtons.OK);
                return;

            }
            string thongBao = taiKhoanBLL.suaMatKhauNguoiDung(email, txtNhapMK.Text, txtXacNhan.Text);
            MessageBox.Show("Thay đổi thành công", "Thông báo", MessageBoxButtons.OK);
            if (thongBao.Equals("Oke"))
            {
                this.Close();
            }
        }
        private void txtNhapMK_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtXacNhan_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
