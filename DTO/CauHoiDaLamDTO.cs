using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class CauHoiDaLamDTO
    {
        private int MaCauHoiDaLam { get; set; }
        private string NoiDung { get; set; }
        private long IdNguoiTao { get; set; }
        private int MaMonHoc { get; set; }
        private string DoKho { get; set; }
        public CauHoiDaLamDTO()
        {
        }
        public CauHoiDaLamDTO(int maCauHoiDaLam, string noiDung, long idNguoiTao, int maMonHoc, string doKho)
        {
            MaCauHoiDaLam = maCauHoiDaLam;
            NoiDung = noiDung;
            IdNguoiTao = idNguoiTao;
            MaMonHoc = maMonHoc;
            DoKho = doKho;
        }
    }
}
