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
using GUI.Users;
using DocumentFormat.OpenXml.Bibliography;

namespace GUI.CauHoi
{
    public partial class fThemCauHoi : Form
    {
        public fThemCauHoi()
        {
            InitializeComponent();
            this.btnLuu.Click += btnLuu_Click;
            this.button1.Click += button1_Click;
            this.button2.Click += button2_Click;

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

        private string tabControl2_SelectedName()
        {
            // Lấy tab hiện tại đang được chọn
            TabPage selectedTab = tabControl2.SelectedTab;

            // Hiển thị tên của tab đang được chọn
            return selectedTab.Text;
        }

        private CauHoiDTO getInfo()
        {
            int MaCauHoi = 0;
            string NoiDung = "";
            string LoaiCauHoi = tabControl2_SelectedName();
            int MaMonHoc = 0;
            long MaNguoiTao = Convert.ToInt64(Session.UserID);
            string DoKho="";
            if (tabControl2_SelectedName().Equals("Trắc nghiệm"))
            {
                string selectedValue = comboBoxDoKho.SelectedItem.ToString();

                switch (selectedValue)
                {
                    case "Dễ":
                        DoKho = "1";
                        break;
                    case "Trung Bình":
                        DoKho = "2";
                        break;
                    case "Khó":
                        DoKho = "3";
                        break;
                }
                NoiDung= txtNoiDung.Text;
                MaMonHoc = Convert.ToInt32(comboBoxMonHoc.SelectedValue);

            } else if (tabControl2_SelectedName().Equals("Điền từ"))
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                switch (selectedValue)
                {
                    case "Dễ":
                        DoKho = "1";
                        break;
                    case "Trung Bình":
                        DoKho = "2";
                        break;
                    case "Khó":
                        DoKho = "3";
                        break;
                }
                NoiDung = textBox1.Text;
                MaMonHoc = Convert.ToInt32(comboBox2.SelectedValue);
            }
            else if (tabControl2_SelectedName().Equals("Nối câu"))
            {
                string selectedValue = comboBox4.SelectedItem.ToString();

                switch (selectedValue)
                {
                    case "Dễ":
                        DoKho = "1";
                        break;
                    case "Trung Bình":
                        DoKho = "2";
                        break;
                    case "Khó":
                        DoKho = "3";
                        break;
                }
                NoiDung = "Hãy nối hai cột lại với nhau:";
                MaMonHoc = Convert.ToInt32(comboBox5.SelectedValue);
            }
            int trangThai = checkBox1.Checked ? 1 : 0;
            int trangThaiXoa = 0;

            return new CauHoiDTO(MaCauHoi, NoiDung, LoaiCauHoi, MaMonHoc, MaNguoiTao, DoKho, trangThai, trangThaiXoa);
        }


        private CauTraLoiDTO getInfoCTL(int MaCauHoi, string NoiDung, int is_DapAn)
        {
            int MaCauTL = 0;
            return new CauTraLoiDTO(MaCauTL, MaCauHoi, NoiDung,is_DapAn);
        }

        private CauTraLoiDienChoTrongDTO getInfoCTLDCT(int MaCauHoi, int viTri, string dapAnText, int isDelete)
        {
            int maCauTLiDienChoTrong = 0;
            return new CauTraLoiDienChoTrongDTO(maCauTLiDienChoTrong, MaCauHoi, viTri, dapAnText, isDelete);
        }

        private NoiCauTraLoiDTO getInfoNC(int MaCauNoi, string NoiDung, string DapAnNoi)
        {
            int maCauTL = 0;
            return new NoiCauTraLoiDTO(maCauTL, MaCauNoi, NoiDung, DapAnNoi);
        }
        private NoiCauDTO getInfoNCTL(int MaCauHoi, string NoiDung, decimal Diem)
        {
            int MaNoiCau = 0;
            return new NoiCauDTO(MaNoiCau, MaCauHoi, NoiDung, Diem);
        }
        private int themCauHoi()
        {
            CauHoiBLL cauHoiBLL = new CauHoiBLL();
            int MaCauHoi = cauHoiBLL.Add(this.getInfo());
            return MaCauHoi;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int MaCauHoi = themCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

            if (MaCauHoi > 0) // Nếu thêm câu hỏi thành công
            {
                themCauTraLoiTracNghiem(MaCauHoi); // Sử dụng MaCauHoi để thêm câu trả lời
                clearFormTracNghiem();
            }
            else
            {
                MessageBox.Show("Thêm câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
            }
        }

        private void themCauTraLoiTracNghiem(int MaCauHoi)
        {
            CauTraLoiBLL cautraloiTN = new CauTraLoiBLL();
            int i = 0;

            // Kiểm tra theo số đáp án được chọn trong ComboBox
            if (cbSoDapAn.SelectedIndex == 2)  // 4 đáp án
            {
                int DA1 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA1.Text, rb1.Checked ? 1 : 0));
                int DA2 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA2.Text, rb2.Checked ? 1 : 0));
                int DA3 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA3.Text, rb3.Checked ? 1 : 0));
                int DA4 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA4.Text, rb4.Checked ? 1 : 0));
                if (DA1 == 1 && DA2 == 1 && DA3 == 1 && DA4 == 1)
                {
                    i = 1;
                }
            }
            else if (cbSoDapAn.SelectedIndex == 1)  // 3 đáp án
            {
                int DA1 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA1.Text, rb1.Checked ? 1 : 0));
                int DA2 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA2.Text, rb2.Checked ? 1 : 0));
                int DA3 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA3.Text, rb3.Checked ? 1 : 0));
                if (DA1 == 1 && DA2 == 1 && DA3 == 1)
                {
                    i = 1;
                }
            }
            else if (cbSoDapAn.SelectedIndex == 0)  // 2 đáp án
            {
                int DA1 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA1.Text, rb1.Checked ? 1 : 0));
                int DA2 = cautraloiTN.Add(this.getInfoCTL(MaCauHoi, txtInputDA2.Text, rb2.Checked ? 1 : 0));
                if (DA1 == 1 && DA2 == 1)
                {
                    i = 1;
                }
            }

            // Thông báo nếu việc thêm câu trả lời thành công
            if (i == 1)
            {
                MessageBox.Show("Nhập thành công", "Thông báo thêm", MessageBoxButtons.OK);
            }
            else if (i == 0)
            {
                MessageBox.Show("Có lỗi xảy ra", "Thông báo thêm", MessageBoxButtons.OK);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int MaCauHoi = themCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

            if (MaCauHoi > 0) // Nếu thêm câu hỏi thành công
            {
                themCauTraLoiDienChoTrong(MaCauHoi); // Sử dụng MaCauHoi để thêm câu trả lời
                clearFormDienChoTrong();
            }
            else
            {
                MessageBox.Show("Thêm câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
            }
        }
        private void themCauTraLoiDienChoTrong(int MaCauHoi)
        {
            CauTraLoiDienChoTrongBLL cautraloiDCT = new CauTraLoiDienChoTrongBLL();
            int i = 0;

            // Kiểm tra theo số đáp án được chọn trong ComboBox
            if (comboBox3.SelectedIndex == 4)  // 5 đáp án
            {
                int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi,1, textBox2.Text,0));
                int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi,2, textBox3.Text, 0));
                int DA3 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi,3, textBox4.Text, 0));
                int DA4 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi,4, textBox5.Text, 0));
                int DA5 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi,5, textBox11.Text, 0));
                if (DA1 == 1 && DA2 == 1 && DA3 == 1 && DA4 == 1 && DA5==1)
                {
                    i = 1;
                }
            }
            else if (comboBox3.SelectedIndex == 3)  // 4 đáp án
            {
                int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 1, textBox2.Text, 0));
                int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 2, textBox3.Text, 0));
                int DA3 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 3, textBox4.Text, 0));
                int DA4 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 4, textBox5.Text, 0));
                if (DA1 == 1 && DA2 == 1 && DA3 == 1 && DA4 == 1)
                {
                    i = 1;
                }
            }
            else if (comboBox3.SelectedIndex == 2)  // 3 đáp án
            {
                int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 1, textBox2.Text, 0));
                int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 2, textBox3.Text, 0));
                int DA3 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 3, textBox4.Text, 0));
                if (DA1 == 1 && DA2 == 1 && DA3 == 1)
                {
                    i = 1;
                }
            }
            else if (comboBox3.SelectedIndex == 1)  // 2 đáp án
            {
                int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 1, textBox2.Text, 0));
                int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 2, textBox3.Text, 0));
                if (DA1 == 1 && DA2 == 1)
                {
                    i = 1;
                }
            }
            else if (comboBox3.SelectedIndex == 0)  // 1 đáp án
            {
                int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(MaCauHoi, 1, textBox2.Text, 0));
                if (DA1 == 1)
                {
                    i = 1;
                }
            }

            // Thông báo nếu việc thêm câu trả lời thành công
            if (i == 1)
            {
                MessageBox.Show("Nhập thành công", "Thông báo thêm", MessageBoxButtons.OK);
            }
            else if (i == 0)
            {
                MessageBox.Show("Có lỗi xảy ra", "Thông báo thêm", MessageBoxButtons.OK);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //themCauHoi();
            //clearFormTracNghiem();
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

        private void clearFormTracNghiem()
        {
            txtNoiDung.Text = "";
            txtInputDA1.Text = "";
            txtInputDA2.Text = "";
            txtInputDA3.Text = "";
            txtInputDA4.Text = "";
        }

        private void clearFormDienChoTrong()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox11.Text = "";
        }
    }
}
