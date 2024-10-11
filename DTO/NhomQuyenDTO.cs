using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhomQuyenDTO
    {
        public int MaNhomQuyen {  get; set; }
        public string TenQuyen { get; set; }
        public int Level {  get; set; }     

        public NhomQuyenDTO() { }

        public NhomQuyenDTO(int maNhomQuyen, string tenQuyen, int level)
        {
            MaNhomQuyen = maNhomQuyen;
            TenQuyen = tenQuyen;
            Level = level;
        }
    }
}
