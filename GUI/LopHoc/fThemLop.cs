using DTO;
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
using GUI.MonHoc;

namespace GUI.LopHoc
{
    public partial class fThemLop : Form
    {
        private LopHocControl lopHocControl;
        private ChiTietLopBLL chiTietLopBLL;
        private string maMoi;
        public fThemLop(LopHocControl flophoc,string maMoi = null)
        {
            InitializeComponent();
            lopHocControl = flophoc;
            this.maMoi = maMoi;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkValidInput())
                {
                    LopDTO lop = new LopDTO(1, 3121410497,txtTenlop.Text.Trim(),maMoi,1,0);
                    lopHocControl.AddLop(lop);
                    //ChiTietLopDTO ctl = new ChiTietLopDTO();
                    //chiTietLopBLL.Add(ctl);
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool checkValidInput()
        {
            if (string.IsNullOrEmpty(txtTenlop.Text))
            {
                MessageBox.Show("Không được để trống tên lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
