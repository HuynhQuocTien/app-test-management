using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauTraLoiDienChoTrongBLL
    {
        private CauTraLoiDienChoTrongDAL cauTraLoiDienChoTrongDAL;
        public CauTraLoiDienChoTrongBLL()
        {
            cauTraLoiDienChoTrongDAL = new CauTraLoiDienChoTrongDAL();
        }
        public List<CauTraLoiDienChoTrongDTO> GetAll()
        {
            return cauTraLoiDienChoTrongDAL.GetAll();
        }
        public int Add(CauTraLoiDienChoTrongDTO cauTraLoiDienChoTrong)
        {
            if (cauTraLoiDienChoTrongDAL.Add(cauTraLoiDienChoTrong))
            {
                return 1;
            }
            return 0;
        }

        public CauTraLoiDienChoTrongDTO GetCauTraLoiById(int maCauTraLoi)
        {
            CauTraLoiDienChoTrongDTO CauTraLoiDienChoTrong = new CauTraLoiDienChoTrongDTO();
            CauTraLoiDienChoTrong.MaCauTLiDienChoTrong = maCauTraLoi;
            return cauTraLoiDienChoTrongDAL.GetById(CauTraLoiDienChoTrong);
        }
    }
}
