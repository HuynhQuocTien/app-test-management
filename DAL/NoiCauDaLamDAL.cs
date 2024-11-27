using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class NoiCauDaLamDAL : IUnitDAL<NoiCauDaLamDTO>
    {
        public static NoiCauDaLamDAL getInstance()
        {
            return new NoiCauDaLamDAL();
        }

        public bool Add(NoiCauDaLamDTO noiCauDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NoiCauDaLam (MaCauHoi, NoiDung) VALUES (@MaCauHoi, @NoiDung);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", noiCauDaLam.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCauDaLam.NoiDung);
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

        public bool AddNoiCauDaLamByMaCauHoi(int maCauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NoiCauDaLam (MaCauHoi, NoiDung) " +
                        " SELECT MaCauHoi, NoiDung FROM NoiCau WHERE MaCauHoi = @MaCauHoi;";
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

        public bool Delete(NoiCauDaLamDTO noiCauDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM NoiCauDaLam WHERE MaNoiCauDaLam = @MaNoiCauDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNoiCauDaLam", noiCauDaLam.MaNoiCauDaLam);
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

        public List<NoiCauDaLamDTO> GetAll()
        {
            List<NoiCauDaLamDTO> noiCauDaLamList = new List<NoiCauDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauDaLam ORDER BY MaNoiCauDaLam ASC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NoiCauDaLamDTO noiCauDaLam = new NoiCauDaLamDTO
                            {
                                MaNoiCauDaLam = Convert.ToInt32(reader["MaNoiCauDaLam"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString()
                            };
                            noiCauDaLamList.Add(noiCauDaLam);
                        }
                    }
                }
            }
            return noiCauDaLamList;
        }

        public int GetAutoIncrement()
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT MaNoiCauDaLam FROM NoiCauDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No data");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    result = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (result == -1)
            {
                return 1;
            }
            return result + 1;
        }

        public NoiCauDaLamDTO GetById(NoiCauDaLamDTO noiCauDaLam)
        {
            NoiCauDaLamDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauDaLam WHERE MaNoiCauDaLam = @MaNoiCauDaLam";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNoiCauDaLam", noiCauDaLam.MaNoiCauDaLam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new NoiCauDaLamDTO
                            {
                                MaNoiCauDaLam = Convert.ToInt32(reader["MaNoiCauDaLam"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString()
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(NoiCauDaLamDTO noiCauDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NoiCauDaLam SET MaCauHoi = @MaCauHoi, NoiDung = @NoiDung WHERE MaNoiCauDaLam = @MaNoiCauDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNoiCauDaLam", noiCauDaLam.MaNoiCauDaLam);
                        command.Parameters.AddWithValue("@MaCauHoi", noiCauDaLam.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCauDaLam.NoiDung);
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

        public List<NoiCauDaLamDTO> GetAllByMaCauHoi(int MaCauHoi)
        {
            List<NoiCauDaLamDTO> results = new List<NoiCauDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauDaLam WHERE MaCauHoi = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NoiCauDaLamDTO cautraloidalam = new NoiCauDaLamDTO
                            {
                                MaNoiCauDaLam = Convert.ToInt32(reader["MaNoiCauDaLam"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString()
                            };

                            results.Add(cautraloidalam); // Thêm đối tượng vào danh sách
                        }
                    }
                }
            }
            return results;
        }
    }
}
