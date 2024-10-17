using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaiKhoanDTO
    {
        public long Username { get; set; } //Noi voi ID cua nguoi dung la` MSSV hoac MSGV
        public string Password { get; set; }
        public int Email { get; set; }
        public int MaNhomQuyen { get; set; }
        public int TrangThai { get; set; }
        public TaiKhoanDTO() { }
        public TaiKhoanDTO(long username, string password, int email, int maNhomQuyen, int trangThai)
        {
            Username = username;
            Password = password;
            Email = email;
            MaNhomQuyen = maNhomQuyen;
            TrangThai = trangThai;
        }
    }
}
