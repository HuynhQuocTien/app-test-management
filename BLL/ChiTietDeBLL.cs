using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ChiTietDeBLL
    {
        private ChiTietDeDAL chiTietDeDAL;
        public ChiTietDeBLL()
        {
            chiTietDeDAL = ChiTietDeDAL.getInstance();
        }
        public bool Add(ChiTietDeDTO chiTietDeDTO)
        {
            return chiTietDeDAL.Add(chiTietDeDTO);
        }
        public bool Delete(ChiTietDeDTO chiTietDe)
        {
            return chiTietDeDAL.Delete(chiTietDe);
        }
        public List<CauHoiDTO> LayDoKho()
        {
            return chiTietDeDAL.LayDoKho();
        }
        public List<CauHoiDTO> GetCauHoiChuaThem(CauHoiDTO cauHoiDTO)
        {
            return chiTietDeDAL.GetCauHoiChuaThem(cauHoiDTO);
        }
        public List<CauHoiDTO> layCauHoiChuaThem(CauHoiDTO cauhoiDTO, int maDeThi)
        {
            return chiTietDeDAL.layCauHoiChuaThem(cauhoiDTO, maDeThi);
        }
        public List<CauHoiDTO> GetCauHoiByMaDe(int made)
        {
            return chiTietDeDAL.GetCauHoiListByMaDe(made);
        }
        public List<CauHoiDTO> GetAllCauHoiOfDeThi(DeThiDTO DeThi)
        {
            return chiTietDeDAL.GetAllCauHoiOfDeThi(DeThi);
        }
        //Write count SoCauHoi to DeThiDTO
        public int CountSoCauHoi(DeThiDTO deThi)
        {
            return chiTietDeDAL.CountSoCauHoi(deThi);
        }
    }
}
