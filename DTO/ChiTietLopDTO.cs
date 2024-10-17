using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietLopDTO
    {
        public int MaChiTietLop {  get; set; }
        public int MaLop { get; set; }
        public long MaSV {  get; set; }
        public int TrangThai { get; set; }
        public int is_delete { get; set; }
        public ChiTietLopDTO() { }
        public ChiTietLopDTO(int maChiTietLop, int maLop, long maSV, int trangThai, int is_delete)
        {
            MaChiTietLop = maChiTietLop;
            MaLop = maLop;
            MaSV = maSV;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
        
    }
}
