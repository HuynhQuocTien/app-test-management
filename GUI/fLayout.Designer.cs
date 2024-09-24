using GUI.CauHoi;
using GUI.DeThi;
using GUI.LopHoc;
using GUI.MonHoc;
using GUI.Users;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class fLayout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fLayout));
            this.containerBtnPanel = new System.Windows.Forms.TableLayoutPanel();
            this.infoPanelBox = new System.Windows.Forms.TableLayoutPanel();
            this.infoOwnerPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblOwnerName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.lblOwnerRule = new System.Windows.Forms.Label();
            this.pictureOwner = new System.Windows.Forms.PictureBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnNguoiDung = new System.Windows.Forms.Button();
            this.btnMonHoc = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnCauHoi = new System.Windows.Forms.Button();
            this.btnLopHoc = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDeThi = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.homePanel = new GUI.fLanding();
            this.lopHocPanel = new GUI.LopHoc.LopHocControl();
            this.monHocPanel = new GUI.MonHoc.MonHocControl();
            this.cauHoiPanel = new GUI.CauHoi.CauHoiControl();
            this.deThiPanel = new GUI.DeThi.DeThiControl();
            this.userPanel = new GUI.Users.UsersControl();
            this.thongKePanel = new GUI.fThongKe();
            this.containerBtnPanel.SuspendLayout();
            this.infoPanelBox.SuspendLayout();
            this.infoOwnerPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOwner)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.containerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerBtnPanel
            // 
            this.containerBtnPanel.ColumnCount = 1;
            this.containerBtnPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.containerBtnPanel.Controls.Add(this.infoPanelBox, 0, 0);
            this.containerBtnPanel.Controls.Add(this.btnHome, 0, 1);
            this.containerBtnPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.containerBtnPanel.Location = new System.Drawing.Point(0, 0);
            this.containerBtnPanel.Margin = new System.Windows.Forms.Padding(10);
            this.containerBtnPanel.Name = "containerBtnPanel";
            this.containerBtnPanel.RowCount = 3;
            this.containerBtnPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.containerBtnPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.containerBtnPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.containerBtnPanel.Size = new System.Drawing.Size(330, 845);
            this.containerBtnPanel.TabIndex = 0;
            // 
            // infoPanelBox
            // 
            this.infoPanelBox.ColumnCount = 2;
            this.infoPanelBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.47401F));
            this.infoPanelBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.52599F));
            this.infoPanelBox.Controls.Add(this.infoOwnerPanel, 1, 0);
            this.infoPanelBox.Controls.Add(this.pictureOwner, 0, 0);
            this.infoPanelBox.Location = new System.Drawing.Point(0, 5);
            this.infoPanelBox.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.infoPanelBox.Name = "infoPanelBox";
            this.infoPanelBox.RowCount = 1;
            this.infoPanelBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanelBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanelBox.Size = new System.Drawing.Size(327, 105);
            this.infoPanelBox.TabIndex = 30;
            // 
            // infoOwnerPanel
            // 
            this.infoOwnerPanel.ColumnCount = 1;
            this.infoOwnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoOwnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoOwnerPanel.Controls.Add(this.lblOwnerName, 0, 0);
            this.infoOwnerPanel.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.infoOwnerPanel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.infoOwnerPanel.Location = new System.Drawing.Point(116, 0);
            this.infoOwnerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.infoOwnerPanel.Name = "infoOwnerPanel";
            this.infoOwnerPanel.RowCount = 2;
            this.infoOwnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.47619F));
            this.infoOwnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.52381F));
            this.infoOwnerPanel.Size = new System.Drawing.Size(211, 105);
            this.infoOwnerPanel.TabIndex = 1;
            // 
            // lblOwnerName
            // 
            this.lblOwnerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOwnerName.AutoSize = true;
            this.lblOwnerName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblOwnerName.Location = new System.Drawing.Point(5, 28);
            this.lblOwnerName.Margin = new System.Windows.Forms.Padding(5, 20, 0, 0);
            this.lblOwnerName.Name = "lblOwnerName";
            this.lblOwnerName.Size = new System.Drawing.Size(174, 25);
            this.lblOwnerName.TabIndex = 0;
            this.lblOwnerName.Text = "Nguyễn Anh Khoa";
            this.lblOwnerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.46392F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.53608F));
            this.tableLayoutPanel1.Controls.Add(this.btnSetting, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOwnerRule, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 56);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(205, 46);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnSetting
            // 
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.FlatAppearance.BorderSize = 0;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnSetting.Image")));
            this.btnSetting.Location = new System.Drawing.Point(137, 3);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(65, 40);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // lblOwnerRule
            // 
            this.lblOwnerRule.AutoSize = true;
            this.lblOwnerRule.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOwnerRule.Location = new System.Drawing.Point(5, 0);
            this.lblOwnerRule.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.lblOwnerRule.Name = "lblOwnerRule";
            this.lblOwnerRule.Size = new System.Drawing.Size(60, 23);
            this.lblOwnerRule.TabIndex = 2;
            this.lblOwnerRule.Text = "Admin";
            this.lblOwnerRule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureOwner
            // 
            this.pictureOwner.Image = ((System.Drawing.Image)(resources.GetObject("pictureOwner.Image")));
            this.pictureOwner.InitialImage = null;
            this.pictureOwner.Location = new System.Drawing.Point(2, 2);
            this.pictureOwner.Margin = new System.Windows.Forms.Padding(2);
            this.pictureOwner.Name = "pictureOwner";
            this.pictureOwner.Size = new System.Drawing.Size(112, 100);
            this.pictureOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureOwner.TabIndex = 0;
            this.pictureOwner.TabStop = false;
            this.pictureOwner.WaitOnLoad = true;
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.White;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(10, 115);
            this.btnHome.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnHome.MinimumSize = new System.Drawing.Size(117, 46);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnHome.Size = new System.Drawing.Size(310, 60);
            this.btnHome.TabIndex = 2;
            this.btnHome.Text = "  Trang chủ";
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnNguoiDung
            // 
            this.btnNguoiDung.BackColor = System.Drawing.Color.White;
            this.btnNguoiDung.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNguoiDung.FlatAppearance.BorderSize = 0;
            this.btnNguoiDung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNguoiDung.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnNguoiDung.Image = ((System.Drawing.Image)(resources.GetObject("btnNguoiDung.Image")));
            this.btnNguoiDung.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNguoiDung.Location = new System.Drawing.Point(10, 185);
            this.btnNguoiDung.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnNguoiDung.MinimumSize = new System.Drawing.Size(117, 46);
            this.btnNguoiDung.Name = "btnNguoiDung";
            this.btnNguoiDung.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNguoiDung.Size = new System.Drawing.Size(310, 60);
            this.btnNguoiDung.TabIndex = 7;
            this.btnNguoiDung.Text = "  Người dùng";
            this.btnNguoiDung.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNguoiDung.UseVisualStyleBackColor = false;
            this.btnNguoiDung.Click += new System.EventHandler(this.btnNguoiDung_Click);
            // 
            // btnMonHoc
            // 
            this.btnMonHoc.BackColor = System.Drawing.Color.White;
            this.btnMonHoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMonHoc.FlatAppearance.BorderSize = 0;
            this.btnMonHoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonHoc.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnMonHoc.Image = ((System.Drawing.Image)(resources.GetObject("btnMonHoc.Image")));
            this.btnMonHoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonHoc.Location = new System.Drawing.Point(10, 291);
            this.btnMonHoc.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnMonHoc.MinimumSize = new System.Drawing.Size(117, 46);
            this.btnMonHoc.Name = "btnMonHoc";
            this.btnMonHoc.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMonHoc.Size = new System.Drawing.Size(310, 60);
            this.btnMonHoc.TabIndex = 4;
            this.btnMonHoc.Text = "  Môn học";
            this.btnMonHoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMonHoc.UseVisualStyleBackColor = false;
            this.btnMonHoc.Click += new System.EventHandler(this.btnMonHoc_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.White;
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.FlatAppearance.BorderSize = 0;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnThongKe.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKe.Image")));
            this.btnThongKe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThongKe.Location = new System.Drawing.Point(10, 529);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnThongKe.MinimumSize = new System.Drawing.Size(117, 46);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnThongKe.Size = new System.Drawing.Size(310, 60);
            this.btnThongKe.TabIndex = 8;
            this.btnThongKe.Text = "  Thống kê";
            this.btnThongKe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnCauHoi
            // 
            this.btnCauHoi.BackColor = System.Drawing.Color.White;
            this.btnCauHoi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCauHoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCauHoi.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCauHoi.FlatAppearance.BorderSize = 0;
            this.btnCauHoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCauHoi.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnCauHoi.Image = ((System.Drawing.Image)(resources.GetObject("btnCauHoi.Image")));
            this.btnCauHoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCauHoi.Location = new System.Drawing.Point(10, 371);
            this.btnCauHoi.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnCauHoi.MinimumSize = new System.Drawing.Size(117, 45);
            this.btnCauHoi.Name = "btnCauHoi";
            this.btnCauHoi.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnCauHoi.Size = new System.Drawing.Size(310, 60);
            this.btnCauHoi.TabIndex = 5;
            this.btnCauHoi.Text = "  Câu hỏi";
            this.btnCauHoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCauHoi.UseVisualStyleBackColor = false;
            this.btnCauHoi.Click += new System.EventHandler(this.btnCauHoi_Click);
            // 
            // btnLopHoc
            // 
            this.btnLopHoc.BackColor = System.Drawing.Color.White;
            this.btnLopHoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLopHoc.FlatAppearance.BorderSize = 0;
            this.btnLopHoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLopHoc.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnLopHoc.Image = ((System.Drawing.Image)(resources.GetObject("btnLopHoc.Image")));
            this.btnLopHoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLopHoc.Location = new System.Drawing.Point(10, 212);
            this.btnLopHoc.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnLopHoc.MinimumSize = new System.Drawing.Size(117, 46);
            this.btnLopHoc.Name = "btnLopHoc";
            this.btnLopHoc.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLopHoc.Size = new System.Drawing.Size(310, 60);
            this.btnLopHoc.TabIndex = 3;
            this.btnLopHoc.Text = "  Lớp học";
            this.btnLopHoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLopHoc.UseVisualStyleBackColor = false;
            this.btnLopHoc.Click += new System.EventHandler(this.btnLopHoc_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.White;
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnThoat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThoat.Location = new System.Drawing.Point(10, 767);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnThoat.MinimumSize = new System.Drawing.Size(117, 46);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnThoat.Size = new System.Drawing.Size(310, 60);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "  Đăng xuất";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnDeThi
            // 
            this.btnDeThi.BackColor = System.Drawing.Color.White;
            this.btnDeThi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeThi.FlatAppearance.BorderSize = 0;
            this.btnDeThi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeThi.Font = new System.Drawing.Font("Segoe UI", 13.2F);
            this.btnDeThi.Image = ((System.Drawing.Image)(resources.GetObject("btnDeThi.Image")));
            this.btnDeThi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeThi.Location = new System.Drawing.Point(10, 451);
            this.btnDeThi.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnDeThi.MinimumSize = new System.Drawing.Size(117, 46);
            this.btnDeThi.Name = "btnDeThi";
            this.btnDeThi.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDeThi.Size = new System.Drawing.Size(310, 60);
            this.btnDeThi.TabIndex = 6;
            this.btnDeThi.Text = "  Đề thi";
            this.btnDeThi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeThi.UseVisualStyleBackColor = false;
            this.btnDeThi.Click += new System.EventHandler(this.btnDeThi_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.containerPanel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(330, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.81897F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.18103F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1210, 845);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // containerPanel
            // 
            this.containerPanel.Controls.Add(this.homePanel);
            this.containerPanel.Controls.Add(this.lopHocPanel);
            this.containerPanel.Controls.Add(this.monHocPanel);
            this.containerPanel.Controls.Add(this.cauHoiPanel);
            this.containerPanel.Controls.Add(this.deThiPanel);
            this.containerPanel.Controls.Add(this.userPanel);
            this.containerPanel.Controls.Add(this.thongKePanel);
            this.containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerPanel.Location = new System.Drawing.Point(0, 0);
            this.containerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(1210, 845);
            this.containerPanel.TabIndex = 0;
            // 
            // homePanel
            // 
            this.homePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homePanel.Location = new System.Drawing.Point(0, 0);
            this.homePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.homePanel.Name = "homePanel";
            this.homePanel.Size = new System.Drawing.Size(1210, 845);
            this.homePanel.TabIndex = 0;
            // 
            // lopHocPanel
            // 
            this.lopHocPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lopHocPanel.Location = new System.Drawing.Point(0, 0);
            this.lopHocPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lopHocPanel.Name = "lopHocPanel";
            this.lopHocPanel.Size = new System.Drawing.Size(1210, 845);
            this.lopHocPanel.TabIndex = 1;
            // 
            // monHocPanel
            // 
            this.monHocPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monHocPanel.Location = new System.Drawing.Point(0, 0);
            this.monHocPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.monHocPanel.Name = "monHocPanel";
            this.monHocPanel.Size = new System.Drawing.Size(1210, 845);
            this.monHocPanel.TabIndex = 2;
            // 
            // cauHoiPanel
            // 
            this.cauHoiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cauHoiPanel.Location = new System.Drawing.Point(0, 0);
            this.cauHoiPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cauHoiPanel.Name = "cauHoiPanel";
            this.cauHoiPanel.Size = new System.Drawing.Size(1210, 845);
            this.cauHoiPanel.TabIndex = 3;
            // 
            // deThiPanel
            // 
            this.deThiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deThiPanel.Location = new System.Drawing.Point(0, 0);
            this.deThiPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deThiPanel.Name = "deThiPanel";
            this.deThiPanel.Size = new System.Drawing.Size(1210, 845);
            this.deThiPanel.TabIndex = 4;
            // 
            // userPanel
            // 
            this.userPanel.AutoSize = true;
            this.userPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userPanel.Location = new System.Drawing.Point(0, 0);
            this.userPanel.Margin = new System.Windows.Forms.Padding(4);
            this.userPanel.Name = "userPanel";
            this.userPanel.Size = new System.Drawing.Size(1210, 845);
            this.userPanel.TabIndex = 6;
            // 
            // thongKePanel
            // 
            this.thongKePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thongKePanel.Location = new System.Drawing.Point(0, 0);
            this.thongKePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.thongKePanel.Name = "thongKePanel";
            this.thongKePanel.Size = new System.Drawing.Size(1210, 845);
            this.thongKePanel.TabIndex = 4;
            // 
            // UserForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.containerBtnPanel);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(700, 550);
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserForm_FormClosed);
            this.Load += new System.EventHandler(this.UserForm_Load_1);
            this.containerBtnPanel.ResumeLayout(false);
            this.infoPanelBox.ResumeLayout(false);
            this.infoOwnerPanel.ResumeLayout(false);
            this.infoOwnerPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOwner)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.containerPanel.ResumeLayout(false);
            this.containerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TableLayoutPanel containerBtnPanel;
        private Button btnCauHoi;
        private Button btnHome;
        private Button btnDeThi;
        private Button btnThongKe;
        private Button btnLopHoc;
        private Button btnThoat;
        private Button btnMonHoc;


        private Color hoverColor = ColorTranslator.FromHtml("#8eddf9");
        private Color defaultTitleBtnColor = ColorTranslator.FromHtml("#646568");
        private Color borderColor = ColorTranslator.FromHtml("#e9edee");
        private TableLayoutPanel infoPanelBox;
        private PictureBox pictureOwner;
        private TableLayoutPanel infoOwnerPanel;
        private Label lblOwnerName;
        private TableLayoutPanel tableLayoutPanel2;
        private fLanding homePanel;
        private LopHocControl lopHocPanel;
        private MonHocControl monHocPanel;
        private CauHoiControl cauHoiPanel;
        private DeThiControl deThiPanel;
        private Panel containerPanel;
        private Button btnNguoiDung;
        private UsersControl userPanel;
        private fThongKe thongKePanel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblOwnerRule;
        private Button btnSetting;
    }
}