using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DeThiDTO
    {
        public int MaDe {  get; set; }
        public int MaMonHoc { get; set; }
        public string TenDe { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc {  get; set; }
        public long NguoiTao { get; set; }
        public int TrangThai {  get; set; }
        public int is_delete { get; set; }
        public string TenMonHoc { get; set; }
        public DeThiDTO() { }

        public DeThiDTO(int maDe, int maMonHoc, string tenDe, DateTime thoiGianTao, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, long nguoiTao, int trangThai, int is_delete)
        {
            MaDe = maDe;
            MaMonHoc = maMonHoc;
            TenDe = tenDe;
            ThoiGianTao = thoiGianTao;
            ThoiGianBatDau = thoiGianBatDau;
            ThoiGianKetThuc = thoiGianKetThuc;
            NguoiTao = nguoiTao;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }

        public DeThiDTO(int maDe, int maMonHoc, string tenDe, DateTime thoiGianTao, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, long nguoiTao, int trangThai, int is_delete, string tenMonHoc)
        {
            MaDe = maDe;
            MaMonHoc = maMonHoc;
            TenDe = tenDe;
            ThoiGianTao = thoiGianTao;
            ThoiGianBatDau = thoiGianBatDau;
            ThoiGianKetThuc = thoiGianKetThuc;
            NguoiTao = nguoiTao;
            TrangThai = trangThai;
            this.is_delete = is_delete;
            TenMonHoc = tenMonHoc;
        }
    }
}
