using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ChiTietLopBLL
    {
        public ChiTietLopDAL chiTietLopDAL;
        public ChiTietLopBLL()
        {
            chiTietLopDAL = ChiTietLopDAL.GetInstance();
        }
        public string Add(ChiTietLopDTO lop)
        {
            if (chiTietLopDAL.Add(lop))
            {
                return "thanh cong";

            }
            return "that bai";
        }
    }
}
