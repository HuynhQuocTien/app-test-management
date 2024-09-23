using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LopDTO
    {
        private int MaLop {  get; set; }
        private int MaGV { get; set; }
        private string TenLop { get; set; }
        public string MaMoi { get; set; }
        public int TrangThai { get; set; }
        public LopDTO() { }

        public LopDTO(int maLop, int maGV, string tenLop, string maMoi, int trangThai)
        {
            MaLop = maLop;
            MaGV = maGV;
            TenLop = tenLop;
            MaMoi = maMoi;
            TrangThai = trangThai;
        }
    }
}
