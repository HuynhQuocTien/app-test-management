using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NoiCauDaLamBLL
    {
        private NoiCauDaLamDAL noiCauDaLamDAL;

        public NoiCauDaLamBLL()
        {
            noiCauDaLamDAL = NoiCauDaLamDAL.getInstance();
        }

        public List<NoiCauDaLamDTO> GetNoiCauByMaCauHoi(int MaCauHoi)
        {
            try
            {
                return noiCauDaLamDAL.GetAllByMaCauHoi(MaCauHoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }
    }
}
