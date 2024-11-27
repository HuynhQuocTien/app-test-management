using DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class MonHocDAL : IUnitDAL<MonHocDTO>
    {
        public static MonHocDAL getInstance()
        {
            return new MonHocDAL();
        }

        public bool Add(MonHocDTO monHoc)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO MonHoc (TenMonHoc, SoTC, SoTietLT, SoTietTH, TrangThai, is_delete)" +
                        "VALUES (@TenMonHoc, @SoTC, @SoTietLT, @SoTietTH, @TrangThai, @is_delete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenMonHoc", monHoc.TenMonHoc);
                        command.Parameters.AddWithValue("@SoTC", monHoc.SoTC);
                        command.Parameters.AddWithValue("@SoTietLT", monHoc.SoTietLT);
                        command.Parameters.AddWithValue("@SoTietTH", monHoc.SoTietTH);
                        command.Parameters.AddWithValue("@TrangThai", monHoc.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", monHoc.is_delete);
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
        public bool Import(MonHocDTO monHoc)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO MonHoc (MaMonHoc, TenMonHoc, SoTC, SoTietLT, SoTietTH, TrangThai, is_delete)" +
                        "VALUES (@TenMonHoc, @SoTC, @SoTietLT, @SoTietTH, @TrangThai, @is_delete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenMonHoc", monHoc.TenMonHoc);
                        command.Parameters.AddWithValue("@SoTC", monHoc.SoTC);
                        command.Parameters.AddWithValue("@SoTietLT", monHoc.SoTietLT);
                        command.Parameters.AddWithValue("@SoTietTH", monHoc.SoTietTH);
                        command.Parameters.AddWithValue("@TrangThai", monHoc.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", monHoc.is_delete);
                        int rowsChanged = command.ExecuteNonQuery();
                        return rowsChanged > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public bool checkMaMonHoc(MonHocDTO monHoc)
        {
            try
            {
                using (SqlConnection conn = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM MonHoc WHERE MaMonHoc = @MaMonHoc";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", monHoc.MaMonHoc);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
        public bool Delete(MonHocDTO monHoc)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE MonHoc SET is_delete = 1 WHERE MaMonHoc = @MaMonHoc";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", monHoc.MaMonHoc);
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

        public List<MonHocDTO> GetAll()
        {
            List<MonHocDTO> monHocList = new List<MonHocDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM MonHoc WHERE is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonHocDTO monHoc = new MonHocDTO
                            {
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenMonHoc = reader["TenMonHoc"].ToString(),
                                SoTC = Convert.ToInt32(reader["SoTC"]),
                                SoTietLT = Convert.ToInt32(reader["SoTietLT"]),
                                SoTietTH = Convert.ToInt32(reader["SoTietTH"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            monHocList.Add(monHoc);
                        }
                    }
                }
            }
            return monHocList;
        }

        public List<MonHocDTO> GetFromPhanCong(long MaGV)
        {
            List<MonHocDTO> monHocList = new List<MonHocDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT MH.* FROM MonHoc MH INNER JOIN PhanCong PC ON MH.MaMonHoc=PC.MaMonHoc WHERE MH.is_delete = 0 and PC.MaGV=@MaGV;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaGV", MaGV);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonHocDTO monHoc = new MonHocDTO
                            {
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenMonHoc = reader["TenMonHoc"].ToString(),
                                SoTC = Convert.ToInt32(reader["SoTC"]),
                                SoTietLT = Convert.ToInt32(reader["SoTietLT"]),
                                SoTietTH = Convert.ToInt32(reader["SoTietTH"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            monHocList.Add(monHoc);
                        }
                    }
                }
            }
            return monHocList;
        }

        public MonHocDTO GetById(MonHocDTO monHoc)
        {
            MonHocDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM MonHoc WHERE MaMonHoc = @MaMonHoc";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaMonHoc", monHoc.MaMonHoc);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new MonHocDTO
                            {
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenMonHoc = reader["TenMonHoc"].ToString(),
                                SoTC = Convert.ToInt32(reader["SoTC"]),
                                SoTietLT = Convert.ToInt32(reader["SoTietLT"]),
                                SoTietTH = Convert.ToInt32(reader["SoTietTH"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }
        public bool Update(MonHocDTO monHoc)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE MonHoc SET TenMonHoc = @TenMonHoc, SoTC = @SoTC, SoTietLT = @SoTietLT, SoTietTH = @SoTietTH, TrangThai = @TrangThai, is_delete = @is_delete WHERE MaMonHoc = @MaMonHoc";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", monHoc.MaMonHoc);
                        command.Parameters.AddWithValue("@TenMonHoc", monHoc.TenMonHoc);
                        command.Parameters.AddWithValue("@SoTC", monHoc.SoTC);
                        command.Parameters.AddWithValue("@SoTietLT", monHoc.SoTietLT);
                        command.Parameters.AddWithValue("@SoTietTH", monHoc.SoTietTH);
                        command.Parameters.AddWithValue("@TrangThai", monHoc.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", monHoc.is_delete);
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


