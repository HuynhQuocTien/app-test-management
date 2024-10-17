using DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LopDAL : IUnitDAL<LopDTO>
    {
        public static LopDAL getInstance()
        {
            return new LopDAL();
        }
        public bool Add(LopDTO lop)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO Lop (MaGV,TenLop, MaMoi, TrangThai,is_delete)" +
                        "VALUES (@MaGV, @TenLop, @MaMoi,@TrangThai,@is_delete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaGV", lop.MaGV);
                        command.Parameters.AddWithValue("@TenLop", lop.TenLop);
                        command.Parameters.AddWithValue("@MaMoi", lop.MaMoi);
                        command.Parameters.AddWithValue("@TrangThai", 1);
                        command.Parameters.AddWithValue("@is_delete", 0);
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

        public bool Delete(LopDTO lop)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE Lop SET is_delete = @is_delete where MaLop = @MaLop";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@is_delete", 1);
                        command.Parameters.AddWithValue("@MaLop", lop.MaLop);
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

        public List<LopDTO> GetAll(long MaSV) //Loc lop theo MaSV
        {
            List<LopDTO> lopList = new List<LopDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT Lop.* FROM ChiTietLop as CTL" +
                    " INNER JOIN Lop ON CTL.MaLop = Lop.MaLop AND CTL.MaSV = " + MaSV + "  AND Lop.is_delete = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopDTO lop = new LopDTO
                            {
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                MaGV = Convert.ToInt64(reader["MaGV"]),
                                TenLop = reader["TenLop"].ToString(),
                                MaMoi = reader["MaMoi"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            lopList.Add(lop);
                        }
                    }

                }
            }
            return lopList;
        }

        public LopDTO GetById(LopDTO lop)
        {
            LopDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM Lop WHERE MaLop = @id and trang_thai = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", lop.MaLop);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new LopDTO
                            {
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                MaGV = Convert.ToInt32(reader["MaGV"]),
                                TenLop = reader["TenLop"].ToString(),
                                MaMoi = reader["MaMoi"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(LopDTO lop)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE Lop SET TenLop = @TenLop where MaLop = @MaLop";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", lop.MaLop);
                        command.Parameters.AddWithValue("@TenLop", lop.TenLop);
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
        public List<LopDTO> GetAll()
        {
            List<LopDTO> lopList = new List<LopDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FORM Lop Where is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopDTO lop = new LopDTO
                            {
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                MaGV = Convert.ToInt32(reader["MaGV"]),
                                TenLop = reader["TenLop"].ToString(),
                                MaMoi = reader["MaMoi"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            lopList.Add(lop);
                        }
                    }

                }
            }
            return lopList;
        }
        public int GetAutoIncrement()//lay MaLop
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT MaLop FROM Lop";
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
                                    result = reader.GetInt32(0); // Lấy giá trị cột AUTO_INCREMENT
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
            return result + 1;
        }
    }
}
