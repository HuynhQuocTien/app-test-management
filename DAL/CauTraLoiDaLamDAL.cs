using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class CauTraLoiDaLamDAL : IUnitDAL<CauTraLoiDaLamDTO>
    {
        public static CauTraLoiDaLamDAL getInstance()
        {
            return new CauTraLoiDaLamDAL();
        }

        public bool Add(CauTraLoiDaLamDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauTraLoiDaLam (MaCauHoiDaLam, NoiDung, IsDapAn, IsChon)" +
                        "VALUES (@MaCauHoiDaLam, @NoiDung, @IsDapAn, @IsChon);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoiDaLam", cauTraLoi.MaCauHoiDaLam);
                        command.Parameters.AddWithValue("@NoiDung", cauTraLoi.NoiDung);
                        command.Parameters.AddWithValue("@IsDapAn", cauTraLoi.IsDapAn);
                        command.Parameters.AddWithValue("@IsChon", cauTraLoi.IsChon);
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

        public bool Delete(CauTraLoiDaLamDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM CauTraLoiDaLam WHERE MaCauTraLoiDaLam = @MaCauTraLoiDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTraLoiDaLam", cauTraLoi.MaCauTraLoiDaLam);
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

        public List<CauTraLoiDaLamDTO> GetAll()
        {
            List<CauTraLoiDaLamDTO> cauTraLoiList = new List<CauTraLoiDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDaLam";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauTraLoiDaLamDTO cauTraLoi = new CauTraLoiDaLamDTO
                            {
                                MaCauTraLoiDaLam = Convert.ToInt32(reader["MaCauTraLoiDaLam"]),
                                MaCauHoiDaLam = Convert.ToInt32(reader["MaCauHoiDaLam"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IsDapAn = Convert.ToInt32(reader["IsDapAn"]),
                                IsChon = Convert.ToInt32(reader["IsChon"])
                            };
                            cauTraLoiList.Add(cauTraLoi);
                        }
                    }
                }
            }
            return cauTraLoiList;
        }

        public CauTraLoiDaLamDTO GetById(CauTraLoiDaLamDTO cauTraLoi)
        {
            CauTraLoiDaLamDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDaLam WHERE MaCauTraLoiDaLam = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cauTraLoi.MaCauTraLoiDaLam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauTraLoiDaLamDTO
                            {
                                MaCauTraLoiDaLam = Convert.ToInt32(reader["MaCauTraLoiDaLam"]),
                                MaCauHoiDaLam = Convert.ToInt32(reader["MaCauHoiDaLam"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IsDapAn = Convert.ToInt32(reader["IsDapAn"]),
                                IsChon = Convert.ToInt32(reader["IsChon"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(CauTraLoiDaLamDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauTraLoiDaLam SET MaCauHoiDaLam = @MaCauHoiDaLam, NoiDung = @NoiDung, IsDapAn = @IsDapAn, IsChon = @IsChon WHERE MaCauTraLoiDaLam = @MaCauTraLoiDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTraLoiDaLam", cauTraLoi.MaCauTraLoiDaLam);
                        command.Parameters.AddWithValue("@MaCauHoiDaLam", cauTraLoi.MaCauHoiDaLam);
                        command.Parameters.AddWithValue("@NoiDung", cauTraLoi.NoiDung);
                        command.Parameters.AddWithValue("@IsDapAn", cauTraLoi.IsDapAn);
                        command.Parameters.AddWithValue("@IsChon", cauTraLoi.IsChon);
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
        public int GetAutoIncrement()//lay MaLop
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT MaCauTraLoiDaLam FROM CauTraLoiDaLam";
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

        public List<CauTraLoiDaLamDTO> GetCauTraLoiDaLamOfDeThi(int maDe)
        {
            List<CauTraLoiDaLamDTO> list = new List<CauTraLoiDaLamDTO>();
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = @"SELECT ctl* FROM ChiTietDeDaLam ctd
                                    INNER JOIN CauHoiDaLam ch ON ctd.MaCauHoi = ch.MaCauHoi
                                    INNER JOIN CauTraLoi ctl ON ch.MaCauHoi = ctl.MaCauHoi
                                    WHERE ctd.MaDe = @MaDe ORDER BY ctl.MaCauHoi ASC ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", maDe);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CauTraLoiDaLamDTO cauTraLoi = new CauTraLoiDaLamDTO
                                {
                                    MaCauTraLoiDaLam = Convert.ToInt32(reader["MaCauTraLoiDaLam"]),
                                    MaCauHoiDaLam = Convert.ToInt32(reader["MaCauHoiDaLam"]),
                                    NoiDung = reader["NoiDung"].ToString(),
                                    IsDapAn = Convert.ToInt32(reader["IsDapAn"]),
                                    IsChon = Convert.ToInt32(reader["IsChon"])
                                };
                                list.Add(cauTraLoi);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return list;
        }


        public List<CauTraLoiDaLamDTO> GetAllByMaCauHoi(int MaCauHoi)
        {
            List<CauTraLoiDaLamDTO> results = new List<CauTraLoiDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDaLam WHERE MaCauHoiDaLam = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauTraLoiDaLamDTO cautraloidalam = new CauTraLoiDaLamDTO
                            {
                                MaCauTraLoiDaLam = Convert.ToInt32(reader["MaCauTraLoiDaLam"]),
                                MaCauHoiDaLam = Convert.ToInt32(reader["MaCauHoiDaLam"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IsDapAn = Convert.ToInt32(reader["IsDapAn"]),
                                IsChon = Convert.ToInt32(reader["IsChon"])
                            };

                            results.Add(cautraloidalam); // Thêm đối tượng vào danh sách
                        }
                    }
                }
            }
            return results;
        }
         
    }
}
