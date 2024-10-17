using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguoiDungDTO
    {
        public long MaNguoiDung {  get; set; }
        public string HoTen {  get; set; } //Database: Ten
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Avatar { get; set; }
        public string SDT { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get; set; }
        public int is_delete { get; set; }
        public NguoiDungDTO() { }

        public NguoiDungDTO(int maNguoiDung, string hoTen, string gioiTinh, DateTime ngaySinh, string avatar, string sDT, DateTime ngayTao, int trangThai, int is_delete)
        {
            MaNguoiDung = maNguoiDung;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            Avatar = avatar;
            SDT = sDT;
            NgayTao = ngayTao;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
    }
}
