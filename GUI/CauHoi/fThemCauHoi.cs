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
using System.Text.RegularExpressions;

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
            //InitTextBox();
            //InitTextBoxAH();
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
            int trangThai = 0;
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
                trangThai = checkBox1.Checked ? 1 : 0;

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
                trangThai = checkBox2.Checked ? 1 : 0;

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
                trangThai = checkBox4.Checked ? 1 : 0;

            }

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

        private NoiCauTraLoiDTO getInfoNCTL(int MaCauNoi, string NoiDung, string DapAnNoi)
        {
            int maCauTL = 0;
            return new NoiCauTraLoiDTO(maCauTL, MaCauNoi, NoiDung, DapAnNoi);
        }
        private NoiCauDTO getInfoNC(int MaCauHoi, string NoiDung, decimal Diem)
        {
            int MaNoiCau = 0;
            return new NoiCauDTO(MaNoiCau, MaCauHoi, NoiDung, Diem);
        }

        private int CountLines(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
                return 0;

            // Đếm số dòng bằng cách đếm ký tự xuống dòng + 1
            return textBox.Text.Split('\n').Length;
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
            if (comboBox6.SelectedIndex==-1)
            {
                MessageBox.Show("Chưa chọn số đáp án", "Báo Lỗi", MessageBoxButtons.OK);
                return;
            }
            if (CountLines(textBox12)>8 || CountLines(textBox13)>8)
            {
                MessageBox.Show("Một trong hai cột đã vượt quá 8 dòng", "Báo Lỗi");
                return;
            }
            if (CountLines(textBox12) != CountLines(textBox13))
            {
                MessageBox.Show("Số dòng ở 2 cột phải ngang nhau", "Báo Lỗi");
                return;
            }
            // Thêm các validation khác nếu cần
            if (string.IsNullOrWhiteSpace(textBox12.Text) || string.IsNullOrWhiteSpace(textBox13.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ nội dung cho cả hai cột", "Báo Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateLineFormat123(textBox12))
            {
                MessageBox.Show("Cột bên trái phải theo định dạng 1., 2., 3.,... ", "Lỗi định dạng");
                return;
            }

            if (!ValidateTextFormatABC(textBox13))
            {
                MessageBox.Show("Cột bên phải phải theo định dạng A., B., C.,... ", "Lỗi định dạng");
                return;
            }

            if(CountLines(textBox12) != int.Parse(comboBox6.SelectedItem.ToString()) || CountLines(textBox13) != int.Parse(comboBox6.SelectedItem.ToString()))
            {
                MessageBox.Show("Số dòng của 2 cột không được khác số đáp án", "Lỗi định dạng");
                return;
            }
            Dictionary<char, string> CauNoi = ProcessTextBoxContent(textBox12);
            Dictionary<char, string> DapAn = ProcessTextBoxContent(textBox13);
            Dictionary<string,string> CauNoi_DapAn=NoiCau_CauTraLoi(CauNoi, DapAn);


            int MaCauHoi = themCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

            if (MaCauHoi > 0) // Nếu thêm câu hỏi thành công
            {
                themNoiCau(MaCauHoi,CauNoi, CauNoi_DapAn); // Sử dụng MaCauHoi để thêm câu trả lời
                //clearFormDienChoTrong();
            }
            else
            {
                MessageBox.Show("Thêm câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
            }
        }

        private bool ValidateTextFormatABC(TextBox textBox)
        {
            if (textBox == null) return false;

            string[] lines = textBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string pattern = @"^[A-H]\.\s*.+"; // Thêm \s*.+ để đảm bảo có nội dung sau dấu chấm

            return lines.All(line =>
            {
                string trimmedLine = line.Trim();
                return !string.IsNullOrEmpty(trimmedLine) && Regex.IsMatch(trimmedLine, pattern);
            });
        }

        private bool ValidateLineFormat123(TextBox textBox)
        {
            if (textBox == null) return false;

            string[] lines = textBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                int expectedNumber = i + 1;
                if (!Regex.IsMatch(line, $@"^{expectedNumber}\.\s*.+"))
                {
                    return false;
                }
            }
            return true;
        }


        private void themNoiCau(int MaCauHoi, Dictionary<char, string> CauNois, Dictionary<string, string> CauNoi_DapAn)
        {
            foreach (KeyValuePair<char, string> CauNoi in CauNois)
            {
                string NoiDungNoiCau = CauNoi.Value;
                NoiCauBLL noiCauBLL = new NoiCauBLL();
                KeyValuePair<int, string> RecordNoiCau = noiCauBLL.Add(this.getInfoNC(MaCauHoi, NoiDungNoiCau, 1));
                string NoiDungCauTraLoi = "";
                bool found = false;

                // Tìm đáp án cho câu hỏi
                foreach (var pair in CauNoi_DapAn)
                {
                    if (pair.Key == RecordNoiCau.Value)
                    {
                        NoiDungCauTraLoi = pair.Value;
                        found = true;
                        break; // Dừng vòng lặp sau khi tìm thấy
                    }
                }

                // Kiểm tra nếu không tìm thấy đáp án
                if (!found)
                {
                    MessageBox.Show("Câu trả lời bị lỗi.", "Thông báo lỗi", MessageBoxButtons.OK);
                    return;
                }

                // Thêm câu trả lời
                NoiCauTraLoiBLL noiCauTraLoi = new NoiCauTraLoiBLL();
                if (!noiCauTraLoi.Add(this.getInfoNCTL(RecordNoiCau.Key, NoiDungCauTraLoi, NoiDungNoiCau)))
                {
                    MessageBox.Show("Câu trả lời không thêm được.", "Thông báo lỗi", MessageBoxButtons.OK);
                    return;
                }
            }
            MessageBox.Show("Thêm thành công", "Thông báo thêm", MessageBoxButtons.OK);
        }

        private Dictionary<string, string> NoiCau_CauTraLoi(Dictionary<char, string> CauNoi, Dictionary<char, string> DapAn)
        {
            Dictionary<string, string> test= new Dictionary<string, string>();
            if (comboBox6.SelectedIndex == 7)
            {
                if (!string.IsNullOrEmpty(textBox15.Text) && textBox15.Text.Length == 1)
                {
                    string keyToCheck = "1";
                    char enteredKey = textBox15.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox16.Text) && textBox16.Text.Length == 1)
                {
                    string keyToCheck = "2";
                    char enteredKey = textBox16.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox14.Text) && textBox14.Text.Length == 1)
                {
                    string keyToCheck = "3";
                    char enteredKey = textBox14.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }


                if (!string.IsNullOrEmpty(textBox9.Text) && textBox9.Text.Length == 1)
                {
                    string keyToCheck = "4";
                    char enteredKey = textBox9.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox6.Text) && textBox6.Text.Length == 1)
                {
                    string keyToCheck = "5";
                    char enteredKey = textBox6.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox7.Text) && textBox7.Text.Length == 1)
                {
                    string keyToCheck = "6";
                    char enteredKey = textBox7.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox8.Text) && textBox8.Text.Length == 1)
                {
                    string keyToCheck = "7";
                    char enteredKey = textBox8.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox10.Text) && textBox10.Text.Length == 1)
                {
                    string keyToCheck = "8";
                    char enteredKey = textBox10.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

            }
            else if (comboBox6.SelectedIndex == 6)
            {
                if (!string.IsNullOrEmpty(textBox15.Text) && textBox15.Text.Length == 1)
                {
                    string keyToCheck = "1";
                    char enteredKey = textBox15.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox16.Text) && textBox16.Text.Length == 1)
                {
                    string keyToCheck = "2";
                    char enteredKey = textBox16.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox14.Text) && textBox14.Text.Length == 1)
                {
                    string keyToCheck = "3";
                    char enteredKey = textBox14.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }


                if (!string.IsNullOrEmpty(textBox9.Text) && textBox9.Text.Length == 1)
                {
                    string keyToCheck = "4";
                    char enteredKey = textBox9.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox6.Text) && textBox6.Text.Length == 1)
                {
                    string keyToCheck = "5";
                    char enteredKey = textBox6.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox7.Text) && textBox7.Text.Length == 1)
                {
                    string keyToCheck = "6";
                    char enteredKey = textBox7.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox8.Text) && textBox8.Text.Length == 1)
                {
                    string keyToCheck = "7";
                    char enteredKey = textBox8.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }
            }
            else if (comboBox6.SelectedIndex == 5)
            {
                if (!string.IsNullOrEmpty(textBox15.Text) && textBox15.Text.Length == 1)
                {
                    string keyToCheck = "1";
                    char enteredKey = textBox15.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox16.Text) && textBox16.Text.Length == 1)
                {
                    string keyToCheck = "2";
                    char enteredKey = textBox16.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox14.Text) && textBox14.Text.Length == 1)
                {
                    string keyToCheck = "3";
                    char enteredKey = textBox14.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }


                if (!string.IsNullOrEmpty(textBox9.Text) && textBox9.Text.Length == 1)
                {
                    string keyToCheck = "4";
                    char enteredKey = textBox9.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox6.Text) && textBox6.Text.Length == 1)
                {
                    string keyToCheck = "5";
                    char enteredKey = textBox6.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox7.Text) && textBox7.Text.Length == 1)
                {
                    string keyToCheck = "6";
                    char enteredKey = textBox7.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }
            }
            else if (comboBox6.SelectedIndex == 4)
            {
                if (!string.IsNullOrEmpty(textBox15.Text) && textBox15.Text.Length == 1)
                {
                    string keyToCheck = "1";
                    char enteredKey = textBox15.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox16.Text) && textBox16.Text.Length == 1)
                {
                    string keyToCheck = "2";
                    char enteredKey = textBox16.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox14.Text) && textBox14.Text.Length == 1)
                {
                    string keyToCheck = "3";
                    char enteredKey = textBox14.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }


                if (!string.IsNullOrEmpty(textBox9.Text) && textBox9.Text.Length == 1)
                {
                    string keyToCheck = "4";
                    char enteredKey = textBox9.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox6.Text) && textBox6.Text.Length == 1)
                {
                    string keyToCheck = "5";
                    char enteredKey = textBox6.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }
            }
            else if (comboBox6.SelectedIndex == 3)
            {
                if (!string.IsNullOrEmpty(textBox15.Text) && textBox15.Text.Length == 1)
                {
                    string keyToCheck = "1";
                    char enteredKey = textBox15.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox16.Text) && textBox16.Text.Length == 1)
                {
                    string keyToCheck = "2";
                    char enteredKey = textBox16.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox14.Text) && textBox14.Text.Length == 1)
                {
                    string keyToCheck = "3";
                    char enteredKey = textBox14.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }


                if (!string.IsNullOrEmpty(textBox9.Text) && textBox9.Text.Length == 1)
                {
                    string keyToCheck = "4";
                    char enteredKey = textBox9.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }
            }
            else if (comboBox6.SelectedIndex == 2)
            {
                if (!string.IsNullOrEmpty(textBox15.Text) && textBox15.Text.Length == 1)
                {
                    string keyToCheck = "1";
                    char enteredKey = textBox15.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox16.Text) && textBox16.Text.Length == 1)
                {
                    string keyToCheck = "2";
                    char enteredKey = textBox16.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox14.Text) && textBox14.Text.Length == 1)
                {
                    string keyToCheck = "3";
                    char enteredKey = textBox14.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }
            }
            else if (comboBox6.SelectedIndex == 1)
            {
                if (!string.IsNullOrEmpty(textBox15.Text) && textBox15.Text.Length == 1)
                {
                    string keyToCheck = "1";
                    char enteredKey = textBox15.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }

                if (!string.IsNullOrEmpty(textBox16.Text) && textBox16.Text.Length == 1)
                {
                    string keyToCheck = "2";
                    char enteredKey = textBox16.Text.ToUpper()[0]; // Lấy ký tự đầu tiên và chuyển thành chữ hoa để so sánh
                    if (DapAn.ContainsKey(enteredKey))
                    {
                        string noicau = CauNoi[keyToCheck[0]];
                        string dapan = DapAn[enteredKey];
                        test[noicau] = dapan;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập trong TextBox không trùng với bất kỳ Key nào trong CauNoi.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một ký tự hợp lệ vào TextBox.");
                    return null;
                }
            }
            return test;
        }

        private Dictionary<char, string> ProcessTextBoxContent(TextBox textBox)
        {
            Dictionary<char, string> keyValuePairs = new Dictionary<char, string>();

            // Tách từng dòng trong textBox
            string[] lines = textBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                // Kiểm tra xem dòng có định dạng đúng không (chứa ít nhất 2 ký tự và có dấu ".")
                if (line.Length > 2 && line[1] == '.')
                {
                    char key = line[0]; // Ký tự đầu tiên làm Key
                    string value = line.Substring(2).Trim(); // Phần còn lại sau dấu "." làm Value

                    // Thêm vào Dictionary
                    keyValuePairs[key] = value;
                }
            }

            return keyValuePairs;
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

        private void clearFromNoiCau()
        {

        }
    }
}
