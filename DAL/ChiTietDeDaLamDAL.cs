using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class ChiTietDeDaLamDAL : IUnitDAL<ChiTietDeDaLamDTO>
    {
        public static ChiTietDeDaLamDAL getInstance()
        {
            return new ChiTietDeDaLamDAL();
        }

        public bool Add(ChiTietDeDaLamDTO chiTietDeDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO ChiTietDeThiDaLam (MaDe, MaCauHoi)" +
                        "VALUES (@MaDe, @MaCauHoi);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDeDaLam", chiTietDeDaLam.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDeDaLam.MaCauHoi);
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

        public bool Delete(ChiTietDeDaLamDTO chiTietDeDaLam)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM ChiTietDeDaLam WHERE MaDe = @MaDe AND MaCauHoi = @MaCauHoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", chiTietDeDaLam.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDeDaLam.MaCauHoi);
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

        public List<ChiTietDeDaLamDTO> GetAll()
        {
            List<ChiTietDeDaLamDTO> chiTietDeDaLamList = new List<ChiTietDeDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM ChiTietDeDaLam";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChiTietDeDaLamDTO chiTietDeDaLam = new ChiTietDeDaLamDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDeDaLam"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                            };
                            chiTietDeDaLamList.Add(chiTietDeDaLam);
                        }
                    }
                }
            }
            return chiTietDeDaLamList;
        }

        public ChiTietDeDaLamDTO GetById(ChiTietDeDaLamDTO chiTietDeDaLam)
        {
            ChiTietDeDaLamDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM ChiTietDeDaLam WHERE MaDe = @MaDe AND MaCauHoi = @MaCauHoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDe", chiTietDeDaLam.MaDe);
                    command.Parameters.AddWithValue("@MaCauHoi", chiTietDeDaLam.MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new ChiTietDeDaLamDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDeDaLam"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(ChiTietDeDaLamDTO chiTietDeDaLam) // Update MaDe, MaCauHoi sai logic
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE ChiTietDeDaLam SET MaDe = @MaDe, MaCauHoi = @MaCauHoi WHERE MaDe = @MaDe AND MaCauHoi = @MaCauHoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {                       
                        command.Parameters.AddWithValue("@MaDe", chiTietDeDaLam.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDeDaLam.MaCauHoi);
                        
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
