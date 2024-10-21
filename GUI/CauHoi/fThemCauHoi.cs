using DAL;
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
using System.Data.SqlClient;
using BLL;

namespace GUI.CauHoi
{
    public partial class fThemCauHoi : Form
    {
        public fThemCauHoi()
        {
            InitializeComponent();
            render();
        }

        private void render()
        {
            loadDataComboBoxMHView();
        }

        private void loadDataComboBoxMHView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            comboBoxMonHoc.DataSource= monHocBLL.GetAll();
            comboBoxMonHoc.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBoxMonHoc.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)

            comboBox2.DataSource = monHocBLL.GetAll();
            comboBox2.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBox2.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)

            comboBox2.DataSource = monHocBLL.GetAll();
            comboBox2.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBox2.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)

            comboBox5.DataSource = monHocBLL.GetAll();
            comboBox5.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBox5.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
           
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void comboBoxMonHoc_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxDoKho_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cbSoDapAn_SelectedValueChanged(object sender, EventArgs e)
        {
            // Kiểm tra giá trị SelectedIndex của ComboBox
            if (cbSoDapAn.SelectedIndex == 2)
            {
                txtInputDA1.Enabled = true;
                rb1.Enabled = true;
                txtInputDA2.Enabled = true;
                rb2.Enabled = true;
                txtInputDA3.Enabled = true;
                rb3.Enabled = true;
                txtInputDA4.Enabled = true;
                rb4.Enabled = true;

            }
            else if (cbSoDapAn.SelectedIndex == 1)
            {
                txtInputDA1.Enabled = true;
                rb1.Enabled = true;
                txtInputDA2.Enabled = true;
                rb2.Enabled = true;
                txtInputDA3.Enabled = true;
                rb3.Enabled = true;
                txtInputDA4.Enabled = false;
                rb4.Enabled = false;
            }
            else if (cbSoDapAn.SelectedIndex == 0)
            {
                txtInputDA1.Enabled = true;
                rb1.Enabled = true;
                txtInputDA2.Enabled = true;
                rb2.Enabled = true;
                txtInputDA3.Enabled = false;
                rb3.Enabled = false;
                txtInputDA4.Enabled = false;
                rb4.Enabled = false;
            }

            //combobox3
            if (comboBox3.SelectedIndex == 4)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox11.Enabled = true;
            }
            else if (comboBox3.SelectedIndex == 3)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox11.Enabled = false;
            }
            else if (comboBox3.SelectedIndex == 2)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = false;
                textBox11.Enabled = false;
            }
            else if (comboBox3.SelectedIndex == 1)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox11.Enabled = false;
            }
            else if (comboBox3.SelectedIndex == 0)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox11.Enabled = false;
            }


            //combobox6
            if (comboBox6.SelectedIndex == 7)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox14.Enabled = true;
                textBox9.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox10.Enabled = true;
            }
            else if (comboBox6.SelectedIndex == 6)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox14.Enabled = true;
                textBox9.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox10.Enabled = false;
            }
            else if (comboBox6.SelectedIndex == 5)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox14.Enabled = true;
                textBox9.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = false;
                textBox10.Enabled = false;
            }
            else if (comboBox6.SelectedIndex == 4)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox14.Enabled = true;
                textBox9.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox10.Enabled = false;
            }
            else if (comboBox6.SelectedIndex == 3)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox14.Enabled = true;
                textBox9.Enabled = true;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox10.Enabled = false;
            }
            else if (comboBox6.SelectedIndex == 2)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox14.Enabled = true;
                textBox9.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox10.Enabled = false;
            }
            else if (comboBox6.SelectedIndex == 1)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox14.Enabled = false;
                textBox9.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox10.Enabled = false;
            }
            else if (comboBox6.SelectedIndex == 1)
            {
                textBox15.Enabled = true;
                textBox16.Enabled = false;
                textBox14.Enabled = false;
                textBox9.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox10.Enabled = false;
            }
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }
        private void cbMonhoc_SelectedValueChanged(object sender, EventArgs e)
        {
        }
    }
}
