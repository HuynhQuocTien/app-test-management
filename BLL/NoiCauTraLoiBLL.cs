using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NoiCauTraLoiBLL
    {
        private NoiCauTraLoiDAL NoiCauTraLoiDAL;
        public NoiCauTraLoiBLL()
        {
            NoiCauTraLoiDAL = new NoiCauTraLoiDAL();
        }
        public List<NoiCauTraLoiDTO> GetAll(int maCauHoi)
        {
            return NoiCauTraLoiDAL.GetAll(maCauHoi);
        }
        public bool Add(NoiCauTraLoiDTO noicautraloi)
        {
            if (NoiCauTraLoiDAL.Add(noicautraloi))
            {
                return true;
            }
            return false;
        }

        public NoiCauTraLoiDTO GetMonHocById(int MaCauTraLoi)
        {
            NoiCauTraLoiDTO noiCau = new NoiCauTraLoiDTO();
            noiCau.MaCauTraLoi = MaCauTraLoi;
            return NoiCauTraLoiDAL.GetById(noiCau);
        }

        public bool Delete(int MaCauNoi)
        {
            if (NoiCauTraLoiDAL.Delete(MaCauNoi))
            {
                return true;
            }
            return false;
        }

        public bool Update(NoiCauTraLoiDTO noicautraloi)
        {
            if (NoiCauTraLoiDAL.Update(noicautraloi))
            {
                return true;
            }
            return false;
        }
    }
}
