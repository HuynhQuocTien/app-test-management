using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    internal class ChiTietDeDAL : IUnitDAL<ChiTietDeDTO>
    {
        public static ChiTietDeDAL getInstance()
        {
            return new ChiTietDeDAL();
        }

        public bool Add(ChiTietDeDTO chiTietDe)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO ChiTietDe (MaDe, MaCauHoi) VALUES (@MaDe, @MaCauHoi);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", chiTietDe.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDe.MaCauHoi);
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

        public bool Delete(ChiTietDeDTO chiTietDe)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM ChiTietDe WHERE MaChiTietDe = @MaChiTietDe";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChiTietDe", chiTietDe.MaChiTietDe);
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

        public List<ChiTietDeDTO> GetAll()
        {
            List<ChiTietDeDTO> chiTietDeList = new List<ChiTietDeDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM ChiTietDe";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChiTietDeDTO chiTietDe = new ChiTietDeDTO
                            {
                                MaChiTietDe = Convert.ToInt32(reader["MaChiTietDe"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"])
                            };
                            chiTietDeList.Add(chiTietDe);
                        }
                    }
                }
            }
            return chiTietDeList;
        }

        public ChiTietDeDTO GetById(ChiTietDeDTO chiTietDe)
        {
            ChiTietDeDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM ChiTietDe WHERE MaChiTietDe = @MaChiTietDe";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaChiTietDe", chiTietDe.MaChiTietDe);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new ChiTietDeDTO
                            {
                                MaChiTietDe = Convert.ToInt32(reader["MaChiTietDe"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(ChiTietDeDTO chiTietDe)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE ChiTietDe SET MaDe = @MaDe, MaCauHoi = @MaCauHoi WHERE MaChiTietDe = @MaChiTietDe";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChiTietDe", chiTietDe.MaChiTietDe);
                        command.Parameters.AddWithValue("@MaDe", chiTietDe.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDe.MaCauHoi);
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
