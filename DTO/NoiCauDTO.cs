using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NoiCauDTO
    {
        public int MaNoiCau { get; set; }
        public int MaCauHoi { get; set; }
        public string NoiDung { get; set; }
        public decimal Diem { get; set; }

        public NoiCauDTO()
        {
        }

        public NoiCauDTO(int maNoiCau, int maCauHoi, string noiDung, decimal diem)
        {
            MaNoiCau = maNoiCau;
            MaCauHoi = maCauHoi;
            NoiDung = noiDung;
            Diem = diem;
        }
    }

}
