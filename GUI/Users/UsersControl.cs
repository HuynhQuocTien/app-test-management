using BLL;
using DAL;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GUI.Users
{
    public partial class UsersControl : UserControl
    {
        private Panel[] panelUser;
        private PictureBox[] avatarImg;
        private Button[] buttonCT;
        private Button[] buttonDELETE;
        private TextBox[] textBoxDate;
        private TextBox[] textBoxRole;
        private TextBox[] textBoxName;

        NguoiDungBLL nguoiDungBLL;
        TaiKhoanDAL taiKhoanDAL;
        private List<NguoiDungDTO> getListNguoiDung()
        {
            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            return nguoiDungBLL.GetAllNguoiDung(); 
        }

        public UsersControl()
        {
            InitializeComponent();
            renderUsers();
        }

        private void Delete_MouseClick(object sender, MouseEventArgs e, NguoiDungDTO nguoiDung)
        {
            if (MessageBox.Show("Bạn có muốn xóa người dùng không?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();

                if (nguoiDungBLL.Delete(nguoiDung))
                {
                    MessageBox.Show("Xóa người dùng thành công.");
                    renderUsers();
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thất bại.");
                }
            }
        }
        private void Detail_MouseClick(object sender, MouseEventArgs e, NguoiDungDTO nguoiDung)
        {

            //InfoUser infoNguoiDung = new InfoUser(nguoiDung);
            //InfoUser infoNguoiDung = new InfoUser(nguoiDung);
            InfoUser infoNguoiDung = new InfoUser();
            infoNguoiDung.getNguoiDungId(nguoiDung);
            infoNguoiDung.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser(this);
            addUser.ShowDialog();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        public void AddNguoiDung(NguoiDungDTO objND, TaiKhoanDTO objTK)
        {

            //MessageBox.Show("Meo");
            MessageBox.Show(objND.ToString());

            MessageBox.Show(objTK.ToString());

            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            nguoiDungBLL.Add(objND);
            taiKhoanDAL.Add(objTK);
            renderUsers();
        }




        private void renderUsers()
        {
            flowLayoutContainer.Controls.Clear();

            var nguoiDungs = getListNguoiDung();
            int nguoiDungCount = nguoiDungs.Count;


            panelUser = new Panel[nguoiDungCount];
            buttonCT = new Button[nguoiDungCount];
            buttonDELETE = new Button[nguoiDungCount];
            textBoxDate = new TextBox[nguoiDungCount];
            textBoxRole = new TextBox[nguoiDungCount];
            textBoxName = new TextBox[nguoiDungCount];
            avatarImg = new PictureBox[nguoiDungCount];

            for (int i = 0; i < nguoiDungCount; i++)
            {
                var nguoiDung = nguoiDungs[i];

                panelUser[i] = new Panel();
                panelUser[i].Name = "panelUser" + i;
                panelUser[i].Size = new Size(385, 150);
                panelUser[i].BorderStyle = BorderStyle.FixedSingle;
                // 
                // buttonCT
                // 
                buttonCT[i] = new Button();
                buttonCT[i].Location = new Point(202, 105);
                buttonCT[i].Name = "buttonCT" + i;
                buttonCT[i].Size = new Size(75, 30);
                buttonCT[i].Tag = "id1";
                buttonCT[i].Text = "Chi tiết";
                buttonCT[i].UseVisualStyleBackColor = true;
                //buttonCT[i].MouseClick += Detail_MouseClick;
                buttonCT[i].MouseClick += (sender, e) => Detail_MouseClick(sender, e, nguoiDung);

                buttonCT[i].Cursor = Cursors.Hand;
                // 
                // buttonDELETE
                // 
                buttonDELETE[i] = new Button();
                buttonDELETE[i].Location = new Point(283, 105);
                buttonDELETE[i].Name = "buttonDELETE" + i;
                buttonDELETE[i].Size = new Size(75, 30);
                buttonDELETE[i].Text = "Xóa";
                buttonDELETE[i].Tag = "id1";
                buttonDELETE[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonDELETE[i].UseVisualStyleBackColor = true;
                //buttonDELETE[i].MouseClick += Delete_MouseClick;
                buttonDELETE[i].MouseClick += (sender, e) => Delete_MouseClick(sender, e, nguoiDung);
                buttonDELETE[i].Cursor = Cursors.Hand;


             
                // 
                // textBoxDate
                // 
                textBoxDate[i] = new TextBox();
                textBoxDate[i].BackColor = SystemColors.Control;
                textBoxDate[i].Location = new Point(149, 76);
                textBoxDate[i].Name = "textBoxDate" + i;
                textBoxDate[i].Size = new Size(209, 23);
                textBoxDate[i].Text = nguoiDung.NgaySinh.ToString("dd/MM/yyyy");
                textBoxDate[i].Enabled = false;
                // 
                // textBoxRole
                // 
                textBoxRole[i] = new TextBox();
                textBoxRole[i].BackColor = SystemColors.Control;
                textBoxRole[i].Location = new Point(149, 47);
                textBoxRole[i].Name = "textBoxRole" + i;
                textBoxRole[i].Size = new Size(209, 23);
                textBoxRole[i].Enabled = false;
                textBoxRole[i].Text += "Admin";


                // 
                // textBoxName
                // 
                textBoxName[i] = new TextBox();
                textBoxName[i].BackColor = SystemColors.Control;
                textBoxName[i].Location = new Point(149, 18);
                textBoxName[i].Name = "textBoxName" + i;
                textBoxName[i].Size = new Size(209, 23);
                textBoxName[i].Text = nguoiDung.HoTen;
                textBoxName[i].Enabled = false;
                // 
                // avatarImg
                // 
                avatarImg[i] = new PictureBox();
                //avatarImg[i].ImageLocation = "Link avt";
                avatarImg[i].Location = new Point(22, 18);
                avatarImg[i].Name = "avatarImg" + i;
                avatarImg[i].Size = new Size(100, 113);
                avatarImg[i].TabStop = false;
                avatarImg[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                avatarImg[i].ImageLocation = nguoiDung.Avatar;


                panelUser[i].Controls.Add(buttonCT[i]);
                panelUser[i].Controls.Add(buttonDELETE[i]);
                panelUser[i].Controls.Add(textBoxDate[i]);
                panelUser[i].Controls.Add(textBoxRole[i]);
                panelUser[i].Controls.Add(textBoxName[i]);
                panelUser[i].Controls.Add(avatarImg[i]);
                flowLayoutContainer.Controls.Add(panelUser[i]);

            }


        }
    }
}
