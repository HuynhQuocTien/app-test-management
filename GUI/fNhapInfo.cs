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
    public partial class fNhapInfo : Form
    {
        TaiKhoanBLL taiKhoanBLL;
        public fNhapInfo()
        {
            InitializeComponent();
            taiKhoanBLL = new TaiKhoanBLL();
        }
        private void btnGui_Click(object sender, EventArgs e)
        {
             
            string email = txtEmailorUsername.Text;
            string thongBao = taiKhoanBLL.kiemTraEmailNguoiDung(email);
            if (thongBao.Equals("Oke"))
            {
                fNhapOTP form = new fNhapOTP(email);
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void txtEmailorUsername_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
