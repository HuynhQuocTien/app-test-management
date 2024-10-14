using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    internal class NhomQuyenDAL : IUnitDAL<NhomQuyenDTO>
    {
        public static NhomQuyenDAL getInstance()
        {
            return new NhomQuyenDAL();
        }

        public bool Add(NhomQuyenDTO nhomQuyen)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NhomQuyen (TenQuyen, Level) VALUES (@TenQuyen, @Level);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenQuyen", nhomQuyen.TenQuyen);
                        command.Parameters.AddWithValue("@Level", nhomQuyen.Level);
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

        public bool Delete(NhomQuyenDTO nhomQuyen)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM NhomQuyen WHERE MaNhomQuyen = @MaNhomQuyen";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhomQuyen", nhomQuyen.MaNhomQuyen);
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

        public List<NhomQuyenDTO> GetAll()
        {
            List<NhomQuyenDTO> nhomQuyenList = new List<NhomQuyenDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NhomQuyen";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhomQuyenDTO nhomQuyen = new NhomQuyenDTO
                            {
                                MaNhomQuyen = Convert.ToInt32(reader["MaNhomQuyen"]),
                                TenQuyen = reader["TenQuyen"].ToString(),
                                Level = Convert.ToInt32(reader["Level"])
                            };
                            nhomQuyenList.Add(nhomQuyen);
                        }
                    }
                }
            }
            return nhomQuyenList;
        }

        public NhomQuyenDTO GetById(NhomQuyenDTO nhomQuyen)
        {
            NhomQuyenDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NhomQuyen WHERE MaNhomQuyen = @MaNhomQuyen";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhomQuyen", nhomQuyen.MaNhomQuyen);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new NhomQuyenDTO
                            {
                                MaNhomQuyen = Convert.ToInt32(reader["MaNhomQuyen"]),
                                TenQuyen = reader["TenQuyen"].ToString(),
                                Level = Convert.ToInt32(reader["Level"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(NhomQuyenDTO nhomQuyen)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NhomQuyen SET TenQuyen = @TenQuyen, Level = @Level WHERE MaNhomQuyen = @MaNhomQuyen";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhomQuyen", nhomQuyen.MaNhomQuyen);
                        command.Parameters.AddWithValue("@TenQuyen", nhomQuyen.TenQuyen);
                        command.Parameters.AddWithValue("@Level", nhomQuyen.Level);
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
