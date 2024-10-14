using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NoiCauTraLoiDTO
    {
        public int MaCauTraLoi { get; set; }
        public int MaCauNoi { get; set; }
        public string NoiDung { get; set; }
        public string DapAnNoi { get; set; }

        public NoiCauTraLoiDTO()
        {
        }

        public NoiCauTraLoiDTO(int maCauTraLoi, int maCauNoi, string noiDung, string dapAnNoi)
        {
            MaCauTraLoi = maCauTraLoi;
            MaCauNoi = maCauNoi;
            NoiDung = noiDung;
            DapAnNoi = dapAnNoi;
        }
    }

}
