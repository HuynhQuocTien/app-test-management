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

namespace GUI.CauHoi
{
    public partial class fCauHoiNoiCau : Form
    {
        private CauHoiDTO cauHoiDTO;
        public fCauHoiNoiCau(CauHoiDTO cauHoiDTO)
        {
            this.cauHoiDTO = cauHoiDTO;
            InitializeComponent();
            render();
        }

        private void render()
        {
            loadDataComboBoxMHView();
            loadDataCauhoi();
            loadDataNoiCau();
        }

        private void loadDataComboBoxMHView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            comboBox5.DataSource = monHocBLL.GetAll();
            comboBox5.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBox5.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)
        }

        private void loadDataCauhoi()
        {
            switch (this.cauHoiDTO.DoKho)
            {
                case "1":
                    comboBox4.SelectedIndex = 0;
                    break;
                case "2":
                    comboBox4.SelectedIndex = 1;
                    break;
                case "3":
                    comboBox4.SelectedIndex = 2;
                    break;
                default:
                    comboBox4.SelectedIndex = -1; // Không chọn mục nào nếu DoKho không hợp lệ
                    break;
            }
            comboBox5.SelectedValue = this.cauHoiDTO.MaMonHoc;
            checkBox4.Checked = this.cauHoiDTO.TrangThai == 1;
        }

        private void loadDataNoiCau()
        {
            List<NoiCauTraLoiDTO> noiCauList = new List<NoiCauTraLoiDTO>();
            NoiCauTraLoiBLL noiCauBll = new NoiCauTraLoiBLL();
            noiCauList = noiCauBll.GetAll(this.cauHoiDTO.MaCauHoi);
            StringBuilder noiDung = new StringBuilder();
            // Duyệt qua danh sách noiCauList và nối các dòng với số thứ tự
            for (int i = 0; i < noiCauList.Count; i++)
            {
                noiDung.AppendLine($"{i + 1}. {noiCauList[i].DapAnNoi}");
            }
            // Gán nội dung vào TextBox
            textBox12.Text = noiDung.ToString();

            StringBuilder noiDungCTL = new StringBuilder();
            // Duyệt qua danh sách noiCauList và nối các dòng với chữ cái đầu dòng (A, B, C, ...)
            for (int i = 0; i < noiCauList.Count; i++)
            {
                // Chuyển chỉ số `i` thành ký tự chữ cái (A, B, C, ...)
                char prefix = Convert.ToChar('A' + i);
                noiDungCTL.AppendLine($"{prefix}.{noiCauList[i].NoiDung}");
            }
            textBox13.Text = noiDungCTL.ToString();
            comboBox6.SelectedItem = noiCauList.Count;

            if (comboBox6.SelectedIndex == 7)
            {
                textBox15.Text = "A";
                textBox16.Text = "B";
                textBox14.Text = "C";
                textBox9.Text = "D";
                textBox6.Text = "E";
                textBox7.Text = "F";
                textBox8.Text = "G";
                textBox10.Text = "H";
            }
            else if (comboBox6.SelectedIndex == 6)
            {
                textBox15.Text = "A";
                textBox16.Text = "B";
                textBox14.Text = "C";
                textBox9.Text = "D";
                textBox6.Text = "E";
                textBox7.Text = "F";
                textBox8.Text = "G";
            }
            else if (comboBox6.SelectedIndex == 5)
            {
                textBox15.Text = "A";
                textBox16.Text = "B";
                textBox14.Text = "C";
                textBox9.Text = "D";
                textBox6.Text = "E";
                textBox7.Text = "F";
            }
            else if (comboBox6.SelectedIndex == 4)
            {
                textBox15.Text = "A";
                textBox16.Text = "B";
                textBox14.Text = "C";
                textBox9.Text = "D";
                textBox6.Text = "E";
            }
            else if (comboBox6.SelectedIndex == 3)
            {
                textBox15.Text = "A";
                textBox16.Text = "B";
                textBox14.Text = "C";
                textBox9.Text = "D";
            }
            else if (comboBox6.SelectedIndex == 2)
            {
                textBox15.Text = "A";
                textBox16.Text = "B";
                textBox14.Text = "C";
            }
            else if (comboBox6.SelectedIndex == 1)
            {
                textBox15.Text = "A";
                textBox16.Text = "B";
            }

        }

        private void cbSoDapAn_SelectedValueChanged (object sender, EventArgs e)
        {
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
        }
    }
}
