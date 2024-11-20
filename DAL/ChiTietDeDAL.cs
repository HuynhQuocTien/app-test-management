using DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class ChiTietDeDAL : IUnitDAL<ChiTietDeDTO>
    {
        public static ChiTietDeDAL getInstance()
        {
            return new ChiTietDeDAL();
        }

        public bool Add(ChiTietDeDTO chiTietDe)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO ChiTietDe (MaDe, MaCauHoi) VALUES (@MaDe, @MaCauHoi);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", chiTietDe.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDe.MaCauHoi);
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

        public bool Delete(ChiTietDeDTO chiTietDe)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM ChiTietDe WHERE MaChiTietDe = @MaChiTietDe";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChiTietDe", chiTietDe.MaChiTietDe);
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

        public List<ChiTietDeDTO> GetAll()
        {
            List<ChiTietDeDTO> chiTietDeList = new List<ChiTietDeDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM ChiTietDe";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChiTietDeDTO chiTietDe = new ChiTietDeDTO
                            {
                                MaChiTietDe = Convert.ToInt32(reader["MaChiTietDe"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"])
                            };
                            chiTietDeList.Add(chiTietDe);
                        }
                    }
                }
            }
            return chiTietDeList;
        }

        public List<CauHoiDTO> GetAllCauHoiOfDeThi(DeThiDTO deThi)
        {
            List<CauHoiDTO> list = new List<CauHoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT cau_hoi.* FROM ChiTietDe" +
                    " INNER JOIN CauHoi ON ChiTietDe.MaCauHoi = CauHoi.MaCauHoi" +
                    " WHERE ChiTietDe.MaDe = " + deThi.MaDe;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //var enumDoKho = (EnumDoKho)Convert.ToInt32(reader["DoKho"].ToString());
                            //DoKho = EnumHelper.GetEnumDescription(enumDoKho) ,

                            CauHoiDTO item = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"].ToString()),
                                MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                DoKho = Convert.ToInt32(reader["DoKho"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])

                            };
                            list.Add(item);
                        }
                    }

                }
                connection.Close();
            }
            return list;

        }

        public ChiTietDeDTO GetById(ChiTietDeDTO chiTietDe)
        {
            ChiTietDeDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM ChiTietDe WHERE MaChiTietDe = @MaChiTietDe";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaChiTietDe", chiTietDe.MaChiTietDe);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new ChiTietDeDTO
                            {
                                MaChiTietDe = Convert.ToInt32(reader["MaChiTietDe"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(ChiTietDeDTO chiTietDe)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE ChiTietDe SET MaDe = @MaDe, MaCauHoi = @MaCauHoi WHERE MaChiTietDe = @MaChiTietDe";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChiTietDe", chiTietDe.MaChiTietDe);
                        command.Parameters.AddWithValue("@MaDe", chiTietDe.MaDe);
                        command.Parameters.AddWithValue("@MaCauHoi", chiTietDe.MaCauHoi);
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
        public List<CauHoiDTO> LayDoKho()
        {
            List<CauHoiDTO> doKhoList = new List<CauHoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT DISTINCT DoKho FROM CauHoi ORDER BY DoKho";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@MaMonHoc", dethi.MaMonHoc);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDTO doKho = new CauHoiDTO
                            {
                                DoKho = Convert.ToInt32(reader["DoKho"])
                            };
                            doKhoList.Add(doKho);
                        }
                    }
                }
            }
            return doKhoList;
        }


        public List<CauHoiDTO> GetCauHoiChuaThem(CauHoiDTO cauHoiDTO)
        {
            List<CauHoiDTO> dethiList = new List<CauHoiDTO>();

            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoi WHERE is_delete = 0";
                if (cauHoiDTO.MaMonHoc != 0)
                {
                    query += " AND MaMonHoc = @MaMonHoc";
                }

                if (cauHoiDTO.DoKho != 0)
                {
                    query += " AND DoKho = @DoKho";
                }
                if (!string.IsNullOrEmpty(cauHoiDTO.NoiDung))
                {
                    query += " AND NoiDung LIKE @NoiDung";
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (cauHoiDTO.MaMonHoc != 0)
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoiDTO.MaMonHoc);
                    }
                    if (cauHoiDTO.DoKho != 0)
                    {
                        command.Parameters.AddWithValue("@DoKho", cauHoiDTO.DoKho);
                    }
                    if (!string.IsNullOrEmpty(cauHoiDTO.NoiDung))
                    {
                        command.Parameters.AddWithValue("@NoiDung", $"%{cauHoiDTO.NoiDung}%");
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDTO de = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                //MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                DoKho = Convert.ToInt32(reader["DoKho"]),
                                //TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                //is_delete = Convert.ToInt32(reader["is_delete"]),
                                MaNguoiTao = reader.IsDBNull(reader.GetOrdinal("NguoiTao")) ? 0 : reader.GetInt64(reader.GetOrdinal("NguoiTao")),
                                TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? 0 : reader.GetInt32(reader.GetOrdinal("TrangThai")),
                                is_delete = reader.IsDBNull(reader.GetOrdinal("is_delete")) ? 0 : reader.GetInt32(reader.GetOrdinal("is_delete"))
                            };
                            dethiList.Add(de);
                        }
                    }
                }
            }
            return dethiList;
        }

        public List<CauHoiDTO> GetCauHoiListByMaDe(int maDe)
        {
            List<CauHoiDTO> cauHoiList = new List<CauHoiDTO>();

            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = @"
            SELECT ch.MaCauHoi, ch.NoiDung, ch.NguoiTao, ch.MaMonHoc, ch.DoKho, ch.TrangThai, ch.is_delete
            FROM CauHoi AS ch
            INNER JOIN ChiTietDe AS ctd ON ch.MaCauHoi = ctd.MaCauHoi
            WHERE ctd.MaDe = @MaDe";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDe", maDe);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDTO cauHoi = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                DoKho = Convert.ToInt32(reader["DoKho"]),
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


    }
}
