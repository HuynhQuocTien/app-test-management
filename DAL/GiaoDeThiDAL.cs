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
                    string query = "INSERT INTO GiaoDeThi (MaDe, MaLop, NguoiGiao, is_delete)" +
                        "VALUES (@MaDe, @MaLop, @NguoiGiao, @is_delete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", giaoDeThi.MaDe);
                        command.Parameters.AddWithValue("@MaLop", giaoDeThi.MaLop);
                        command.Parameters.AddWithValue("@NguoiGiao", giaoDeThi.NguoiGiao);
                        command.Parameters.AddWithValue("@is_delete", giaoDeThi.IsDelete);
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

        public List<DeThiDTO> GetDeThiChuaThem(DeThiDTO dethi)
        {
            List<DeThiDTO> dethiList = new List<DeThiDTO>();
            //DeThiDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM DeThi";
                if (dethi.MaMonHoc != 0)
                {
                    query += " WHERE MaMonHoc = @MaMonHoc";
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (dethi.MaMonHoc != 0)
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", dethi.MaMonHoc);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DeThiDTO de = new DeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                TenDe = reader["TenDe"].ToString(),
                                ThoiGianTao = Convert.ToDateTime(reader["ThoiGianTao"]),
                                ThoiGianBatDau = Convert.ToDateTime(reader["ThoiGianBatDau"]),
                                ThoiGianKetThuc = Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                                ThoiGianLamBai = Convert.ToInt32(reader["ThoiGianLamBai"]),
                                NguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            dethiList.Add(de);
                        }
                    }
                }
            }
            return dethiList;
        }

        public List<PhanCongDTO> GetPhanCong(long maNguoiDung)
        {
            List<PhanCongDTO> dtList = new List<PhanCongDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM PhanCong WHERE MaGV = " + maNguoiDung;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhanCongDTO dt = new PhanCongDTO
                            {
                                MaPhanCong = Convert.ToInt32(reader["MaPhanCong"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaGV = Convert.ToInt64(reader["MaGV"])
                            };
                            dtList.Add(dt);
                        }
                    }

                }
            }
            return dtList;
        }
        public List<MonHocDTO> LayTenMonHoc(int maMonHoc)
        {
            List<MonHocDTO> dtList = new List<MonHocDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM MonHoc WHERE is_delete = 0 AND MaMonHoc = " + maMonHoc;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
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

        public List<GiaoDeThiDTO> GetGiaoDeThi(long maNguoiGiao, int maLop)
        {
            List<GiaoDeThiDTO> dtList = new List<GiaoDeThiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM GiaoDeThi WHERE NguoiGiao = @NguoiGiao AND MaLop = @MaLop";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NguoiGiao", maNguoiGiao);
                    command.Parameters.AddWithValue("@MaLop", maLop);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GiaoDeThiDTO dt = new GiaoDeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                NguoiGiao = Convert.ToInt64(reader["NguoiGiao"]),
                                IsDelete = Convert.ToInt32(reader["is_delete"])
                            };
                            dtList.Add(dt);
                        }
                    }

                }
            }
            return dtList;
        }

        public List<DeThiDTO> LayDeThiDaGiao(int maDe)
        {
            List<DeThiDTO> dtList = new List<DeThiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM DeThi WHERE is_delete = 0 AND MaDe = " + maDe;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DeThiDTO mh = new DeThiDTO
                            {
                                MaDe = Convert.ToInt32(reader["MaMonHoc"]),
                                TenDe = reader["TenMonHoc"].ToString(),
                                ThoiGianBatDau = Convert.ToDateTime(reader["ThoiGianBatDau"])
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

