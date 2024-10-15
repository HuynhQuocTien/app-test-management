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
        public fNhapInfo()
        {
            InitializeComponent();
        }
        private void btnGui_Click(object sender, EventArgs e)
        {
            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            string email = txtEmailorUsername.Text;
            string thongBao = nguoiDungBLL.kiemTraEmailNguoiDung(email);
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
