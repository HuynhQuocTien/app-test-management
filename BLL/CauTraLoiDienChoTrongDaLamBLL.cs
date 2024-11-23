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
