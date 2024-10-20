using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class NoiCauDAL : IUnitDAL<NoiCauDTO>
    {
        public static NoiCauDAL getInstance()
        {
            return new NoiCauDAL();
        }

        public bool Add(NoiCauDTO noiCau)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NoiCau (MaCauHoi, NoiDung, Diem) VALUES (@MaCauHoi, @NoiDung, @Diem);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
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

        public bool Delete(NoiCauDTO noiCau)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM NoiCau WHERE MaNoiCau = @MaNoiCau";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNoiCau", noiCau.MaNoiCau);
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
