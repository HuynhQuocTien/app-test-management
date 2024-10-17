using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class GiaoDeThiDAL : IUnitDAL<GiaoDeThiDTO>
    {
        public static GiaoDeThiDAL getInstance()
        {
            return new GiaoDeThiDAL();
        }

        public bool Add(GiaoDeThiDTO giaoDeThi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO GiaoDeThi (MaDe, MaLop, NguoiGiao, IsDelete)" +
                        "VALUES (@MaDe, @MaLop, @NguoiGiao, @IsDelete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", giaoDeThi.MaDe);
                        command.Parameters.AddWithValue("@MaLop", giaoDeThi.MaLop);
                        command.Parameters.AddWithValue("@NguoiGiao", giaoDeThi.NguoiGiao);
                        command.Parameters.AddWithValue("@IsDelete", giaoDeThi.IsDelete);
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

        public bool Delete(GiaoDeThiDTO giaoDeThi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM GiaoDeThi WHERE MaDe = @MaDe AND MaLop = @MaLop";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", giaoDeThi.MaDe);
                        command.Parameters.AddWithValue("@MaLop", giaoDeThi.MaLop);
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

        public List<GiaoDeThiDTO> GetAll()
        {
            List<GiaoDeThiDTO> giaoDeThiList = new List<GiaoDeThiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM GiaoDeThi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GiaoDeThiDTO giaoDeThi = new GiaoDeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                NguoiGiao = Convert.ToInt64(reader["NguoiGiao"]),
                                IsDelete = Convert.ToInt32(reader["IsDelete"])
                            };
                            giaoDeThiList.Add(giaoDeThi);
                        }
                    }
                }
            }
            return giaoDeThiList;
        }

        public GiaoDeThiDTO GetById(GiaoDeThiDTO giaoDeThi)
        {
            GiaoDeThiDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM GiaoDeThi WHERE MaDe = @MaDe AND MaLop = @MaLop";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDe", giaoDeThi.MaDe);
                    command.Parameters.AddWithValue("@MaLop", giaoDeThi.MaLop);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new GiaoDeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                NguoiGiao = Convert.ToInt64(reader["NguoiGiao"]),
                                IsDelete = Convert.ToInt32(reader["IsDelete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(GiaoDeThiDTO giaoDeThi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE GiaoDeThi SET NguoiGiao = @NguoiGiao, IsDelete = @IsDelete WHERE MaDe = @MaDe AND MaLop = @MaLop";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", giaoDeThi.MaDe);
                        command.Parameters.AddWithValue("@MaLop", giaoDeThi.MaLop);
                        command.Parameters.AddWithValue("@NguoiGiao", giaoDeThi.NguoiGiao);
                        command.Parameters.AddWithValue("@IsDelete", giaoDeThi.IsDelete);
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

