using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class CauHoiDaLamDAL : IUnitDAL<CauHoiDaLamDTO>
    {
        public static CauHoiDaLamDAL getInstance()
        {
            return new CauHoiDaLamDAL();
        }

        public bool Add(CauHoiDaLamDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauHoiDaLam (NoiDung, IdNguoiTao, MaMonHoc, DoKho,LoaiCauHoi)" +
                        "VALUES (@NoiDung, @IdNguoiTao, @MaMonHoc, @DoKho,@LoaiCauHoi);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@IdNguoiTao", cauHoi.IdNguoiTao);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
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

        public bool Delete(CauHoiDaLamDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauHoiDaLam SET is_delete = @is_delete WHERE MaCauHoiDaLam = @MaCauHoiDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@is_delete", 1);
                        command.Parameters.AddWithValue("@MaCauHoiDaLam", cauHoi.MaCauHoiDaLam);
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

        public List<CauHoiDaLamDTO> GetAll()
        {
            List<CauHoiDaLamDTO> cauHoiList = new List<CauHoiDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoiDaLam WHERE is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDaLamDTO cauHoi = new CauHoiDaLamDTO
                            {
                                MaCauHoiDaLam = Convert.ToInt32(reader["MaCauHoiDaLam"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IdNguoiTao = Convert.ToInt64(reader["IdNguoiTao"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                DoKho = Convert.ToInt32(reader["DoKho"].ToString()),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),

                            };
                            cauHoiList.Add(cauHoi);
                        }
                    }
                }
            }
            return cauHoiList;
        }

        public CauHoiDaLamDTO GetById(CauHoiDaLamDTO cauHoi)
        {
            CauHoiDaLamDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoiDaLam WHERE MaCauHoiDaLam = @id AND TrangThai = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cauHoi.MaCauHoiDaLam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauHoiDaLamDTO
                            {
                                MaCauHoiDaLam = Convert.ToInt32(reader["MaCauHoiDaLam"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IdNguoiTao = Convert.ToInt64(reader["IdNguoiTao"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                DoKho = Convert.ToInt32(reader["DoKho"].ToString()),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),

                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(CauHoiDaLamDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauHoiDaLam SET NoiDung = @NoiDung, IdNguoiTao = @IdNguoiTao, MaMonHoc = @MaMonHoc, DoKho = @DoKho, LoaiCauHoi = @LoaiCauHoi WHERE MaCauHoiDaLam = @MaCauHoiDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoiDaLam", cauHoi.MaCauHoiDaLam);
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@IdNguoiTao", cauHoi.IdNguoiTao);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
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
        public int GetAutoIncrement()//lay MaCauHoiDaLam
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT MaCauHoiDaLam FROM CauHoiDaLam";
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
