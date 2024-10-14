using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CauTraLoiDienChoTrongDTO
    {
        public int MaCauTLiDienChoTrong { get; set; }
        public int MaCauHoi { get; set; }
        public int ViTri { get; set; }
        public string DapAnText { get; set; }
        public int IsDelete { get; set; }
        public CauTraLoiDienChoTrongDTO()
        {
            MaCauTLiDienChoTrong = 0;
            MaCauHoi = 0;
            ViTri = 0;
            DapAnText = "";
            IsDelete = 0;
        }

        public CauTraLoiDienChoTrongDTO(int maCauTLiDienChoTrong, int maCauHoi, int viTri, string dapAnText, int isDelete)
        {
            MaCauTLiDienChoTrong = maCauTLiDienChoTrong;
            MaCauHoi = maCauHoi;
            ViTri = viTri;
            DapAnText = dapAnText;
            IsDelete = isDelete;
        }
    }
}
