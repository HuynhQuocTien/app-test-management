using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.DeThi
{
    public partial class fThemDeThi : Form
    {
        private DeThiControl deThiControl;
        MonHocBLL monHocBLL;
        DeThiBLL deThiBLL;
        private string hanhDong;
        private DeThiDTO deThiUpdate;

        public fThemDeThi(DeThiControl fdethi, string hanhDong, DeThiDTO dethi = null)
        {
            InitializeComponent();
            monHocBLL = new MonHocBLL();
            deThiBLL = new DeThiBLL();

            LoadMonHoc();
            deThiControl = fdethi;
            this.hanhDong = hanhDong;
            this.deThiUpdate = dethi;
            if (hanhDong.Equals("edit"))
            {
                deThiUpdate = dethi;
                txtTenDeThi.Text = deThiUpdate.TenDe;
                numThoiGianLam.Text = (deThiUpdate.ThoiGianKetThuc - deThiUpdate.ThoiGianBatDau).TotalMinutes.ToString();
                cbMonHoc.SelectedValue = deThiUpdate.MaMonHoc;
                this.Text = "Cập nhật đề thi";
                label2.Text = "Cập nhật đề thi";
            }
        }
        private void LoadMonHoc()
        {
            try
            {
                var monHocs = monHocBLL.GetAll();
                cbMonHoc.DataSource = monHocs;
                cbMonHoc.DisplayMember = "TenMonHoc";
                cbMonHoc.ValueMember = "MaMonHoc";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subjects: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (hanhDong.Equals("edit"))
            {
                try
                {
                    MonHocDTO cbMonHocValue = (MonHocDTO)cbMonHoc.SelectedItem;
                    int thoiGianLamBai = (int)numThoiGianLam.Value;
                    DeThiDTO objUpdate = new DeThiDTO(deThiUpdate.MaDe, cbMonHocValue.MaMonHoc, txtTenDeThi.Text, deThiUpdate.ThoiGianTao,
                        deThiUpdate.ThoiGianBatDau, deThiUpdate.ThoiGianBatDau, thoiGianLamBai,
                        fDangNhap.nguoiDungDTO.MaNguoiDung, deThiUpdate.TrangThai, deThiUpdate.is_delete, cbMonHocValue.TenMonHoc);


                    deThiControl.UpdateDeThi(objUpdate);
                    this.Close();
                    MessageBox.Show("Cập nhật đề thi thành công!");
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (hanhDong.Equals("add"))
            {
                try
                {
                    string txtTendeValue = txtTenDeThi.Text;
                    //string nudValue = nud.Text;
                    MonHocDTO cbMonHocValue = (MonHocDTO)cbMonHoc.SelectedItem;
                    int thoiGianLamBai = (int)numThoiGianLam.Value;
                    DeThiDTO deThiAdd = new DeThiDTO(deThiBLL.GetAutoIncrement(), cbMonHocValue.MaMonHoc,
                        txtTendeValue, DateTime.Now, DateTime.Now, DateTime.Now, thoiGianLamBai,
                        fDangNhap.nguoiDungDTO.MaNguoiDung, -1, 0, cbMonHocValue.TenMonHoc);
                    deThiControl.AddDeThi(deThiAdd);
                    this.Close();
                    MessageBox.Show("Thêm đề thi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void cbMonHoc_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
