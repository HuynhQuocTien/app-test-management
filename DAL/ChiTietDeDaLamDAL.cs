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
                    string query = "INSERT INTO ChiTietDeThiDaLam (MaDe, MaCauHoi,MaKetQua)" +
                        "VALUES (@MaDe, @MaCauHoi,@MaKetQua);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", chiTietDeDaLam.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDeDaLam.MaCauHoi);
                        command.Parameters.AddWithValue("@MaKetQua", chiTietDeDaLam.MaKetQua);
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
                                MaChiTietDeDaLam = Convert.ToInt32(reader["MaCTDTDL"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                MaKetQua = Convert.ToInt32(reader["MaKetQua"]),
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
                                MaChiTietDeDaLam = Convert.ToInt32(reader["MaCTDTDL"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                MaKetQua = Convert.ToInt32(reader["MaKetQua"]),
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
                    string query = "UPDATE ChiTietDeDaLam SET MaDe = @MaDe, MaCauHoi = @MaCauHoi WHERE MaCTDTDL = @MaCTDTDL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {                       
                        command.Parameters.AddWithValue("@MaCTDTDL", chiTietDeDaLam.MaChiTietDeDaLam);
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
