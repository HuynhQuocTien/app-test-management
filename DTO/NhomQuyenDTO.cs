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
        private string TenQuyen { get; set; }
        private int Level {  get; set; }     

        public NhomQuyenDTO() { }

        public NhomQuyenDTO(int maNhomQuyen, string tenQuyen, int level)
        {
            MaNhomQuyen = maNhomQuyen;
            TenQuyen = tenQuyen;
            Level = level;
        }
    }
}
