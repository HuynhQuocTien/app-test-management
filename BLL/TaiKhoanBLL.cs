﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL taiKhoanDAL;
        public TaiKhoanBLL()
        {
            taiKhoanDAL = new TaiKhoanDAL();  // Khởi tạo đối tượng DAL để truy cập dữ liệu
        }

        public TaiKhoanDTO getTaiKhoanById(long username)
        {
            TaiKhoanDTO taiKhoan = new TaiKhoanDTO();
            taiKhoan.Username = username;
            return taiKhoanDAL.GetById(taiKhoan);
        }

        public string kiemTraEmailNguoiDung(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "Không được để trống";
            }
            return taiKhoanDAL.kiemTraEmailNguoiDung(email) ? "Oke" : "Gmail không tồn tại";
        }
        public string kiemTraTaiKhoan(string taiKhoan, string matKhau)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
                {
                    return "Tài khoản hoặc mật khẩu không được để trống."; // Trả về thông báo lỗi
                }

                // Gọi lớp DAL để kiểm tra tài khoản và mật khẩu trong cơ sở dữ liệu
                return taiKhoanDAL.kiemTraTaiKhoan(taiKhoan, matKhau)
                       ? "Đăng nhập thành công!"
                       : "Tài khoản hoặc mật khẩu không chính xác.";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ghi log nếu cần
                Console.WriteLine("Lỗi trong BLL: " + ex.Message);
                return "Đã xảy ra lỗi. Vui lòng thử lại sau.";
            }
        }
        public string suaMatKhauNguoiDung(string email, string matKhau, string matKhauXacNhan)
        {
            if (string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(matKhauXacNhan))
            {
                return "Vui lòng không để trống (Mật khẩu hoặc Xác nhận)";
            }
            else if (!matKhau.Equals(matKhauXacNhan))
            {
                return "Mật khẩu mới không trùng Mật khẩu xác nhận";
            }
            else if (matKhau.Length < 6)
            {
                return "Mật khẩu phải ít nhất 6 ký tự";
            }
            return taiKhoanDAL.suaMatKhauNguoiDung(email, matKhau) ? "Oke" : "Lỗi DAL";
        }

        public string updateEmail(string old_email, string new_email, long maNguoiDung)
        {
            if (new_email == "")
            {
                return "Vui lòng không để trống (Email)";
            }

            if (taiKhoanDAL.checkEmail(new_email, maNguoiDung))
            {
                return "Email đã trùng với tài khoản khác!";
            }

            return taiKhoanDAL.UpdateEmail(new_email, maNguoiDung) ? "1" : "2";
        }
        public TaiKhoanDTO getTaiKhoanByEmail(string email)
        {
            return taiKhoanDAL.getTaiKhoanByEmail(email);
        }
    }
}
