using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DeThiBLL
    {
        public DeThiDAL deThiDAL;
        public DeThiBLL() {
            deThiDAL = DeThiDAL.getInstance();
        }

        public bool DeleteByMaDeThi(LopDTO lop, DeThiDTO deThi)
        {
            return deThiDAL.DeleteByMaDeThi(lop, deThi);
        }

        public List<DeThiDTO> GetAllDeThiCuaLop(LopDTO lop)
        {
           return deThiDAL.GetAllDeThiCuaLop(lop);
        }

        public DeThiDTO GetById(DeThiDTO deThi)
        {
            return deThiDAL.GetById(deThi);
        }
        public List<MonHocDTO> TenMonHoc()
        {
            List<MonHocDTO> dsMonHoc = deThiDAL.LayTenMonHoc();
            return dsMonHoc;
        }

        public bool Add(DeThiDTO dethidto)
        {
            return deThiDAL.Add(dethidto);
        }
        public bool Update(DeThiDTO d)
        {
            return deThiDAL.Update(d);
        }
        public List<DeThiDTO> getDeThiByMaGV(long maGV)
        {
            return deThiDAL.GetAll(maGV);
        }
        public bool Delete(DeThiDTO deThi)
        {
            return deThiDAL.Delete(deThi);
        }
        public int GetAutoIncrement()
        {
            return deThiDAL.GetAutoIncrement();
        }
        public bool checkDeThiCoTrongLop(int MaDe, int MaLop)
        {
            if (deThiDAL.CheckDeThiCoTrongLop(MaDe, MaLop) >= 1)
            {
                return true;
            }
            return false;
        }

    }
}
