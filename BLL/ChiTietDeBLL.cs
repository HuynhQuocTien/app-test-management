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

        public List<CauHoiDTO> GetAllCauHoiOfDeThi(DeThiDTO DeThi)
        {
            return chiTietDeDAL.GetAllCauHoiOfDeThi(DeThi);
        }
    }
}
