using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietQuyenDTO
    {
        private int MaChiTietQuyen {  get; set; }
        private int MaNhomQuyen { get; set; }
        private string ChucNang {  get; set; }
        private string HanhDong { get; set; }
        public ChiTietQuyenDTO() { }

        public ChiTietQuyenDTO(int maChiTietQuyen, int maNhomQuyen, string chucNang, string hanhDong)
        {
            MaChiTietQuyen = maChiTietQuyen;
            MaNhomQuyen = maNhomQuyen;
            ChucNang = chucNang;
            HanhDong = hanhDong;
        }
    }
}
