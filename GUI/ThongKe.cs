﻿using BLL;
using DTO;
using System;
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
    public partial class ThongKe: Form
    {
        private StatisticBLL _statisticBLL = new StatisticBLL();
        public ThongKe()
        {
            InitializeComponent();

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            
            fThongKe fThongKe = new fThongKe();
            this.Controls.Add(fThongKe);
        }
    }
}
