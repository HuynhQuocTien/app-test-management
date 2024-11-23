using System.Windows.Forms;

namespace GUI
{
    partial class fXemdapan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel pnlMain;
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
            this.pnlMain = new Panel();

            // 
            // pnlMain
            // 
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.AutoScroll = true; // Cho phép cuộn nội dung
            this.pnlMain.AutoSize = true;   // Kích thước tự động
            this.pnlMain.AutoSizeMode = AutoSizeMode.GrowAndShrink; // Tự động điều chỉnh kích thước
            this.pnlMain.Padding = new Padding(10);

            // 
            // fXemdapan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMain); // Thêm Panel vào Form
            this.Text = "Xem Đáp án";
            this.StartPosition = FormStartPosition.CenterScreen; // Đặt Form nằm giữa màn hình

            this.ResumeLayout(false);
        }


        #endregion
    }
}