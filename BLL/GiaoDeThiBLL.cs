﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GiaoDeThiBLL
    {
        private GiaoDeThiDAL giaoDeThiDAL;
        public GiaoDeThiBLL()
        {
            giaoDeThiDAL = GiaoDeThiDAL.getInstance();
        }
    }
}
