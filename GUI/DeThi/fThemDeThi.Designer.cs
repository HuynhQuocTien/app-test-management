namespace GUI.DeThi
{
    partial class fThemDeThi
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenDeThi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblChonMonHoc = new System.Windows.Forms.Label();
            this.cbMonHoc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numThoiGianLam = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numThoiGianLam)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập tên đề thi:";
            // 
            // txtTenDeThi
            // 
            this.txtTenDeThi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDeThi.Location = new System.Drawing.Point(221, 84);
            this.txtTenDeThi.Name = "txtTenDeThi";
            this.txtTenDeThi.Size = new System.Drawing.Size(234, 29);
            this.txtTenDeThi.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(162, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 41);
            this.label2.TabIndex = 2;
            this.label2.Text = "THÊM ĐỀ THI";
            // 
            // btnThem
            // 
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(348, 236);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(108, 34);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Lưu ";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblChonMonHoc
            // 
            this.lblChonMonHoc.AutoSize = true;
            this.lblChonMonHoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChonMonHoc.Location = new System.Drawing.Point(33, 186);
            this.lblChonMonHoc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChonMonHoc.Name = "lblChonMonHoc";
            this.lblChonMonHoc.Size = new System.Drawing.Size(119, 21);
            this.lblChonMonHoc.TabIndex = 5;
            this.lblChonMonHoc.Text = "Chọn môn học: ";
            // 
            // cbMonHoc
            // 
            this.cbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonHoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonHoc.FormattingEnabled = true;
            this.cbMonHoc.Location = new System.Drawing.Point(221, 183);
            this.cbMonHoc.Margin = new System.Windows.Forms.Padding(2);
            this.cbMonHoc.Name = "cbMonHoc";
            this.cbMonHoc.Size = new System.Drawing.Size(235, 29);
            this.cbMonHoc.TabIndex = 6;
            this.cbMonHoc.SelectedValueChanged += new System.EventHandler(this.cbMonHoc_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Thời gian làm bài:";
            // 
            // numThoiGianLam
            // 
            this.numThoiGianLam.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numThoiGianLam.Location = new System.Drawing.Point(221, 137);
            this.numThoiGianLam.Name = "numThoiGianLam";
            this.numThoiGianLam.Size = new System.Drawing.Size(60, 29);
            this.numThoiGianLam.TabIndex = 8;
            // 
            // fThemDeThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 309);
            this.Controls.Add(this.numThoiGianLam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbMonHoc);
            this.Controls.Add(this.lblChonMonHoc);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTenDeThi);
            this.Controls.Add(this.label1);
            this.Name = "fThemDeThi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm đề thi";
            ((System.ComponentModel.ISupportInitialize)(this.numThoiGianLam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenDeThi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblChonMonHoc;
        private System.Windows.Forms.ComboBox cbMonHoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numThoiGianLam;
    }
}