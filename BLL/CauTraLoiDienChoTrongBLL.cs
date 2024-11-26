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
        public List<CauTraLoiDienChoTrongDTO> GetAll(int MaCauHoi)
        {
            return cauTraLoiDienChoTrongDAL.GetAll(MaCauHoi);
        }
        public int Add(CauTraLoiDienChoTrongDTO cauTraLoiDienChoTrong)
        {
            if (cauTraLoiDienChoTrongDAL.Add(cauTraLoiDienChoTrong))
            {
                return 1;
            }
            return 0;
        }
        //Write method update
        public int Update(CauTraLoiDienChoTrongDTO cauTraLoiDienChoTrong)
        {
            if (cauTraLoiDienChoTrongDAL.Update(cauTraLoiDienChoTrong))
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
        public CauTraLoiDienChoTrongDTO GetCauTraLoiByMaCauHoiAndViTri(int maCauHoi,int vitri)
        {
            return cauTraLoiDienChoTrongDAL.GetCauTraLoiByMaCauHoiAndViTri(maCauHoi,vitri);
        }

    }
}
