using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    internal class PhanCongDAL : IUnitDAL<PhanCongDTO>
    {
        public static PhanCongDAL getInstance()
        {
            return new PhanCongDAL();
        }

        public bool Add(PhanCongDTO phanCong)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO PhanCong (MaMonHoc, MaGV) VALUES (@MaMonHoc, @MaGV);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                        command.Parameters.AddWithValue("@MaGV", phanCong.MaGV);
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

        public bool Delete(PhanCongDTO phanCong)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM PhanCong WHERE MaPhanCong = @MaPhanCong";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
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

        public List<PhanCongDTO> GetAll()
        {
            List<PhanCongDTO> phanCongList = new List<PhanCongDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM PhanCong";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhanCongDTO phanCong = new PhanCongDTO
                            {
                                MaPhanCong = Convert.ToInt32(reader["MaPhanCong"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaGV = Convert.ToInt32(reader["MaGV"])
                            };
                            phanCongList.Add(phanCong);
                        }
                    }
                }
            }
            return phanCongList;
        }

        public PhanCongDTO GetById(PhanCongDTO phanCong)
        {
            PhanCongDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM PhanCong WHERE MaPhanCong = @MaPhanCong";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new PhanCongDTO
                            {
                                MaPhanCong = Convert.ToInt32(reader["MaPhanCong"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaGV = Convert.ToInt32(reader["MaGV"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(PhanCongDTO phanCong)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE PhanCong SET MaMonHoc = @MaMonHoc, MaGV = @MaGV WHERE MaPhanCong = @MaPhanCong";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
                        command.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                        command.Parameters.AddWithValue("@MaGV", phanCong.MaGV);
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
