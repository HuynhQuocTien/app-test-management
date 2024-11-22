using DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DeThiDAL : IUnitDAL<DeThiDTO>
    {
        public static DeThiDAL getInstance()
        {
            return new DeThiDAL();
        }
        public bool Add(DeThiDTO dethi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    //string query = "INSERT INTO DeThi (MaMon, TenDe, ThoiGianTao, ThoiGianBatDau,ThoiGianKetThuc,NguoiTao,TrangThai,is_delete)" +
                    //    "VALUES (@MaMon, @TenDe, @ThoiGianTao, @ThoiGianBatDau, @ThoiGianKetThuc,@NguoiTao,@TrangThai,@is_delete)";
                    string query = "INSERT INTO DeThi (MaMonHoc, TenDe, ThoiGianTao, ThoiGianBatDau, ThoiGianKetThuc, NguoiTao, TrangThai, is_delete)" +
                        " VALUES (@MaMonHoc, @TenDe, @ThoiGianTao, @ThoiGianBatDau, @ThoiGianKetThuc, @NguoiTao, @TrangThai, @is_delete)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", dethi.MaMonHoc);
                        command.Parameters.AddWithValue("@TenDe", dethi.TenDe);
                        command.Parameters.AddWithValue("@ThoiGianTao", dethi.ThoiGianTao);
                        command.Parameters.AddWithValue("@ThoiGianBatDau", dethi.ThoiGianBatDau);
                        command.Parameters.AddWithValue("@ThoiGianKetThuc", dethi.ThoiGianKetThuc);
                        command.Parameters.AddWithValue("@NguoiTao", dethi.NguoiTao);
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

        public bool Delete(DeThiDTO dethi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE DeThi SET is_delete = @is_delete where MaDe = @MaDe";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", dethi.MaDe);
                        command.Parameters.AddWithValue("@is_delete", 1);
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

        public List<DeThiDTO> GetAll()
        {
            List<DeThiDTO> dtList = new List<DeThiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM DeThi WHERE is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DeThiDTO dt = new DeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenDe = reader["TenDe"].ToString(),
                                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                                ThoiGianBatDau = Convert.ToDateTime(reader["ThoiGianBatDau"]),
                                ThoiGianKetThuc = Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                                NguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            dtList.Add(dt);
                        }
                    }

                }
            }
            return dtList;
        }

        public List<DeThiDTO> GetAll(long maNguoiTao)
        {
            List<DeThiDTO> dtList = new List<DeThiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                //string query = "SELECT * FROM DeThi WHERE is_delete = 0 AND NguoiTao = " + maNguoiTao;
                string query = "SELECT DeThi.*, MonHoc.TenMonHoc FROM DeThi JOIN MonHoc ON DeThi.MaMonHoc = MonHoc.MaMonHoc WHERE DeThi.is_delete = 0 AND DeThi.NguoiTao = " + maNguoiTao;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DeThiDTO dt = new DeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenDe = reader["TenDe"].ToString(),
                                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                                ThoiGianBatDau = Convert.ToDateTime(reader["ThoiGianBatDau"]),
                                ThoiGianKetThuc = Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                                NguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"]),
                                TenMonHoc = reader["TenMonHoc"].ToString()
                            };
                            dtList.Add(dt);
                        }
                    }

                }
            }
            return dtList;
        }

        public DeThiDTO GetById(DeThiDTO dethi)
        {
            DeThiDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM DeThi WHERE MaDe = @MaDe";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDe", dethi.MaDe);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new DeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenDe = reader["TenDe"].ToString(),
                                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                                ThoiGianBatDau = Convert.ToDateTime(reader["ThoiGianBatDau"]),
                                ThoiGianKetThuc = Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                                NguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(DeThiDTO dethi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE DeThi SET MaMonHoc = @MaMonHoc, TenDe = @TenDe, ThoiGianTao = @ThoiGianTao, " +
                        " ThoiGianBatDau = @ThoiGianBatDau, ThoiGianKetThuc = @ThoiGianKetThuc, NguoiTao = @NguoiTao" +
                        " WHERE MaDe = @MaDe and is_delete = 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", dethi.MaMonHoc);
                        command.Parameters.AddWithValue("@TenDe", dethi.TenDe);
                        command.Parameters.AddWithValue("@ThoiGianTao", dethi.ThoiGianTao);
                        command.Parameters.AddWithValue("@ThoiGianBatDau", dethi.ThoiGianBatDau);
                        command.Parameters.AddWithValue("@ThoiGianKetThuc", dethi.ThoiGianKetThuc);
                        command.Parameters.AddWithValue("@NguoiTao", dethi.NguoiTao);
                        command.Parameters.AddWithValue("@MaDe", dethi.MaDe);
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
        public int GetAutoIncrement()
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT MaDe from DeThi";
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

        public List<DeThiDTO> GetAllDeThiCuaLop(LopDTO lop)
        {
            List<DeThiDTO> dtList = new List<DeThiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT DeThi.* FROM DeThi JOIN GiaoDeThi ON DeThi.MaDe = GiaoDeThi.MaDe WHERE GiaoDeThi.is_delete = 0 AND GiaoDeThi.MaLop = " + lop.MaLop;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DeThiDTO dt = new DeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenDe = reader["TenDe"].ToString(),
                                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                                ThoiGianBatDau = Convert.ToDateTime(reader["ThoiGianBatDau"]),
                                ThoiGianKetThuc = Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                                NguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            dtList.Add(dt);
                        }
                    }

                }
            }
            return dtList;
        }
        public bool DeleteByMaDeThi(LopDTO lop, DeThiDTO deThi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE DeThi SET TrangThai = @TrangThai WHERE MaDe = @MaDe";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", deThi.MaDe);
                        command.Parameters.AddWithValue("@TrangThai", 0);
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
        public List<MonHocDTO> LayTenMonHoc()
        {
            List<MonHocDTO> dtList = new List<MonHocDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM MonHoc WHERE is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("sai");
                            MonHocDTO mh = new MonHocDTO
                            {
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenMonHoc = reader["TenMonHoc"].ToString()
                            };
                            dtList.Add(mh);
                        }
                    }

                }
            }
            return dtList;
        }
    }
}
