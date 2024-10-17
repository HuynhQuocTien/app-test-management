namespace GUI
{
    partial class fNhapOTP
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
            this.components = new System.ComponentModel.Container();
            this.txtNhapMa = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnGuilaiOTP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNhapMa
            // 
            this.txtNhapMa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapMa.Location = new System.Drawing.Point(10, 53);
            this.txtNhapMa.Name = "txtNhapMa";
            this.txtNhapMa.Size = new System.Drawing.Size(204, 29);
            this.txtNhapMa.TabIndex = 0;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.Location = new System.Drawing.Point(10, 104);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(122, 37);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(6, 27);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(80, 21);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Nhập OTP";
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(249, 45);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(107, 46);
            this.lblTime.TabIndex = 4;
            // 
            // btnGuilaiOTP
            // 
            this.btnGuilaiOTP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuilaiOTP.Location = new System.Drawing.Point(238, 49);
            this.btnGuilaiOTP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGuilaiOTP.Name = "btnGuilaiOTP";
            this.btnGuilaiOTP.Size = new System.Drawing.Size(118, 37);
            this.btnGuilaiOTP.TabIndex = 5;
            this.btnGuilaiOTP.Text = "Gửi lại OTP";
            this.btnGuilaiOTP.UseVisualStyleBackColor = true;
            this.btnGuilaiOTP.Click += new System.EventHandler(this.btnGuilaiOTP_Click);
            // 
            // fNhapOTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 183);
            this.Controls.Add(this.btnGuilaiOTP);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtNhapMa);
            this.MaximumSize = new System.Drawing.Size(440, 222);
            this.MinimumSize = new System.Drawing.Size(440, 222);
            this.Name = "fNhapOTP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập OTP";
            this.Load += new System.EventHandler(this.fNhapOTP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNhapMa;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnGuilaiOTP;
    }
}