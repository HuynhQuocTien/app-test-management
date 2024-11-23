using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class NoiCauTraLoiDaLamDAL : IUnitDAL<NoiCauTraLoiDaLamDTO>
    {
        public static NoiCauTraLoiDaLamDAL getInstance()
        {
            return new NoiCauTraLoiDaLamDAL();
        }

        public bool Add(NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NoiCauTraLoiDaLam (MaCauNoi, NoiDung, DapAnNoi, DapAnChon) VALUES (@MaCauNoi, @NoiDung, @DapAnNoi, @DapAnChon);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauNoi", noiCauTraLoiDaLam.MaCauNoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCauTraLoiDaLam.NoiDung);
                        command.Parameters.AddWithValue("@DapAnNoi", noiCauTraLoiDaLam.DapAnNoi);
                        command.Parameters.AddWithValue("@DapAnChon", noiCauTraLoiDaLam.DapAnChon);
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

        public bool Delete(NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM NoiCauTraLoiDaLam WHERE MaCauTLDaLam = @MaCauTLDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTLDaLam", noiCauTraLoiDaLam.MaCauTLDaLam);
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

        public List<NoiCauTraLoiDaLamDTO> GetAll()
        {
            List<NoiCauTraLoiDaLamDTO> noiCauTraLoiDaLamList = new List<NoiCauTraLoiDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauTraLoiDaLam";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam = new NoiCauTraLoiDaLamDTO
                            {
                                MaCauTLDaLam = Convert.ToInt32(reader["MaCauTLDaLam"]),
                                MaCauNoi = Convert.ToInt32(reader["MaCauNoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                DapAnNoi = reader["DapAnNoi"].ToString(),
                                DapAnChon = reader["DapAnChon"].ToString()
                            };
                            noiCauTraLoiDaLamList.Add(noiCauTraLoiDaLam);
                        }
                    }
                }
            }
            return noiCauTraLoiDaLamList;
        }

        public NoiCauTraLoiDaLamDTO GetById(NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam)
        {
            NoiCauTraLoiDaLamDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauTraLoiDaLam WHERE MaCauTLDaLam = @MaCauTLDaLam";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauTLDaLam", noiCauTraLoiDaLam.MaCauTLDaLam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new NoiCauTraLoiDaLamDTO
                            {
                                MaCauTLDaLam = Convert.ToInt32(reader["MaCauTLDaLam"]),
                                MaCauNoi = Convert.ToInt32(reader["MaCauNoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                DapAnNoi = reader["DapAnNoi"].ToString(),
                                DapAnChon = reader["DapAnChon"].ToString()
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NoiCauTraLoiDaLam SET MaCauNoi = @MaCauNoi, NoiDung = @NoiDung, DapAnNoi = @DapAnNoi, DapAnChon = @DapAnChon WHERE MaCauTLDaLam = @MaCauTLDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTLDaLam", noiCauTraLoiDaLam.MaCauTLDaLam);
                        command.Parameters.AddWithValue("@MaCauNoi", noiCauTraLoiDaLam.MaCauNoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCauTraLoiDaLam.NoiDung);
                        command.Parameters.AddWithValue("@DapAnNoi", noiCauTraLoiDaLam.DapAnNoi);
                        command.Parameters.AddWithValue("@DapAnChon", noiCauTraLoiDaLam.DapAnChon);
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


        public NoiCauTraLoiDaLamDTO GetByMaNoiCau(int MaNoiCau)
        {
            NoiCauTraLoiDaLamDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauTraLoiDaLam WHERE MaCauNoi = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", MaNoiCau);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new NoiCauTraLoiDaLamDTO
                            {
                                MaCauTLDaLam = Convert.ToInt32(reader["MaCauTLDaLam"]),
                                MaCauNoi = Convert.ToInt32(reader["MaCauNoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                DapAnNoi = reader["DapAnNoi"].ToString(),
                                DapAnChon = reader["DapAnChon"].ToString()
                            };
                        }
                    }
                }
            }
            return result;
        }
    }
}
