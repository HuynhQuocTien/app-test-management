using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhomQuyenDTO
    {
        private int MaNhomQuyen {  get; set; }
        private int MaChiTietQuyen { get; set; }
        private string TenQuyen { get; set; }
        private int Level {  get; set; }
        private int TrangThai { get; set; }
        private int is_delete { get; set; }

        public NhomQuyenDTO() { }
        public NhomQuyenDTO(int maNhomQuyen, int maChiTietQuyen, string tenQuyen, int level, int trangThai, int is_delete)
        {
            MaNhomQuyen = maNhomQuyen;
            MaChiTietQuyen = maChiTietQuyen;
            TenQuyen = tenQuyen;
            Level = level;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
    }
}
