using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauTraLoiDienChoTrongDaLamBLL
    {
        private CauTraLoiDienChoTrongDaLamDAL cauTraLoiDienChoTrongDaLamDAL;
        public CauTraLoiDienChoTrongDaLamBLL()
        {
            cauTraLoiDienChoTrongDaLamDAL = CauTraLoiDienChoTrongDaLamDAL.getInstance();
        }
        public List<CauTraLoiDienChoTrongDaLamDTO> GetAll(int MaCauHoi)
        {
            return cauTraLoiDienChoTrongDaLamDAL.GetAll(MaCauHoi);
        }
        public int Add(CauTraLoiDienChoTrongDaLamDTO cauTraLoiDienChoTrongDaLam)
        {
            if (cauTraLoiDienChoTrongDaLamDAL.Add(cauTraLoiDienChoTrongDaLam))
            {
                return 1;
            }
            return 0;
        }
        public CauTraLoiDienChoTrongDaLamDTO GetCauTraLoiById(int maCauTraLoi)
        {
            CauTraLoiDienChoTrongDaLamDTO CauTraLoiDienChoTrongDaLam = new CauTraLoiDienChoTrongDaLamDTO();
            CauTraLoiDienChoTrongDaLam.MaCauTLDienChoTrongDaLam = maCauTraLoi;
            return cauTraLoiDienChoTrongDaLamDAL.GetById(CauTraLoiDienChoTrongDaLam);
        }
        //write method update
        public bool Update(CauTraLoiDienChoTrongDaLamDTO cauTraLoiDienChoTrongDaLam)
        {
            return cauTraLoiDienChoTrongDaLamDAL.Update(cauTraLoiDienChoTrongDaLam);
            
        }
        public List<CauTraLoiDienChoTrongDaLamDTO> GetCauTraLoiByMaCauHoi(int MaCauHoi)
        {
            try
            {
                return cauTraLoiDienChoTrongDaLamDAL.GetAllByMaCauHoi(MaCauHoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }
    }
}
