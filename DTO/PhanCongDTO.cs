using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhanCongDTO
    {
        private int MaPhanCong {  get; set; }
        private int MaMonHoc { get; set; }
        private int MaGV {  get; set; }

        public PhanCongDTO() { }

        public PhanCongDTO(int maPhanCong, int maMonHoc, int maGV)
        {
            MaPhanCong = maPhanCong;
            MaMonHoc = maMonHoc;
            MaGV = maGV;
        }
    }
}
