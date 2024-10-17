using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NoiCauDaLamDTO
    {
        public int MaNoiCauDaLam { get; set; }
        public int MaCauHoi { get; set; }
        public string NoiDung { get; set; }

        public NoiCauDaLamDTO()
        {
        }

        public NoiCauDaLamDTO(int maNoiCauDaLam, int maCauHoi, string noiDung)
        {
            MaNoiCauDaLam = maNoiCauDaLam;
            MaCauHoi = maCauHoi;
            NoiDung = noiDung;
        }
    }
}
