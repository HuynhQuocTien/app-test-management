using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class NoiCauTraLoiDAL : IUnitDAL<NoiCauTraLoiDTO>
    {
        public static NoiCauTraLoiDAL getInstance()
        {
            return new NoiCauTraLoiDAL();
        }

        public bool Add(NoiCauTraLoiDTO noiCauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NoiCauTraLoi (MaCauNoi, NoiDung, DapAnNoi) VALUES (@MaCauNoi, @NoiDung, @DapAnNoi);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauNoi", noiCauTraLoi.MaCauNoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCauTraLoi.NoiDung);
                        command.Parameters.AddWithValue("@DapAnNoi", noiCauTraLoi.DapAnNoi);
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

        public bool Delete(NoiCauTraLoiDTO noiCauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM NoiCauTraLoi WHERE MaCauTraLoi = @MaCauTraLoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTraLoi", noiCauTraLoi.MaCauTraLoi);
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

        public List<NoiCauTraLoiDTO> GetAll()
        {
            List<NoiCauTraLoiDTO> noiCauTraLoiList = new List<NoiCauTraLoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauTraLoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NoiCauTraLoiDTO noiCauTraLoi = new NoiCauTraLoiDTO
                            {
                                MaCauTraLoi = Convert.ToInt32(reader["MaCauTraLoi"]),
                                MaCauNoi = Convert.ToInt32(reader["MaCauNoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                DapAnNoi = reader["DapAnNoi"].ToString()
                            };
                            noiCauTraLoiList.Add(noiCauTraLoi);
                        }
                    }
                }
            }
            return noiCauTraLoiList;
        }

        public NoiCauTraLoiDTO GetById(NoiCauTraLoiDTO noiCauTraLoi)
        {
            NoiCauTraLoiDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NoiCauTraLoi WHERE MaCauTraLoi = @MaCauTraLoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauTraLoi", noiCauTraLoi.MaCauTraLoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new NoiCauTraLoiDTO
                            {
                                MaCauTraLoi = Convert.ToInt32(reader["MaCauTraLoi"]),
                                MaCauNoi = Convert.ToInt32(reader["MaCauNoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                DapAnNoi = reader["DapAnNoi"].ToString()
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(NoiCauTraLoiDTO noiCauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NoiCauTraLoi SET MaCauNoi = @MaCauNoi, NoiDung = @NoiDung, DapAnNoi = @DapAnNoi WHERE MaCauTraLoi = @MaCauTraLoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTraLoi", noiCauTraLoi.MaCauTraLoi);
                        command.Parameters.AddWithValue("@MaCauNoi", noiCauTraLoi.MaCauNoi);
                        command.Parameters.AddWithValue("@NoiDung", noiCauTraLoi.NoiDung);
                        command.Parameters.AddWithValue("@DapAnNoi", noiCauTraLoi.DapAnNoi);
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
