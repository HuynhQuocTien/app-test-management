using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDeDTO
    {
        public int MaChiTietDe { get; set; }
        public int MaDe { get; set; }
        public int MaCauHoi { get; set; }

        public ChiTietDeDTO()
        {
        }

        public ChiTietDeDTO(int maChiTietDe, int maDe, int maCauHoi)
        {
            MaChiTietDe = maChiTietDe;
            MaDe = maDe;
            MaCauHoi = maCauHoi;
        }
    }
}
