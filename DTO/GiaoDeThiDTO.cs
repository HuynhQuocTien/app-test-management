using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiaoDeThiDTO
    {
        public int MaDe { get; set; }
        public int MaLop { get; set; }
        public int NguoiGiao { get; set; }
        public int is_delete { get; set; }
        public GiaoDeThiDTO() { }
        public GiaoDeThiDTO(int maDe, int maLop, int nguoiGiao, int is_delete)
        {
            MaDe = maDe;
            MaLop = maLop;
            NguoiGiao = nguoiGiao;
            this.is_delete = is_delete;
        }
    }
}
