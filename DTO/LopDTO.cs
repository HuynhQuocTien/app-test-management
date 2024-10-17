using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LopDTO
    {
        public int MaLop {  get; set; }
        public long MaGV { get; set; }
        public string TenLop { get; set; }
        public string MaMoi { get; set; }
        public int TrangThai { get; set; }
        public int is_delete { get; set; }
        public LopDTO() { }
        public LopDTO(int maLop, long maGV, string tenLop, string maMoi, int trangThai, int is_delete)
        {
            MaLop = maLop;
            MaGV = maGV;
            TenLop = tenLop;
            MaMoi = maMoi;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
    }
}
