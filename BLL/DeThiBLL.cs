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
    }
}
