using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
                    string query = "INSERT INTO KetQua (MaDe, MaNguoiDung, Diem, SoCauDung, SoCauSai, TrangThai, is_delete)" +
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
                                MaNguoiDung = Convert.ToInt32(reader["MaNguoiDung"]),
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
                                MaNguoiDung = Convert.ToInt32(reader["MaNguoiDung"]),
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
                    string query = "UPDATE KetQua SET MaDe = @MaDe, MaNguoiDung = @MaNguoiDung, Diem = @Diem, SoCauDung = @SoCauDung, SoCauSai = @SoCauSai, TrangThai = @TrangThai, is_delete = @is_delete WHERE MaKetQua = @MaKetQua";
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
    }
}


