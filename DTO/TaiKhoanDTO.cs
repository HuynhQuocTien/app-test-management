using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaiKhoanDTO
    {
        private long Username { get; set; } //Noi voi ID cua nguoi dung la` MSSV hoac MSGV
        private string Password { get; set; }
        private int Email { get; set; }
        private int MaNhomQuyen { get; set; }
        private int TrangThai { get; set; }
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
