using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauTraLoiDaLamBLL
    {
        private CauTraLoiDaLamDAL cauTraLoiDaLamDAL;

        public CauTraLoiDaLamBLL()
        {
            cauTraLoiDaLamDAL = CauTraLoiDaLamDAL.getInstance();
        }

        public List<CauTraLoiDaLamDTO> GetCauTraLoiByMaCauHoi(int MaCauHoi)
        {
            try
            {
                return cauTraLoiDaLamDAL.GetAllByMaCauHoi(MaCauHoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }
    }
}
