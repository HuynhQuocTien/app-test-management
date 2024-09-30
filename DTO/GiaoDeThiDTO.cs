using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiaoDeThiDTO
    {
        private int MaDe { get; set; }
        private int MaLop { get; set; }
        private int NguoiGiao { get; set; }
        private int is_delete { get; set; }
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
