using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietLopDTO
    {
        private int MaChiTietLop {  get; set; }
        private int MaLop { get; set; }
        private int MaSV {  get; set; }
        private int TrangThai { get; set; }
        private int is_delete { get; set; }
        public ChiTietLopDTO() { }
        private ChiTietLopDTO(int maChiTietLop, int maLop, int maSV, int trangThai, int is_delete)
        {
            MaChiTietLop = maChiTietLop;
            MaLop = maLop;
            MaSV = maSV;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
        
    }
}
