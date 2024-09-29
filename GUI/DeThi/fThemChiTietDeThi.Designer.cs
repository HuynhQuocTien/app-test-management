namespace GUI.DeThi
{
    partial class fThemChiTietDeThi
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
            this.lbCauHoi = new System.Windows.Forms.ListBox();
            this.lbDeThi = new System.Windows.Forms.ListBox();
            this.btnLeftToRight = new System.Windows.Forms.Button();
            this.btnRightToLeft = new System.Windows.Forms.Button();
            this.btnLeftToRightAll = new System.Windows.Forms.Button();
            this.btnRightToLeftAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDoKho = new System.Windows.Forms.ComboBox();
            this.cbMonHoc = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbCauHoi
            // 
            this.lbCauHoi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCauHoi.FormattingEnabled = true;
            this.lbCauHoi.ItemHeight = 17;
            this.lbCauHoi.Location = new System.Drawing.Point(12, 210);
            this.lbCauHoi.Name = "lbCauHoi";
            this.lbCauHoi.Size = new System.Drawing.Size(369, 344);
            this.lbCauHoi.TabIndex = 0;
            this.lbCauHoi.DoubleClick += new System.EventHandler(this.lbCauHoi_DoubleClick);
            // 
            // lbDeThi
            // 
            this.lbDeThi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDeThi.FormattingEnabled = true;
            this.lbDeThi.ItemHeight = 17;
            this.lbDeThi.Location = new System.Drawing.Point(565, 210);
            this.lbDeThi.Name = "lbDeThi";
            this.lbDeThi.Size = new System.Drawing.Size(369, 344);
            this.lbDeThi.TabIndex = 0;
            this.lbDeThi.DoubleClick += new System.EventHandler(this.lbDeThi_DoubleClick);
            // 
            // btnLeftToRight
            // 
            this.btnLeftToRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLeftToRight.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeftToRight.Location = new System.Drawing.Point(415, 342);
            this.btnLeftToRight.Name = "btnLeftToRight";
            this.btnLeftToRight.Size = new System.Drawing.Size(55, 37);
            this.btnLeftToRight.TabIndex = 1;
            this.btnLeftToRight.Text = ">";
            this.btnLeftToRight.UseVisualStyleBackColor = true;
            this.btnLeftToRight.Click += new System.EventHandler(this.btnLeftToRight_Click_1);
            // 
            // btnRightToLeft
            // 
            this.btnRightToLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRightToLeft.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRightToLeft.Location = new System.Drawing.Point(476, 342);
            this.btnRightToLeft.Name = "btnRightToLeft";
            this.btnRightToLeft.Size = new System.Drawing.Size(55, 37);
            this.btnRightToLeft.TabIndex = 1;
            this.btnRightToLeft.Text = "<";
            this.btnRightToLeft.UseVisualStyleBackColor = true;
            this.btnRightToLeft.Click += new System.EventHandler(this.btnRightToLeft_Click_1);
            // 
            // btnLeftToRightAll
            // 
            this.btnLeftToRightAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLeftToRightAll.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeftToRightAll.Location = new System.Drawing.Point(415, 385);
            this.btnLeftToRightAll.Name = "btnLeftToRightAll";
            this.btnLeftToRightAll.Size = new System.Drawing.Size(55, 37);
            this.btnLeftToRightAll.TabIndex = 1;
            this.btnLeftToRightAll.Text = ">>";
            this.btnLeftToRightAll.UseVisualStyleBackColor = true;
            this.btnLeftToRightAll.Click += new System.EventHandler(this.btnLeftToRightAll_Click_1);
            // 
            // btnRightToLeftAll
            // 
            this.btnRightToLeftAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRightToLeftAll.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRightToLeftAll.Location = new System.Drawing.Point(476, 385);
            this.btnRightToLeftAll.Name = "btnRightToLeftAll";
            this.btnRightToLeftAll.Size = new System.Drawing.Size(55, 37);
            this.btnRightToLeftAll.TabIndex = 1;
            this.btnRightToLeftAll.Text = "<<";
            this.btnRightToLeftAll.UseVisualStyleBackColor = true;
            this.btnRightToLeftAll.Click += new System.EventHandler(this.btnRightToLeftAll_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(295, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(379, 45);
            this.label1.TabIndex = 2;
            this.label1.Text = "Thêm câu hỏi vào đề thi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Danh sách câu hỏi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(561, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(339, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Danh sách câu hỏi đã được thêm vào đề thi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Nhập nội dung câu hỏi: ";
            // 
            // txt
            // 
            this.txt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt.Location = new System.Drawing.Point(238, 82);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(504, 29);
            this.txt.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(57, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "Chọn độ khó: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(407, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 21);
            this.label6.TabIndex = 6;
            this.label6.Text = "Chọn môn học: ";
            // 
            // cbDoKho
            // 
            this.cbDoKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDoKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDoKho.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDoKho.FormattingEnabled = true;
            this.cbDoKho.Location = new System.Drawing.Point(238, 131);
            this.cbDoKho.Name = "cbDoKho";
            this.cbDoKho.Size = new System.Drawing.Size(138, 29);
            this.cbDoKho.TabIndex = 7;
            this.cbDoKho.SelectedValueChanged += new System.EventHandler(this.cbDoKho_SelectedValueChanged);
            // 
            // cbMonHoc
            // 
            this.cbMonHoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonHoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonHoc.FormattingEnabled = true;
            this.cbMonHoc.Location = new System.Drawing.Point(546, 131);
            this.cbMonHoc.Name = "cbMonHoc";
            this.cbMonHoc.Size = new System.Drawing.Size(196, 29);
            this.cbMonHoc.TabIndex = 7;
            this.cbMonHoc.SelectedValueChanged += new System.EventHandler(this.cbMonHoc_SelectedValueChanged);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(773, 82);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(88, 32);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.Location = new System.Drawing.Point(773, 129);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(88, 32);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // fThemChiTietDeThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 623);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.cbMonHoc);
            this.Controls.Add(this.cbDoKho);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRightToLeftAll);
            this.Controls.Add(this.btnLeftToRightAll);
            this.Controls.Add(this.btnRightToLeft);
            this.Controls.Add(this.btnLeftToRight);
            this.Controls.Add(this.lbDeThi);
            this.Controls.Add(this.lbCauHoi);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(962, 662);
            this.MinimumSize = new System.Drawing.Size(962, 662);
            this.Name = "fThemChiTietDeThi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm câu hỏi vào đề thi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbCauHoi;
        private System.Windows.Forms.ListBox lbDeThi;
        private System.Windows.Forms.Button btnLeftToRight;
        private System.Windows.Forms.Button btnRightToLeft;
        private System.Windows.Forms.Button btnLeftToRightAll;
        private System.Windows.Forms.Button btnRightToLeftAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDoKho;
        private System.Windows.Forms.ComboBox cbMonHoc;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
    }
}