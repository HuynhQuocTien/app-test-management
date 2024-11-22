using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDeDaLamDTO
    {
        public int MaChiTietDeDaLam { get; set; }
        public int MaDe { get; set; }
        public int MaCauHoi { get; set; }
        public int MaKetQua { get; set; }
        public ChiTietDeDaLamDTO() { }

        public ChiTietDeDaLamDTO(int maChiTietDeDaLam, int maDe, int maCauHoi, int maKetQua)
        {
            MaChiTietDeDaLam = maChiTietDeDaLam;
            MaDe = maDe;
            MaCauHoi = maCauHoi;
            MaKetQua = maKetQua;
        }
    }
}
