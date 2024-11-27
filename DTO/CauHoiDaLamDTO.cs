using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class CauHoiDaLamDTO
    {
        public int MaCauHoiDaLam { get; set; }
        public string NoiDung { get; set; }
        public long IdNguoiTao { get; set; }
        public int MaMonHoc { get; set; }
        public string DoKho { get; set; }
        public string LoaiCauHoi { get; set; }

        public CauHoiDaLamDTO()
        {
        }
        public CauHoiDaLamDTO(int maCauHoiDaLam, string noiDung, long idNguoiTao, int maMonHoc, string doKho, string loaiCauHoi)
        {
            MaCauHoiDaLam = maCauHoiDaLam;
            NoiDung = noiDung;
            IdNguoiTao = idNguoiTao;
            MaMonHoc = maMonHoc;
            DoKho = doKho;
            LoaiCauHoi = loaiCauHoi;
        }
    }
}
