using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauHoiDaLamBLL
    {

        private CauHoiDaLamDAL cauHoiDaLamDAL;

        public CauHoiDaLamBLL()
        {
            cauHoiDaLamDAL = CauHoiDaLamDAL.getInstance();
        }

        public CauHoiDaLamDTO GetCauHoiDaLamByMaCauHoi(int MaCauHoi)
        {
            try
            {
                return cauHoiDaLamDAL.GetByMaCauHoi(MaCauHoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }

    }
}
