using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CauTraLoiDTO
    {
        public int MaCauTL {  get; set; }
        public int MaCauHoi { get; set; }
        public string NoiDung { get; set; }
        public int IsDapAn {  get; set; }

        public CauTraLoiDTO()
        {
        }

        public CauTraLoiDTO(int maCauTL, int maCauHoi, string noiDung, int is_DapAn)
        {
            MaCauTL = maCauTL;
            MaCauHoi = maCauHoi;
            NoiDung = noiDung;
            this.IsDapAn = is_DapAn;
        }

    }
}
