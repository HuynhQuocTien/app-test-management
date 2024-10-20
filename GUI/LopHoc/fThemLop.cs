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
        LopBLL lopBLL;
        private ChiTietLopBLL chiTietLopBLL;
        private string hanhDong;
        private string maMoi;
        private LopDTO lopUpdate;
        public fThemLop(LopHocControl flophoc,string hanhDong,string maMoi = null,LopDTO lop=null)
        {
            InitializeComponent();
            lopHocControl = flophoc;
            this.hanhDong = hanhDong;
            this.maMoi = maMoi;
            this.lopUpdate = lop;
            lopBLL = new LopBLL();
            chiTietLopBLL = new ChiTietLopBLL();
            if (hanhDong.Equals("edit"))
            {
                lopUpdate = lop;
                txtTenlop.Text = lopUpdate.TenLop;
                this.Text = "Cập nhật lớp học";
                label2.Text = "Cập nhật lớp học";
            }
            if (hanhDong.Equals("join"))
            {
                this.Text = "Mã mời";
                label2.Text = "Nhập mã mời để vào lớp";
                label2.Location = new Point(50, 15);
                label1.Text = "Nhập mã mời";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (hanhDong.Equals("add"))
            {
                try
                {
                    if (checkValidInput())
                    {
                        LopDTO lopAdd = new LopDTO(lopBLL.GetAutoIncrement(), fDangNhap.nguoiDungDTO.MaNguoiDung, txtTenlop.Text, maMoi, 1,0);
                        lopHocControl.AddLop(lopAdd);
                        this.Close();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (hanhDong.Equals("edit"))
            {
                if (checkValidInput())
                {
                    try
                    {
                        LopDTO objUpdate = new LopDTO(lopUpdate.MaLop, fDangNhap.nguoiDungDTO.MaNguoiDung, txtTenlop.Text, maMoi, 1, 0);
                        lopHocControl.UpdateLop(objUpdate);
                        this.Close();
                        MessageBox.Show("Cập nhật tên lớp thành công!");
                        this.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else if (hanhDong.Equals("join")) // Tham gia lớp Sinh viên
            {
                if (checkValidInputMaMoi())
                {
                    if (lopBLL.checkMaMoi(txtTenlop.Text)) //Mã mời nhập
                    {
                        MessageBox.Show("Tham gia lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        int maLopAdd = lopBLL.GetMaLopByMaMoi(txtTenlop.Text);
                        ChiTietLopDTO objAdd = new ChiTietLopDTO(1,maLopAdd, fDangNhap.nguoiDungDTO.MaNguoiDung, 1,0); //Mã chi tiết cho mặc định vì tự tăng
                        chiTietLopBLL.Add(objAdd);
                        lopHocControl.renderLopDTO(lopBLL.getListLopByMaSV(fDangNhap.nguoiDungDTO.MaNguoiDung));
                        this.Dispose();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã nhập sai mã mời", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
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
        private bool checkValidInputMaMoi()
        {
            if (string.IsNullOrEmpty(txtTenlop.Text))
            {
                MessageBox.Show("Mã mời không được rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtTenlop.Text.Length != 10)
            {
                MessageBox.Show("Mã mời phải có đủ 10 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;
        }
    }
}
