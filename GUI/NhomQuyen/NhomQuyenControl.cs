using GUI.MonHoc;
using GUI.PhanCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.NhomQuyen
{
    public partial class NhomQuyenControl : UserControl
    {
        public NhomQuyenControl()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            AddNhomQuyen fthemNhomQuyen = new AddNhomQuyen();
            fthemNhomQuyen.ShowDialog();
        }
    }
}
