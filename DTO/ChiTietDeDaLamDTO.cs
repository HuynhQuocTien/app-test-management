using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDeDaLamDTO
    {
        public int MaDe { get; set; }
        public int MaCauHoi { get; set; }
        public ChiTietDeDaLamDTO() { }
        public ChiTietDeDaLamDTO(int maDe, int maCauHoi)
        {
            MaDe = maDe;
            MaCauHoi = maCauHoi;
        }
    }
}
