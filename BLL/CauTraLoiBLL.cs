using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauTraLoiBLL
    {
        private CauTraLoiDAL CauTraLoiDAL;
        public CauTraLoiBLL()
        {
            CauTraLoiDAL = new CauTraLoiDAL();
        }
        public List<CauTraLoiDTO> GetAll()
        {
            return CauTraLoiDAL.GetAll();
        }
        public int Add(CauTraLoiDTO cautraloi)
        {
            if (CauTraLoiDAL.Add(cautraloi))
            {
                return 1;
            }
            return 0;
        }

        public CauTraLoiDTO GetCauTraLoiById(int maCauTraLoi)
        {
            CauTraLoiDTO CauTraLoi = new CauTraLoiDTO();
            CauTraLoi.MaCauTL = maCauTraLoi;
            return CauTraLoiDAL.GetById(CauTraLoi);
        }
    }
}
