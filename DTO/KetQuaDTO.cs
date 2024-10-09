using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KetQuaDTO
    {
        private int MaKetQua {  get; set; }
        private int MaDe {  get; set; }
        private int MaNguoiDung { get; set; } //MaSV
        private int Diem {  get; set; }
        private int SoCauDung { get; set; }
        private int SoCauSai {  get; set; }
        private int TrangThai { get; set; }
        private int is_delete { get; set; }
        public KetQuaDTO() { }

        public KetQuaDTO(int maKetQua, int maDe, int maNguoiDung, int diem, int soCauDung, int soCauSai, int trangThai, int is_delete)
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
