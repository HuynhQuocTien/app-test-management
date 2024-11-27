using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NoiCauTraLoiBLLDaLam
    {
        private NoiCauTraLoiDaLamDAL noiCauTraLoiDaLamDAL;

        public NoiCauTraLoiBLLDaLam()
        {
            noiCauTraLoiDaLamDAL = NoiCauTraLoiDaLamDAL.getInstance();
        }

        public NoiCauTraLoiDaLamDTO GetNoiCauTraLoiByMaNoiCau(int MaNoiCau)
        {
            try
            {
                return noiCauTraLoiDaLamDAL.GetByMaNoiCau(MaNoiCau);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }
    }
}
