﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public  class PhanCongBLL
    {
        private PhanCongDAL phanCongDAL;
        public PhanCongBLL()
        {
            phanCongDAL = PhanCongDAL.getInstance();
        }
    }
}
