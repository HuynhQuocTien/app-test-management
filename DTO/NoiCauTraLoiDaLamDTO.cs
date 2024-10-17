using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NoiCauTraLoiDaLamDTO
    {
        public int MaCauTLDaLam { get; set; }
        public int MaCauNoi { get; set; }
        public string NoiDung { get; set; }
        public string DapAnNoi { get; set; }
        public string DapAnChon { get; set; }
        public NoiCauTraLoiDaLamDTO()
        {
        }

        public NoiCauTraLoiDaLamDTO(int maCauTLDaLam, int maCauNoi, string noiDung, string dapAnNoi, string dapAnChon)
        {
            MaCauTLDaLam = maCauTLDaLam;
            MaCauNoi = maCauNoi;
            NoiDung = noiDung;
            DapAnNoi = dapAnNoi;
            DapAnChon = dapAnChon;
        }
    }

}
