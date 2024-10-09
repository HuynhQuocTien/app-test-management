using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguoiDungDTO
    {
        private int MaNguoiDung {  get; set; }
        private string HoTen {  get; set; } //Database: Ten
        private string GioiTinh { get; set; }
        private DateTime NgaySinh { get; set; }
        private string Avatar { get; set; }
        private string SDT { get; set; }
        private DateTime NgayTao { get; set; }
        private int TrangThai { get; set; }
        private int is_delete { get; set; }
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
