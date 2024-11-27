using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public bool Add(ChiTietLopDTO lop)
        {
            if (chiTietLopDAL.Add(lop))
            {
                return true;

            }
            return false;
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
        public bool IsStudentInClass(long maSV, int maLop)
        {
           List<NguoiDungDTO> students = chiTietLopDAL.GetSV(maLop) ?? null;
            if (students == null) return false;
            foreach (NguoiDungDTO student in students)
            {
                if (student.MaNguoiDung == maSV) return true;
            }
            return false;
        }
        

    }
}
