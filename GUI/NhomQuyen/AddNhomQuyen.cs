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
    public partial class AddNhomQuyen : Form
    {
        public AddNhomQuyen()
        {
            InitializeComponent();
        }

        private void thamgiathi_CheckedChanged(object sender, EventArgs e)
        {
            if (thamgiathi.Checked)
            {
                thamgiaday.Checked = false;
            }
        }

        private void thamgiaday_CheckedChanged(object sender, EventArgs e)
        {
            if (thamgiaday.Checked)
            {
                thamgiathi.Checked = false;
            }
        }
    }
}
