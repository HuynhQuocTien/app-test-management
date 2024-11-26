using DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace DAL
{
    public class KetQuaDAL : IUnitDAL<KetQuaDTO>
    {
        public static KetQuaDAL getInstance()
        {
            return new KetQuaDAL();
        }

        public bool Add(KetQuaDTO ketQua)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO KetQua (MaDe, MaND, Diem, SoCauDung, SoCauSai, TrangThai, is_delete)" +
                        "VALUES (@MaDe, @MaNguoiDung, @Diem, @SoCauDung, @SoCauSai, @TrangThai, @is_delete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", ketQua.MaDe);
                        command.Parameters.AddWithValue("@MaNguoiDung", ketQua.MaNguoiDung);
                        command.Parameters.AddWithValue("@Diem", ketQua.Diem);
                        command.Parameters.AddWithValue("@SoCauDung", ketQua.SoCauDung);
                        command.Parameters.AddWithValue("@SoCauSai", ketQua.SoCauSai);
                        command.Parameters.AddWithValue("@TrangThai", ketQua.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", ketQua.is_delete);
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

        public bool Delete(KetQuaDTO ketQua)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM KetQua WHERE MaKetQua = @MaKetQua";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaKetQua", ketQua.MaKetQua);
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

        public KetQuaDTO Get(int MaDe, long MaSV)
        {
            KetQuaDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM KetQua WHERE MaDe = @MaDe AND MaND = @MaSV";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDe", MaDe);
                    command.Parameters.AddWithValue("@MaSV", MaSV);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new KetQuaDTO
                            {
                                MaKetQua = Convert.ToInt32(reader["MaKetQua"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaNguoiDung = Convert.ToInt64(reader["MaND"]),
                                Diem = Math.Round(Convert.ToDecimal(reader["Diem"]), 2),
                                SoCauDung = Convert.ToInt32(reader["SoCauDung"]),
                                SoCauSai = Convert.ToInt32(reader["SoCauSai"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public List<KetQuaDTO> GetAll()
        {
            List<KetQuaDTO> ketQuaList = new List<KetQuaDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM KetQua";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KetQuaDTO ketQua = new KetQuaDTO
                            {
                                MaKetQua = Convert.ToInt32(reader["MaKetQua"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaNguoiDung = Convert.ToInt64(reader["MaND"]),
                                Diem = Convert.ToInt32(reader["Diem"]),
                                SoCauDung = Convert.ToInt32(reader["SoCauDung"]),
                                SoCauSai = Convert.ToInt32(reader["SoCauSai"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            ketQuaList.Add(ketQua);
                        }
                    }
                }
            }
            return ketQuaList;
        }

        public KetQuaDTO GetById(KetQuaDTO ketQua)
        {
            KetQuaDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM KetQua WHERE MaKetQua = @MaKetQua";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKetQua", ketQua.MaKetQua);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new KetQuaDTO
                            {
                                MaKetQua = Convert.ToInt32(reader["MaKetQua"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaNguoiDung = Convert.ToInt64(reader["MaND"]),
                                Diem = Convert.ToInt32(reader["Diem"]),
                                SoCauDung = Convert.ToInt32(reader["SoCauDung"]),
                                SoCauSai = Convert.ToInt32(reader["SoCauSai"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public KetQuaDTO GetByMaDeAndMaND(int maDe, long maND)
        {
            KetQuaDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM KetQua WHERE MaDe = @MaDe AND MaND = @maND";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDe", maDe);
                    command.Parameters.AddWithValue("@MaND", maND);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new KetQuaDTO
                            {
                                MaKetQua = Convert.ToInt32(reader["MaKetQua"]),
                                MaDe = Convert.ToInt32(reader["MaDe"]),
                                MaNguoiDung = Convert.ToInt64(reader["MaND"]),
                                Diem = Convert.ToInt32(reader["Diem"]),
                                SoCauDung = Convert.ToInt32(reader["SoCauDung"]),
                                SoCauSai = Convert.ToInt32(reader["SoCauSai"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(KetQuaDTO ketQua)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE KetQua SET MaDe = @MaDe, MaND = @MaNguoiDung, Diem = @Diem, SoCauDung = @SoCauDung, SoCauSai = @SoCauSai, TrangThai = @TrangThai, is_delete = @is_delete WHERE MaKetQua = @MaKetQua";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaKetQua", ketQua.MaKetQua);
                        command.Parameters.AddWithValue("@MaDe", ketQua.MaDe);
                        command.Parameters.AddWithValue("@MaNguoiDung", ketQua.MaNguoiDung);
                        command.Parameters.AddWithValue("@Diem", ketQua.Diem);
                        command.Parameters.AddWithValue("@SoCauDung", ketQua.SoCauDung);
                        command.Parameters.AddWithValue("@SoCauSai", ketQua.SoCauSai);
                        command.Parameters.AddWithValue("@TrangThai", ketQua.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", ketQua.is_delete);
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

        public bool UpdateTrangThaiMoDapAn(int maLop, int maDe, int TrangThai)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE KetQua SET TrangThai = @TrangThai " +
                                   "WHERE MaDe = @MaDe AND MaND IN (SELECT MaSV FROM ChiTietLop WHERE MaLop = @MaLop)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TrangThai", TrangThai);
                        command.Parameters.AddWithValue("@MaDe", maDe);
                        command.Parameters.AddWithValue("@MaLop", maLop);
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

        public bool UpdateByMaDe(KetQuaDTO ketQua)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE KetQua SET TrangThai = @TrangThai WHERE MaDe = @MaDe";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaDe", ketQua.MaDe);
                        command.Parameters.AddWithValue("@TrangThai", ketQua.TrangThai);
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




        public int? GetTrangThaiByMaLopAndMaDe(int maLop, int maDe)
        {
            int? trangThai = null; // Use nullable int to handle cases where no record is found
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = @"SELECT kq.TrangThai 
                             FROM KetQua kq 
                             JOIN ChiTietLop ctl ON kq.MaND = ctl.MaSV 
                             WHERE ctl.MaLop = @MaLop AND kq.MaDe = @MaDe";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", maLop);
                        command.Parameters.AddWithValue("@MaDe", maDe);

                        // Execute the command and retrieve the result
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            trangThai = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return trangThai;
        }

        public int GetAutoIncrement()//lay MaKetQua
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT MaKetQua FROM KetQua";
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

        public bool checkDeThiInKetQua(DeThiDTO deThi)
        {
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM KetQua WHERE MaDe = @MaDe";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDe", deThi.MaDe);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}


