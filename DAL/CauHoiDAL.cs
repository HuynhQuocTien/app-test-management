using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class CauHoiDAL : IUnitCTL<CauHoiDTO>
    {
        public static CauHoiDAL getInstance()
        {
            return new CauHoiDAL();
        }

        public int Add(CauHoiDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauHoi (NoiDung, NguoiTao, MaMonHoc, DoKho, TrangThai, is_delete, LoaiCauHoi)" +
                        "VALUES (@NoiDung, @NguoiTao, @MaMonHoc, @DoKho, @TrangThai, @is_delete, @LoaiCauHoi); " +
                        "SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@NguoiTao", cauHoi.MaNguoiTao);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
                        command.Parameters.AddWithValue("@TrangThai", cauHoi.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", 0);
                        command.Parameters.AddWithValue("@LoaiCauHoi", cauHoi.LoaiCauHoi);
                        int maCauHoi = Convert.ToInt32(command.ExecuteScalar());
                        return maCauHoi;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public bool Delete(CauHoiDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauHoi SET is_delete = @is_delete WHERE MaCauHoi = @MaCauHoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@is_delete", 1);
                        command.Parameters.AddWithValue("@MaCauHoi", cauHoi.MaCauHoi);
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

        public List<CauHoiDTO> GetAll()
        {
            List<CauHoiDTO> cauHoiList = new List<CauHoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoi WHERE is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDTO cauHoi = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                DoKho = reader["DoKho"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            cauHoiList.Add(cauHoi);
                        }
                    }
                }
            }
            return cauHoiList;
        }

        public CauHoiDTO GetById(CauHoiDTO cauHoi)
        {
            CauHoiDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoi WHERE MaCauHoi = @id AND TrangThai = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cauHoi.MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaNguoiTao = Convert.ToInt64(reader["MaNguoiTao"]),
                                DoKho = reader["DoKho"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(CauHoiDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauHoi SET NoiDung = @NoiDung, MaMonHoc = @MaMonHoc, NguoiTao = @MaNguoiTao, DoKho = @DoKho WHERE MaCauHoi = @MaCauHoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", cauHoi.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@MaNguoiTao", cauHoi.MaNguoiTao);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
                        int rowsChanged = command.ExecuteNonQuery();
                        if (rowsChanged > 0)
                        {
                            string query1 = "DELETE FROM CauTraLoiDienChoTrong WHERE MaCauHoi = @MaCauHoi";

                            using (SqlCommand command1 = new SqlCommand(query1, connection))
                            {
                                command1.Parameters.AddWithValue("@MaCauHoi", cauHoi.MaCauHoi);
                                int rowsChanged1 = command1.ExecuteNonQuery();
                            }
                            return true;
                        } else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Import(CauHoiDTO cauHoi)
        {
           try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauHoi (NoiDung, NguoiTao, MaMonHoc, DoKho, TrangThai, is_delete, LoaiCauHoi)" +
                        "VALUES (@NoiDung, @NguoiTao, @MaMonHoc, @DoKho, @TrangThai, @is_delete, @LoaiCauHoi);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@NguoiTao", cauHoi.MaNguoiTao);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
                        command.Parameters.AddWithValue("@TrangThai", cauHoi.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", 0);
                        command.Parameters.AddWithValue("@LoaiCauHoi", cauHoi.LoaiCauHoi);
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
