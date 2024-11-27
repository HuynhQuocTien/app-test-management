using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class fNhapOTP : Form
    {
        private string email;
        private int otp;
        public fNhapOTP(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        internal void Showdialog()
        {
            throw new NotImplementedException();
        }

        private void btnGuilaiOTP_Click(object sender, EventArgs e)
        {
            this.otp = sendOTP();
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // otp toString để so sánh
            string otp = this.otp.ToString();
            if (otp.Equals(txtNhapMa.Text))
            {
                fMatKhauMoi form = new fMatKhauMoi(this.email);
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai OTP", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private int sendOTP()
        {
            try
            {
                //ptest8867@gmail.com
                //tranducan1
                Random random = new Random();
                int otp = random.Next(1000, 10000);
                //MessageBox.Show(otp + "", "Mã OTP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var fromAddress = new MailAddress("ptest8867@gmail.com"); //Mail dùng để gửi OTP
                var toAddress = new MailAddress(email); // Mail Nhận
                const string frompass = "wkytqnqfiymgdmyk";
                const string subject = "OTP code";
                string body = otp.ToString();

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return otp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        private void fNhapOTP_Load(object sender, EventArgs e)
        {
            this.otp = sendOTP();
        }
    }
}
