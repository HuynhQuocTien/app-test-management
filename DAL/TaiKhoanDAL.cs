using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class TaiKhoanDAL : IUnitDAL<TaiKhoanDTO>
    {
        public static TaiKhoanDAL getInstance()
        {
            return new TaiKhoanDAL();
        }
        // kết nối csdl
        private SqlConnection conn;
        public TaiKhoanDAL()
        {
            conn = GetConnectionDb.GetConnection(); // Khởi tạo đối tượng ConnectDB
        }
        // Kiểm tra gmail có trong db không
        public bool kiemTraEmailNguoiDung(string email)
        {
            string query = $"SELECT COUNT(*) FROM TaiKhoan WHERE Email = @email";
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
            string query = $"SELECT Password FROM TaiKhoan WHERE Username = @TaiKhoan";
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
            string query = "UPDATE TaiKhoan SET Password = @MatKhau WHERE Email = @Email";
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

        public bool Add(TaiKhoanDTO taiKhoan)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO TaiKhoan (Username, Password, Email, MaNhomQuyen, TrangThai) VALUES (@Username, @Password, @Email, @MaNhomQuyen, @TrangThai);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", taiKhoan.Username);
                        command.Parameters.AddWithValue("@Password", taiKhoan.Password);
                        command.Parameters.AddWithValue("@Email", taiKhoan.Email);
                        command.Parameters.AddWithValue("@MaNhomQuyen", taiKhoan.MaNhomQuyen);
                        command.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);
                        int rowsChanged = command.ExecuteNonQuery();
                        return rowsChanged > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Delete(TaiKhoanDTO taiKhoan)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM TaiKhoan WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", taiKhoan.Username);
                        int rowsChanged = command.ExecuteNonQuery();
                        return rowsChanged > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<TaiKhoanDTO> GetAll()
        {
            List<TaiKhoanDTO> taiKhoanList = new List<TaiKhoanDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM TaiKhoan";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                            {
                                Username = Convert.ToInt64(reader["Username"]),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString(),
                                MaNhomQuyen = Convert.ToInt32(reader["MaNhomQuyen"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"])
                            };
                            taiKhoanList.Add(taiKhoan);
                        }
                    }
                }
            }
            return taiKhoanList;
        }

        public TaiKhoanDTO GetById(TaiKhoanDTO taiKhoan)
        {
            TaiKhoanDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM TaiKhoan WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", taiKhoan.Username);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new TaiKhoanDTO
                            {
                                Username = Convert.ToInt64(reader["Username"]),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString(),
                                MaNhomQuyen = Convert.ToInt32(reader["MaNhomQuyen"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(TaiKhoanDTO taiKhoan)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE TaiKhoan SET Password = @Password, Email = @Email, MaNhomQuyen = @MaNhomQuyen, TrangThai = @TrangThai WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", taiKhoan.Username);
                        command.Parameters.AddWithValue("@Password", taiKhoan.Password);
                        command.Parameters.AddWithValue("@Email", taiKhoan.Email);
                        command.Parameters.AddWithValue("@MaNhomQuyen", taiKhoan.MaNhomQuyen);
                        command.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);
                        int rowsChanged = command.ExecuteNonQuery();
                        return rowsChanged > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool UpdateEmail(string email, long maNguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE TaiKhoan SET Email = @Email WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", maNguoiDung);
                        command.Parameters.AddWithValue("@Email", email);
                        int rowsChanged = command.ExecuteNonQuery();
                        return rowsChanged > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        public bool checkEmail(string email, long maNguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email and Username!=@MaNguoiDung";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                        // Dùng ExecuteScalar để trả về kết quả COUNT(*)
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0; // Email tồn tại nếu COUNT > 0
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra email: {ex.Message}");
                return false; // Trả về false nếu gặp lỗi
            }
        }

        public TaiKhoanDTO getTaiKhoanByEmail(string email)
        {
            TaiKhoanDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM TaiKhoan WHERE Email = @email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new TaiKhoanDTO
                            {
                                Username = Convert.ToInt64(reader["Username"]),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString(),
                                MaNhomQuyen = Convert.ToInt32(reader["MaNhomQuyen"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"])
                            };
                        }
                    }
                }
            }
            return result;
        }
    }
}

