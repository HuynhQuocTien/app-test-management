using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CauTraLoiDienChoTrongDaLamDTO
    {
        public int MaCauTLDienChoTrongDaLam { get; set; }
        public int MaCauHoi { get; set; }
        public int ViTri { get; set; }
        public string CauTraLoiText { get; set; }
        public string DapAnText { get; set; }
        public int IsDelete { get; set; }
        public CauTraLoiDienChoTrongDaLamDTO()
        {
            MaCauTLDienChoTrongDaLam = 0;
            MaCauHoi = 0;
            ViTri = 0;
            CauTraLoiText = "";
            DapAnText = "";
            IsDelete = 0;
        }

        public CauTraLoiDienChoTrongDaLamDTO(int maCauTLDienChoTrongDaLam, int maCauHoi, int viTri, string cauTraLoiText, string dapAnText, int isDelete)
        {
            MaCauTLDienChoTrongDaLam = maCauTLDienChoTrongDaLam;
            MaCauHoi = maCauHoi;
            ViTri = viTri;
            CauTraLoiText = cauTraLoiText;
            DapAnText = dapAnText;
            IsDelete = isDelete;
        }
    }
       
}
