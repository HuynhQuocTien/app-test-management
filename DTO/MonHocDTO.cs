using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MonHocDTO
    {
        private int MaMonHoc { set; get; }
        private string TenMonHoc { set; get; }
        private int SoTC {  set; get; }
        private int SoTietLT { set; get; }
        private int SoTietTH { set; get; }
        private int Chuong {  set; get; }
        private int TrangThai { set; get; }
        private int is_delete { get; set; }
        public MonHocDTO() { }
        public MonHocDTO(int maMonHoc, string tenMonHoc, int soTC, int soTietLT, int soTietTH, int chuong, int trangThai, int is_delete)
        {
            MaMonHoc = maMonHoc;
            TenMonHoc = tenMonHoc;
            SoTC = soTC;
            SoTietLT = soTietLT;
            SoTietTH = soTietTH;
            Chuong = chuong;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
    }
}
