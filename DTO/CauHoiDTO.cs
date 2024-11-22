using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CauHoiDTO
    {
        public int MaCauHoi { get; set; }
        public string NoiDung { get; set; }
        public string LoaiCauHoi { get; set; }
        public int MaMonHoc { get; set; }
        public long MaNguoiTao { get; set; }
        public int DoKho { get; set; }
        public int TrangThai { get; set; }
        public int is_delete { get; set; }
        public CauHoiDTO()
        {
        }
        public CauHoiDTO(int maCauHoi, string noiDung,string loaiCauHoi, int maMonHoc, long maNguoiTao, int doKho, int trangThai, int is_delete)
        {
            MaCauHoi = maCauHoi;
            NoiDung = noiDung;
            LoaiCauHoi = loaiCauHoi;
            MaMonHoc = maMonHoc;
            MaNguoiTao = maNguoiTao;
            DoKho = doKho;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
        public override string ToString()
        {

            return $"Độ khó: " + DoKho + " | Tên: " + NoiDung; // Hiển thị thuộc tính Name trong ListBox
            //return $"mã: " + MaCauHoi;
        }
    }
}
