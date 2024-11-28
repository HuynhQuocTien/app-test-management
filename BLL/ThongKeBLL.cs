using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class ThongKeBLL
    {
        private ThongKeDAL _thongKeDAL;

        public ThongKeBLL()
        {
            _thongKeDAL = ThongKeDAL.GetInstance();
        }
        public int GetSoLuongDeThi(int maLop)
        {
            return _thongKeDAL.GetSoLuongDeThi(maLop);
        }
        public double getDiemCuaDeThiByUserId(int maLop, int maDeThi, long userId)
        {
            return _thongKeDAL.getDiemCuaDeThiByUserId(maLop, maDeThi, userId);
        }

        public int getSlHSDaNopBai(int maLop, int maDeThi)
        {
            return _thongKeDAL.getSlHsDaNopBai(maLop, maDeThi);
        }
        public int getCountHs()
        {
            return _thongKeDAL.getCountHs();
        }
        public int getCountCauHoi()
        {
            return _thongKeDAL.getCountCauHoi();
        }

        public int getCountGv()
        {
            return _thongKeDAL.getCountGv();

        }
        public Dictionary<NguoiDungDTO, KetQuaDTO> GetAllDiemTBCuaHs(int maLop)
        {
            return _thongKeDAL.GetAllDiemTBCuaHs(maLop);
        }
        public int getSlHsDaNopBai(int maLop, int maDeThi)
        {
            return _thongKeDAL.getSlHsDaNopBai(maLop,maDeThi);
        }
        public Dictionary<NguoiDungDTO, KetQuaDTO> GetTop5HsCoDiemCaoNhatTheoDeThi(int maLop, int maDeThi)
        {
            return _thongKeDAL.GetTop5HsCoDiemCaoNhatTheoDeThi(maLop , maDeThi);
        }
    }
}
