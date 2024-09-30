using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DeThiDTO
    {
        private int MaDe {  get; set; }
        private int MaMonHoc { get; set; }
        private string TenDe { get; set; }
        private DateTime ThoiGianTao { get; set; }
        private DateTime ThoiGianBatDau { get; set; }
        private DateTime ThoiGianKetThuc {  get; set; }
        private int NguoiTao { get; set; }
        private int TrangThai {  get; set; }
        private int is_delete { get; set; }
        public DeThiDTO() { }

        public DeThiDTO(int maDe, int maMonHoc, string tenDe, DateTime thoiGianTao, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, int nguoiTao, int trangThai, int is_delete)
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
    }
}
