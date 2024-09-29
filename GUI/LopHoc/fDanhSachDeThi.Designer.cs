namespace GUI.LopHoc
{
    partial class fDanhSachDeThi
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
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.headingPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeading = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblTenDeThi = new System.Windows.Forms.Label();
            this.lblMonHoc = new System.Windows.Forms.Label();
            this.txtDeThi = new System.Windows.Forms.TextBox();
            this.cbMonHoc = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.mainPanel.SuspendLayout();
            this.headingPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 1;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainPanel.Controls.Add(this.headingPanel, 0, 0);
            this.mainPanel.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 2;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.7366F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.2634F));
            this.mainPanel.Size = new System.Drawing.Size(1283, 478);
            this.mainPanel.TabIndex = 0;
            // 
            // headingPanel
            // 
            this.headingPanel.ColumnCount = 2;
            this.headingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.31975F));
            this.headingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.68025F));
            this.headingPanel.Controls.Add(this.lblHeading, 0, 0);
            this.headingPanel.Controls.Add(this.searchPanel, 1, 0);
            this.headingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headingPanel.Location = new System.Drawing.Point(3, 2);
            this.headingPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.headingPanel.Name = "headingPanel";
            this.headingPanel.RowCount = 1;
            this.headingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.headingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.headingPanel.Size = new System.Drawing.Size(1277, 109);
            this.headingPanel.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.AllowDrop = true;
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(11, 10);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(11, 10, 3, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(468, 54);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Danh sách đề thi đã tạo";
            // 
            // searchPanel
            // 
            this.searchPanel.ColumnCount = 3;
            this.searchPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.81227F));
            this.searchPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.18773F));
            this.searchPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.searchPanel.Controls.Add(this.lblTenDeThi, 0, 0);
            this.searchPanel.Controls.Add(this.lblMonHoc, 0, 1);
            this.searchPanel.Controls.Add(this.txtDeThi, 1, 0);
            this.searchPanel.Controls.Add(this.cbMonHoc, 1, 1);
            this.searchPanel.Controls.Add(this.btnTimKiem, 2, 1);
            this.searchPanel.Controls.Add(this.button1, 2, 0);
            this.searchPanel.Location = new System.Drawing.Point(543, 2);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.RowCount = 2;
            this.searchPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.searchPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.searchPanel.Size = new System.Drawing.Size(731, 101);
            this.searchPanel.TabIndex = 1;
            // 
            // lblTenDeThi
            // 
            this.lblTenDeThi.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTenDeThi.AutoSize = true;
            this.lblTenDeThi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDeThi.Location = new System.Drawing.Point(3, 11);
            this.lblTenDeThi.Name = "lblTenDeThi";
            this.lblTenDeThi.Size = new System.Drawing.Size(105, 28);
            this.lblTenDeThi.TabIndex = 0;
            this.lblTenDeThi.Text = "Tên đề thi: ";
            // 
            // lblMonHoc
            // 
            this.lblMonHoc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMonHoc.AutoSize = true;
            this.lblMonHoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonHoc.Location = new System.Drawing.Point(3, 61);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new System.Drawing.Size(99, 28);
            this.lblMonHoc.TabIndex = 0;
            this.lblMonHoc.Text = "Môn học: ";
            // 
            // txtDeThi
            // 
            this.txtDeThi.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDeThi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeThi.Location = new System.Drawing.Point(143, 8);
            this.txtDeThi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDeThi.Name = "txtDeThi";
            this.txtDeThi.Size = new System.Drawing.Size(399, 34);
            this.txtDeThi.TabIndex = 1;
            this.txtDeThi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeThi_KeyPress);
            // 
            // cbMonHoc
            // 
            this.cbMonHoc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbMonHoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonHoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonHoc.FormattingEnabled = true;
            this.cbMonHoc.Location = new System.Drawing.Point(143, 57);
            this.cbMonHoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbMonHoc.Name = "cbMonHoc";
            this.cbMonHoc.Size = new System.Drawing.Size(399, 36);
            this.cbMonHoc.TabIndex = 2;
            this.cbMonHoc.SelectedValueChanged += new System.EventHandler(this.cbMonHoc_SelectedValueChanged);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(581, 54);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(113, 43);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(581, 3);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 43);
            this.button1.TabIndex = 3;
            this.button1.Text = "Làm mới";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 115);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1277, 361);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // fDanhSachDeThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 478);
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fDanhSachDeThi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách đề thi";
            this.mainPanel.ResumeLayout(false);
            this.headingPanel.ResumeLayout(false);
            this.headingPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.TableLayoutPanel headingPanel;
        private System.Windows.Forms.TableLayoutPanel searchPanel;
        private System.Windows.Forms.Label lblTenDeThi;
        private System.Windows.Forms.Label lblMonHoc;
        private System.Windows.Forms.TextBox txtDeThi;
        private System.Windows.Forms.ComboBox cbMonHoc;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Button button1;
    }
}