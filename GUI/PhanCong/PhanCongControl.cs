using GUI.MonHoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.PhanCong
{
    public partial class PhanCongControl : UserControl
    {
        public PhanCongControl()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fAddPhanCong fthemPhanCong = new fAddPhanCong();
            fthemPhanCong.ShowDialog();
        }
    }
}
