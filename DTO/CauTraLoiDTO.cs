using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CauTraLoiDTO
    {
        private int MaCauTL {  get; set; }
        private int MaCauHoi { get; set; }
        private string NoiDung { get; set; }
        private int is_DapAn {  get; set; }

        public CauTraLoiDTO()
        {
        }

        public CauTraLoiDTO(int maCauTL, int maCauHoi, string noiDung, int is_DapAn)
        {
            MaCauTL = maCauTL;
            MaCauHoi = maCauHoi;
            NoiDung = noiDung;
            this.is_DapAn = is_DapAn;
        }

    }
}
