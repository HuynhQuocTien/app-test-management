using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NguoiDungDAL
    {
        // kết nối csdl
        private SqlConnection conn;
        public NguoiDungDAL()
        {
            conn = GetConnectionDb.GetConnection(); // Khởi tạo đối tượng ConnectDB
        }
        // Kiểm tra gmail có trong db không
        public bool kiemTraEmailNguoiDung(string email)
        {
            string query = $"SELECT COUNT(*) FROM NguoiDung WHERE Email = @email";
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@email", email);
                    // kiểu var giống biến trong js
                    var result = command.ExecuteScalar();
                    return (int)result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool kiemTraTaiKhoan(string taiKhoan, string matKhau)
        {
            string query = $"SELECT Password FROM NguoiDung WHERE MaNguoiDung = @TaiKhoan";
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // thêm tham số vào câu lệnh SQL
                    command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Kiểm tra xem có hàng nào không
                        {
                            string matKhauNguoiDung = reader["Password"].ToString(); // Lấy giá trị từ cột Password
                            Console.Write(matKhauNguoiDung);
                            return matKhauNguoiDung.Equals(matKhau);
                        }
                        else
                        {
                            Console.WriteLine("Tài khoản không tồn tại.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                // Đảm bảo rằng kết nối được đóng
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        public bool suaMatKhauNguoiDung(string email, string matKhau)
        {
            string query = "UPDATE NguoiDung SET Password = @MatKhau WHERE Email = @Email";
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@MatKhau", matKhau);
                    command.Parameters.AddWithValue("@Email", email);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
