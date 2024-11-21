﻿using BLL;
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
            LoadMonHoc();
            monHocBLL = new MonHocBLL();
            deThiControl = fdethi;
            this.hanhDong = hanhDong;
            this.deThiUpdate = dethi;
            deThiBLL = new DeThiBLL();
            if (hanhDong.Equals("edit"))
            {
                deThiUpdate = dethi;
                txtTenDeThi.Text = deThiUpdate.TenDe;
                nud.Text = (deThiUpdate.ThoiGianKetThuc - deThiUpdate.ThoiGianBatDau).TotalMinutes.ToString();
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
                    DeThiDTO objUpdate = new DeThiDTO(deThiUpdate.MaDe, cbMonHocValue.MaMonHoc, txtTenDeThi.Text, deThiUpdate.ThoiGianTao,
                        deThiUpdate.ThoiGianBatDau, deThiUpdate.ThoiGianBatDau.AddMinutes(Convert.ToInt32(nud.Text)),
                        fDangNhap.nguoiDungDTO.MaNguoiDung, 1, 0, cbMonHocValue.TenMonHoc);


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
                    string nudValue = nud.Text;
                    MonHocDTO cbMonHocValue = (MonHocDTO)cbMonHoc.SelectedItem;
                    DeThiDTO deThiAdd = new DeThiDTO(deThiBLL.GetAutoIncrement(), cbMonHocValue.MaMonHoc,
                        txtTendeValue, DateTime.Now, DateTime.Now, DateTime.Now.AddMinutes(Convert.ToInt32(nud.Text)),
                        fDangNhap.nguoiDungDTO.MaNguoiDung, 1, 0, cbMonHocValue.TenMonHoc);
                    deThiControl.AddDeThi(deThiAdd);
                    this.Close();
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
