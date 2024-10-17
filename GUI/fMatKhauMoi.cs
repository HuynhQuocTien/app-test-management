using BLL;
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
        public fMatKhauMoi(string email)
        {
            InitializeComponent();
            this.email = email;
            // display email
            lblEmail.Text = email;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //txtNhapMK.Text
            //txtXacNhan.Text
            TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
            string thongBao = taiKhoanBLL.suaMatKhauNguoiDung(email, txtNhapMK.Text, txtXacNhan.Text);
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK);
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
