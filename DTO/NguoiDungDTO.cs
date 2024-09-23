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
        private string HoTen {  get; set; }
        private string Email { get; set; }
        private string GioiTinh { get; set; }
        private DateTime NgaySinh { get; set; }
        private string Avatar { get; set; }
        private string SDT { get; set; }
        private int MaNhomQuyen { get; set; }
        private int TrangThai { get; set; }
        public NguoiDungDTO() { }

        public NguoiDungDTO(int maNguoiDung, string hoTen, string email, string gioiTinh, DateTime ngaySinh, string avatar, string sDT, int maNhomQuyen, int trangThai)
        {
            MaNguoiDung = maNguoiDung;
            HoTen = hoTen;
            Email = email;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            Avatar = avatar;
            SDT = sDT;
            MaNhomQuyen = maNhomQuyen;
            TrangThai = trangThai;
        }
    }
}
