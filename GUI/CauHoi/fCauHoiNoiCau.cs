using BLL;
using DTO;
using GUI.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                case 1:
                    comboBox4.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox4.SelectedIndex = 1;
                    break;
                case 3:
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
            comboBox6.SelectedItem = noiCauList.Count.ToString();

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

        private CauHoiDTO getInfo()
        {
            int MaCauHoi = cauHoiDTO.MaCauHoi;
            string NoiDung = "Hãy nối hai cột lại với nhau:";
            string LoaiCauHoi = "Nối câu";
            int MaMonHoc = 0;
            long MaNguoiTao = Convert.ToInt64(Session.UserID);
            string DoKho = "";
            int trangThai = 0;
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
            MaMonHoc = Convert.ToInt32(comboBox5.SelectedValue);
            trangThai = checkBox4.Checked ? 1 : 0;
            int trangThaiXoa = 0;
            return new CauHoiDTO(MaCauHoi, NoiDung, LoaiCauHoi, MaMonHoc, MaNguoiTao, Convert.ToInt32(DoKho), trangThai, trangThaiXoa);
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

        private Dictionary<string, string> NoiCau_CauTraLoi(Dictionary<char, string> CauNoi, Dictionary<char, string> DapAn)
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
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

        private bool suaCauHoi()
        {
            CauHoiBLL cauHoiBLL = new CauHoiBLL();
            bool check = cauHoiBLL.Update(this.getInfo());
            return check;
        }

        private void suaNoiCau(List<int>allMaNoiCau, Dictionary<char, string> CauNois, Dictionary<string, string> CauNoi_DapAn)
        {
            foreach(int manoicau in allMaNoiCau)
            {
                NoiCauTraLoiBLL noiCauTraLoiBLL=new NoiCauTraLoiBLL();
                if (!noiCauTraLoiBLL.Delete(manoicau))
                {
                    MessageBox.Show("Không thể sửa mời thử lại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    return;
                };
            }
            NoiCauBLL noiCauBLL1 = new NoiCauBLL();
            if(noiCauBLL1.Delete(this.cauHoiDTO.MaCauHoi))
            {
                foreach (KeyValuePair<char, string> CauNoi in CauNois)
                {
                    string NoiDungNoiCau = CauNoi.Value;
                    NoiCauBLL noiCauBLL = new NoiCauBLL();
                    KeyValuePair<int, string> RecordNoiCau = noiCauBLL.Add(this.getInfoNC(this.cauHoiDTO.MaCauHoi, NoiDungNoiCau, 1));
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
                MessageBox.Show("Sửa thành công", "Thông báo sửa", MessageBoxButtons.OK);
            } else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo sửa", MessageBoxButtons.OK);
            } 
            
        }

        private List<int> clearNoiCau()
        {
            NoiCauBLL noiCauBLL = new NoiCauBLL();
            return noiCauBLL.GetAllMaNoiCau(this.cauHoiDTO.MaCauHoi);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn số đáp án", "Báo Lỗi", MessageBoxButtons.OK);
                return;
            }
            if (CountLines(textBox12) > 8 || CountLines(textBox13) > 8)
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

            if (CountLines(textBox12) != int.Parse(comboBox6.SelectedItem.ToString()) || CountLines(textBox13) != int.Parse(comboBox6.SelectedItem.ToString()))
            {
                MessageBox.Show("Số dòng của 2 cột không được khác số đáp án", "Lỗi định dạng");
                return;
            }
            Dictionary<char, string> CauNoi = ProcessTextBoxContent(textBox12);
            Dictionary<char, string> DapAn = ProcessTextBoxContent(textBox13);
            Dictionary<string, string> CauNoi_DapAn = NoiCau_CauTraLoi(CauNoi, DapAn);


            bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

            if (Check) // Nếu thêm câu hỏi thành công
            {
                List<int> allMaNoiCau = clearNoiCau();
                //string allMaNoiCauString = string.Join(", ", allMaNoiCau);
                //MessageBox.Show(allMaNoiCauString, "");
                if (allMaNoiCau != null)
                {
                    suaNoiCau(allMaNoiCau, CauNoi, CauNoi_DapAn); // Sử dụng MaCauHoi để thêm câu trả lời
                }
            }
            else
            {
                MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
            }
        }

        private void cbSoDapAn_SelectedValueChanged(object sender, EventArgs e)
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
