namespace GUI
{
    partial class fNhapInfo
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
            this.lblQuenMK = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmailorUsername = new System.Windows.Forms.TextBox();
            this.btnGui = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblQuenMK
            // 
            this.lblQuenMK.AutoSize = true;
            this.lblQuenMK.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuenMK.Location = new System.Drawing.Point(314, 43);
            this.lblQuenMK.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblQuenMK.Name = "lblQuenMK";
            this.lblQuenMK.Size = new System.Drawing.Size(238, 45);
            this.lblQuenMK.TabIndex = 0;
            this.lblQuenMK.Text = "Quên mật khẩu";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(15, 174);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(199, 21);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Nhập Tài khoản hoặc Email:";
            // 
            // txtEmailorUsername
            // 
            this.txtEmailorUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailorUsername.Location = new System.Drawing.Point(298, 171);
            this.txtEmailorUsername.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtEmailorUsername.Multiline = true;
            this.txtEmailorUsername.Name = "txtEmailorUsername";
            this.txtEmailorUsername.Size = new System.Drawing.Size(521, 39);
            this.txtEmailorUsername.TabIndex = 2;
            this.txtEmailorUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailorUsername_KeyDown);
            // 
            // btnGui
            // 
            this.btnGui.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGui.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGui.Location = new System.Drawing.Point(669, 280);
            this.btnGui.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(150, 61);
            this.btnGui.TabIndex = 3;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // fNhapInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 403);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.txtEmailorUsername);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblQuenMK);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximumSize = new System.Drawing.Size(910, 442);
            this.MinimumSize = new System.Drawing.Size(910, 442);
            this.Name = "fNhapInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quên mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuenMK;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmailorUsername;
        private System.Windows.Forms.Button btnGui;
    }
}