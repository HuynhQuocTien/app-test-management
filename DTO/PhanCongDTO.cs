using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhanCongDTO
    {
        public int MaPhanCong {  get; set; }
        public int MaMonHoc { get; set; }
        public int MaGV {  get; set; }

        public PhanCongDTO() { }

        public PhanCongDTO(int maPhanCong, int maMonHoc, int maGV)
        {
            MaPhanCong = maPhanCong;
            MaMonHoc = maMonHoc;
            MaGV = maGV;
        }
    }
}
