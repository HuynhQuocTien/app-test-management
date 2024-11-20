using BLL;
using DAL;
using DTO;
using GUI.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.CauHoi
{
    public partial class fCauHoiDCT : Form
    {
        private CauHoiDTO cauHoiDTO;

        public fCauHoiDCT(CauHoiDTO cauHoiDTO)
        {
            this.cauHoiDTO = cauHoiDTO;
            InitializeComponent();
            render();
        }
        private void render()
        {
            loadDataComboBoxMHView();
            loadDataCauhoi();
            loadDataCauTraLoi();
        }

        private void loadDataComboBoxMHView()
        {
            MonHocBLL monHocBLL = new MonHocBLL();
            comboBox2.DataSource = monHocBLL.GetAll();
            comboBox2.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBox2.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)
        }

        private void loadDataCauhoi()
        {
            switch (this.cauHoiDTO.DoKho)
            {
                case 1:
                    comboBox1.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox1.SelectedIndex = 1;
                    break;
                case 3:
                    comboBox1.SelectedIndex = 2;
                    break;
                default:
                    comboBox1.SelectedIndex = -1; // Không chọn mục nào nếu DoKho không hợp lệ
                    break;
            }
            comboBox2.SelectedValue = this.cauHoiDTO.MaMonHoc;
            textBox1.Text = this.cauHoiDTO.NoiDung;
            checkBox2.Checked = this.cauHoiDTO.TrangThai == 1;
        }


        private void loadDataCauTraLoi()
        {
            List<CauTraLoiDienChoTrongDTO> cauTraLoiList = new List<CauTraLoiDienChoTrongDTO>();
            CauTraLoiDienChoTrongBLL cauTraLoiDienChoTrongBLL = new CauTraLoiDienChoTrongBLL();
            cauTraLoiList=cauTraLoiDienChoTrongBLL.GetAll(this.cauHoiDTO.MaCauHoi);
            comboBox3.SelectedItem = cauTraLoiList.Count.ToString();
            // Kiểm tra theo số đáp án được chọn trong ComboBox
            if (comboBox3.SelectedIndex == 4)  // 5 đáp án
            {
                textBox2.Text=cauTraLoiList[0].DapAnText.ToString();
                textBox3.Text = cauTraLoiList[1].DapAnText.ToString();
                textBox4.Text = cauTraLoiList[2].DapAnText.ToString();
                textBox5.Text = cauTraLoiList[3].DapAnText.ToString();
                textBox11.Text = cauTraLoiList[4].DapAnText.ToString();

            }
            else if (comboBox3.SelectedIndex == 3)  // 4 đáp án
            {
                textBox2.Text = cauTraLoiList[0].DapAnText.ToString();
                textBox3.Text = cauTraLoiList[1].DapAnText.ToString();
                textBox4.Text = cauTraLoiList[2].DapAnText.ToString();
                textBox5.Text = cauTraLoiList[3].DapAnText.ToString();
            }
            else if (comboBox3.SelectedIndex == 2)  // 3 đáp án
            {
                textBox2.Text = cauTraLoiList[0].DapAnText.ToString();
                textBox3.Text = cauTraLoiList[1].DapAnText.ToString();
                textBox4.Text = cauTraLoiList[2].DapAnText.ToString();
            }
            else if (comboBox3.SelectedIndex == 1)  // 2 đáp án
            {
                textBox2.Text = cauTraLoiList[0].DapAnText.ToString();
                textBox3.Text = cauTraLoiList[1].DapAnText.ToString();
            }
            else if (comboBox3.SelectedIndex == 0)  // 1 đáp án
            {
                textBox2.Text = cauTraLoiList[0].DapAnText.ToString();
            }
        } 

        private CauHoiDTO getInfo()
        {
            int MaCauHoi = this.cauHoiDTO.MaCauHoi;
            string NoiDung = "";
            string LoaiCauHoi = "Điền từ";
            int MaMonHoc = 0;
            long MaNguoiTao = Convert.ToInt64(Session.UserID);
            int DoKho = 0;

            string selectedValue = comboBox1.SelectedItem.ToString();

            switch (selectedValue)
            {
                case "Dễ":
                    DoKho = 1;
                    break;
                case "Trung Bình":
                    DoKho = 2;
                    break;
                case "Khó":
                    DoKho = 3;
                    break;
            }
            NoiDung = textBox1.Text;
            MaMonHoc = Convert.ToInt32(comboBox2.SelectedValue);


            int trangThai = checkBox2.Checked ? 1 : 0;
            int trangThaiXoa = 0;

            return new CauHoiDTO(MaCauHoi, NoiDung, LoaiCauHoi, MaMonHoc, MaNguoiTao, DoKho, trangThai, trangThaiXoa);
        }

        private CauTraLoiDienChoTrongDTO getInfoCTLDCT(int MaCauHoi, int viTri, string dapAnText, int isDelete)
        {
            int maCauTLiDienChoTrong = 0;
            return new CauTraLoiDienChoTrongDTO(maCauTLiDienChoTrong, MaCauHoi, viTri, dapAnText, isDelete);
        }

        private bool suaCauHoi()
        {
            CauHoiBLL cauHoiBLL = new CauHoiBLL();
            bool check = cauHoiBLL.Update(this.getInfo());
            return check;
        }

        private void cbSoDapAn_SelectedValueChanged(object sender, EventArgs e)
        {
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
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int i = 0;
            // Kiểm tra theo số đáp án được chọn trong ComboBox
            if (comboBox3.SelectedIndex == 4)  // 5 đáp án
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox11.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiDienChoTrongBLL cautraloiDCT = new CauTraLoiDienChoTrongBLL();
                        int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 1, textBox2.Text, 0));
                        int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 2, textBox3.Text, 0));
                        int DA3 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 3, textBox4.Text, 0));
                        int DA4 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 4, textBox5.Text, 0));
                        int DA5 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 5, textBox11.Text, 0));
                        if (DA1 == 1 && DA2 == 1 && DA3 == 1 && DA4 == 1 && DA5 == 1)
                        {
                            i = 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }
                }


            }
            else if (comboBox3.SelectedIndex == 3)  // 4 đáp án
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiDienChoTrongBLL cautraloiDCT = new CauTraLoiDienChoTrongBLL();
                        int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 1, textBox2.Text, 0));
                        int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 2, textBox3.Text, 0));
                        int DA3 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 3, textBox4.Text, 0));
                        int DA4 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 4, textBox5.Text, 0));
                        if (DA1 == 1 && DA2 == 1 && DA3 == 1 && DA4 == 1)
                        {
                            i = 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }
                }
            }
            else if (comboBox3.SelectedIndex == 2)  // 3 đáp án
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiDienChoTrongBLL cautraloiDCT = new CauTraLoiDienChoTrongBLL();
                        int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 1, textBox2.Text, 0));
                        int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 2, textBox3.Text, 0));
                        int DA3 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 3, textBox4.Text, 0));
                        if (DA1 == 1 && DA2 == 1 && DA3 == 1)
                        {
                            i = 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }
                }
            }
            else if (comboBox3.SelectedIndex == 1)  // 2 đáp án
            {
                if (textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiDienChoTrongBLL cautraloiDCT = new CauTraLoiDienChoTrongBLL();
                        int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 1, textBox2.Text, 0));
                        int DA2 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 2, textBox3.Text, 0));
                        if (DA1 == 1 && DA2 == 1)
                        {
                            i = 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }
                }
            }
            else if (comboBox3.SelectedIndex == 0)  // 1 đáp án
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID

                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiDienChoTrongBLL cautraloiDCT = new CauTraLoiDienChoTrongBLL();
                        int DA1 = cautraloiDCT.Add(this.getInfoCTLDCT(this.cauHoiDTO.MaCauHoi, 1, textBox2.Text, 0));
                        if (DA1 == 1)
                        {
                            i = 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }
                }
            }


            // Thông báo nếu việc thêm câu trả lời thành công
            if (i == 1)
            {
                MessageBox.Show("Sửa thành công", "Thông báo sửa", MessageBoxButtons.OK);
            }
            else if (i == 0)
            {
                MessageBox.Show("Có lỗi xảy ra", "Thông báo sửa", MessageBoxButtons.OK);
            }
        }


    }
}
