using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MonHocDTO
    {
        public int MaMonHoc { set; get; }
        public string TenMonHoc { set; get; }
        public int SoTC {  set; get; }
        public int SoTietLT { set; get; }
        public int SoTietTH { set; get; }
        public int TrangThai { set; get; }
        public int is_delete { get; set; }
        public MonHocDTO() { }
        public MonHocDTO(int maMonHoc, string tenMonHoc, int soTC, int soTietLT, int soTietTH, int trangThai, int is_delete)
        {
            MaMonHoc = maMonHoc;
            TenMonHoc = tenMonHoc;
            SoTC = soTC;
            SoTietLT = soTietLT;
            SoTietTH = soTietTH;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
    }
}
