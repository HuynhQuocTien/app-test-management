using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KetQuaDTO
    {
        public int MaKetQua {  get; set; }
        public int MaDe {  get; set; }
        public int MaNguoiDung { get; set; } //MaSV
        public decimal Diem {  get; set; }
        public int SoCauDung { get; set; }
        public int SoCauSai {  get; set; }
        public int TrangThai { get; set; }
        public int is_delete { get; set; }
        public KetQuaDTO() { }

        public KetQuaDTO(int maKetQua, int maDe, int maNguoiDung, decimal diem, int soCauDung, int soCauSai, int trangThai, int is_delete)
        {
            MaKetQua = maKetQua;
            MaDe = maDe;
            MaNguoiDung = maNguoiDung;
            Diem = diem;
            SoCauDung = soCauDung;
            SoCauSai = soCauSai;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
    }
}
