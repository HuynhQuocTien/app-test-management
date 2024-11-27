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
        public List<NguoiDungDTO> GetSV(int maLop)
        {
            //NguoiDungDTO nguoiDung = new NguoiDungDTO();
            return chiTietLopDAL.GetSV(maLop);
        }

        public int xemSLDeThi(int maLop)
        {
            return chiTietLopDAL.XemSLDeThi(maLop);
        }
        public int xemSLDeThiHoatDong(int maLop)
        {
            return chiTietLopDAL.XemSLDeThiHoatDong(maLop);
        }
    }
}
