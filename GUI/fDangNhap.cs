﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class fDangNhap : Form
    {
        private Size formOriginalSize;
        private Rectangle recLab1;
        private Rectangle recLab2;
        private Rectangle recLab3;
        private Rectangle recLab4;
        private Rectangle recBut1;
        private Rectangle recTxt1;
        private Rectangle recTxt2;
        private Rectangle recCBox1;
        public fDangNhap()
        {
            InitializeComponent();
            this.Resize += Form1_Resiz;
            formOriginalSize = this.Size;
            recLab1 = new Rectangle(label1.Location, label1.Size);
            recLab2 = new Rectangle(label2.Location, label2.Size);
            recLab3 = new Rectangle(label3.Location, label3.Size);
            recLab4 = new Rectangle(label4.Location, label4.Size);
            recBut1 = new Rectangle(button1.Location, button1.Size);
            recTxt1 = new Rectangle(textBox1.Location, textBox1.Size);
            recTxt2 = new Rectangle(textBox2.Location, textBox2.Size);
            recCBox1 = new Rectangle(checkBox1.Location, checkBox1.Size);
            textBox1.Multiline = true;
            textBox2.Multiline = true;
        }
        private void Form1_Resiz(object sender, EventArgs e)
        {
            resize_Control(button1, recBut1);
            resize_Control(textBox1, recTxt1);
            resize_Control(textBox2, recTxt2);
            resize_Control(label1, recLab1);
            resize_Control(label2, recLab2);
            resize_Control(label3, recLab3);
            resize_Control(label4, recLab4);
            resize_Control(checkBox1, recCBox1);
        }

        private void resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
            float newFontSize = c.Font.Size * Math.Min(xRatio, yRatio);
            if (newFontSize > 20)
            {
                newFontSize = 20; // Keep the maximum font size
            }
            else if (newFontSize < 6)
            {
                newFontSize = 6; // Keep the minimum font size
            }
            c.Font = new Font(c.Font.FontFamily, newFontSize);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }
        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }
        private void lblQuenMatKhau_Click(object sender, EventArgs e)
        {

        }
    }
}