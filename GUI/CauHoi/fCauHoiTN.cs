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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.CauHoi
{
    public partial class fCauHoiTN : Form
    {
        private CauHoiDTO cauHoiDTO;
        public fCauHoiTN(CauHoiDTO cauHoiDTO)
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
            comboBoxMonHoc.DataSource = monHocBLL.GetAll();
            comboBoxMonHoc.ValueMember = "MaMonHoc";    // Cột giá trị (ID)
            comboBoxMonHoc.DisplayMember = "TenMonHoc"; // Cột hiển thị (Tên môn học)
        }

        private void loadDataCauhoi()
        {
            switch (this.cauHoiDTO.DoKho)
            {
                case "1":
                    comboBoxDoKho.SelectedIndex = 0;
                    break;
                case "2":
                    comboBoxDoKho.SelectedIndex = 1;
                    break;
                case "3":
                    comboBoxDoKho.SelectedIndex = 2;
                    break;
                default:
                    comboBoxDoKho.SelectedIndex = -1; // Không chọn mục nào nếu DoKho không hợp lệ
                    break;
            }
            comboBoxMonHoc.SelectedValue = this.cauHoiDTO.MaMonHoc;
            txtNoiDung.Text = this.cauHoiDTO.NoiDung;
            checkBox1.Checked = this.cauHoiDTO.TrangThai == 1;
        }


        private void loadDataCauTraLoi()
        {
            List<CauTraLoiDTO> cauTraLoiList = new List<CauTraLoiDTO>();
            CauTraLoiBLL cautraloiTN = new CauTraLoiBLL();
            cauTraLoiList = cautraloiTN.GetAll(this.cauHoiDTO.MaCauHoi);
            cbSoDapAn.SelectedItem = cauTraLoiList.Count;
            if (cbSoDapAn.SelectedIndex == 2)  // 4 đáp án
            {
                rb1.Checked = cauTraLoiList[0].IsDapAn==1;
                txtInputDA1.Text = cauTraLoiList[0].NoiDung.ToString();
                rb2.Checked = cauTraLoiList[1].IsDapAn == 1;
                txtInputDA2.Text = cauTraLoiList[1].NoiDung.ToString();
                rb3.Checked = cauTraLoiList[2].IsDapAn == 1;
                txtInputDA3.Text = cauTraLoiList[2].NoiDung.ToString();
                rb4.Checked = cauTraLoiList[3].IsDapAn == 1;
                txtInputDA4.Text = cauTraLoiList[3].NoiDung.ToString();

            }
            else if (cbSoDapAn.SelectedIndex == 1)  // 3 đáp án
            {
                rb1.Checked = cauTraLoiList[0].IsDapAn == 1;
                txtInputDA1.Text = cauTraLoiList[0].NoiDung.ToString();
                rb2.Checked = cauTraLoiList[1].IsDapAn == 1;
                txtInputDA2.Text = cauTraLoiList[1].NoiDung.ToString();
                rb3.Checked = cauTraLoiList[2].IsDapAn == 1;
                txtInputDA3.Text = cauTraLoiList[2].NoiDung.ToString();
            }
            else if (cbSoDapAn.SelectedIndex == 0)  // 2 đáp án
            {
                rb1.Checked = cauTraLoiList[0].IsDapAn == 1;
                txtInputDA1.Text = cauTraLoiList[0].NoiDung.ToString();
                rb2.Checked = cauTraLoiList[1].IsDapAn == 1;
                txtInputDA2.Text = cauTraLoiList[1].NoiDung.ToString();
            }
        }

        private CauHoiDTO getInfo()
        {
            int MaCauHoi = this.cauHoiDTO.MaCauHoi;
            string NoiDung = "";
            string LoaiCauHoi = "Trắc nghiệm";
            int MaMonHoc = 0;
            long MaNguoiTao = Convert.ToInt64(Session.UserID);
            string DoKho = "";

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
            NoiDung = txtNoiDung.Text;
            MaMonHoc = Convert.ToInt32(comboBoxMonHoc.SelectedValue);


            int trangThai = checkBox1.Checked ? 1 : 0;
            int trangThaiXoa = 0;

            return new CauHoiDTO(MaCauHoi, NoiDung, LoaiCauHoi, MaMonHoc, MaNguoiTao, DoKho, trangThai, trangThaiXoa);
        }

        private CauTraLoiDTO getInfoCTL(int MaCauHoi, string NoiDung, int is_DapAn)
        {
            int MaCauTL = 0;
            return new CauTraLoiDTO(MaCauTL, MaCauHoi, NoiDung, is_DapAn);
        }

        public void cbSoDapAn_SelectedValueChanged(object sender, EventArgs e)
        {
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
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra theo số đáp án được chọn trong ComboBox
            if (cbSoDapAn.SelectedIndex == 2)  // 4 đáp án
            {
                if (txtInputDA1.Text == "" || txtInputDA2.Text == "" || txtInputDA3.Text == "" || txtInputDA4.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID
                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiBLL cautraloiTN = new CauTraLoiBLL();
                        int i = 0;
                        int DA1 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA1.Text, rb1.Checked ? 1 : 0));
                        int DA2 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA2.Text, rb2.Checked ? 1 : 0));
                        int DA3 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA3.Text, rb3.Checked ? 1 : 0));
                        int DA4 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA4.Text, rb4.Checked ? 1 : 0));
                        if (DA1 == 1 && DA2 == 1 && DA3 == 1 && DA4 == 1)
                        {
                            i = 1;
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
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }
                    
                } 
                    
            }
            else if (cbSoDapAn.SelectedIndex == 1)  // 3 đáp án
            {
                if (txtInputDA1.Text == "" || txtInputDA2.Text == "" || txtInputDA3.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID
                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiBLL cautraloiTN = new CauTraLoiBLL();
                        int i = 0;
                        int DA1 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA1.Text, rb1.Checked ? 1 : 0));
                        int DA2 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA2.Text, rb2.Checked ? 1 : 0));
                        int DA3 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA3.Text, rb3.Checked ? 1 : 0));
                        if (DA1 == 1 && DA2 == 1 && DA3 == 1)
                        {
                            i = 1;
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
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }

                }
            }
            else if (cbSoDapAn.SelectedIndex == 0)  // 2 đáp án
            {
                if (txtInputDA1.Text == "" || txtInputDA2.Text == "")
                {
                    MessageBox.Show("Ô được chọn không được để trống.", "Thông báo thêm", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    bool Check = suaCauHoi(); // Chỉ gọi themCauHoi() một lần và lưu ID
                    if (Check) // Nếu thêm câu hỏi thành công
                    {
                        CauTraLoiBLL cautraloiTN = new CauTraLoiBLL();
                        int i = 0;
                        int DA1 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA1.Text, rb1.Checked ? 1 : 0));
                        int DA2 = cautraloiTN.Add(this.getInfoCTL(this.cauHoiDTO.MaCauHoi, txtInputDA2.Text, rb2.Checked ? 1 : 0));
                        if (DA1 == 1 && DA2 == 1)
                        {
                            i = 1;
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
                    else
                    {
                        MessageBox.Show("Sửa câu hỏi thất bại.", "Thông báo lỗi", MessageBoxButtons.OK);
                    }

                }
            }
            
        }

        private bool suaCauHoi()
        {
            CauHoiBLL cauHoiBLL = new CauHoiBLL();
            bool check = cauHoiBLL.Update(this.getInfo());
            return check;
        }

    }
}
