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
                                Email = Convert.ToInt32(reader["Email"]),
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
                                Email = Convert.ToInt32(reader["Email"]),
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
    }
}

