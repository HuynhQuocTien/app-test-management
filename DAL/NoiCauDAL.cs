using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class NoiCauDAL : IUnitNoiCau<NoiCauDTO>
    {
        public static NoiCauDAL getInstance()
        {
            return new NoiCauDAL();
        }

        public KeyValuePair<int, string> Add(NoiCauDTO noiCau)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NoiCau (MaCauHoi, NoiDung, Diem) VALUES (@MaCauHoi, @NoiDung, @Diem); " +
                        "SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", noiCau.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCau.NoiDung);
                        command.Parameters.AddWithValue("@Diem", noiCau.Diem);
                        int maCauNoi = Convert.ToInt32(command.ExecuteScalar());
                        return new KeyValuePair<int, string>(maCauNoi, noiCau.NoiDung);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return default; // Hoặc xử lý lỗi khác
            }
        }

        public bool Delete(int maCauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM NoiCau WHERE MaCauHoi = @MaCauHoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", maCauHoi);
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

        public List<NoiCauDTO> GetAll()
        {
            List<NoiCauDTO> noiCauList = new List<NoiCauDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCau";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NoiCauDTO noiCau = new NoiCauDTO
                            {
                                MaNoiCau = Convert.ToInt32(reader["MaNoiCau"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                Diem = Convert.ToDecimal(reader["Diem"])
                            };
                            noiCauList.Add(noiCau);
                        }
                    }
                }
            }
            return noiCauList;
        }
        public List<NoiCauDTO> GetAllByMaCauHoi(int MaCauHoi)
        {
            List<NoiCauDTO> noiCauList = new List<NoiCauDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCau Where MaCauHoi = @MaCauHoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauHoi", MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NoiCauDTO noiCau = new NoiCauDTO
                            {
                                MaNoiCau = Convert.ToInt32(reader["MaNoiCau"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                Diem = Convert.ToDecimal(reader["Diem"])
                            };
                            noiCauList.Add(noiCau);
                        }
                    }
                }
            }
            return noiCauList;
        }
        public List<int> GetAllMaNoiCau(int maCauHoi)
        {
            List<int> noiCauList = new List<int>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT MaNoiCau FROM NoiCau WHERE MaCauHoi = @MaCauHoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauHoi", maCauHoi);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int MaNoiCau = Convert.ToInt32(reader["MaNoiCau"]);
                            noiCauList.Add(MaNoiCau);
                        }
                    }
                }
            }
            return noiCauList;
        }

        public NoiCauDTO GetById(NoiCauDTO noiCau)
        {
            NoiCauDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCau WHERE MaNoiCau = @MaNoiCau";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNoiCau", noiCau.MaNoiCau);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new NoiCauDTO
                            {
                                MaNoiCau = Convert.ToInt32(reader["MaNoiCau"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                Diem = Convert.ToDecimal(reader["Diem"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(NoiCauDTO noiCau)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NoiCau SET MaCauHoi = @MaCauHoi, NoiDung = @NoiDung, Diem = @Diem WHERE MaNoiCau = @MaNoiCau";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNoiCau", noiCau.MaNoiCau);
                        command.Parameters.AddWithValue("@MaCauHoi", noiCau.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCau.NoiDung);
                        command.Parameters.AddWithValue("@Diem", noiCau.Diem);
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
