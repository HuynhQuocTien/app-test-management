using DTO;
using GUI.LopHoc;
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
    public partial class fBaiThi : Form
    {
        private DeThiDTO deThi;
        private LopDTO lop;
        private fChiTietLop fChiTietLop;
        public fBaiThi(DeThiDTO deThi,LopDTO lop, fChiTietLop fChiTietLop)
        {
            InitializeComponent();
            this.deThi = deThi;
            this.lop = lop;
            this.fChiTietLop = fChiTietLop;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {          
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
        }
        private void Baithi_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void Baithi_Load(object sender, EventArgs e)
        {

        }

    }
}
