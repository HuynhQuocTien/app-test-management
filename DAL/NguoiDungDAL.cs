﻿using DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NguoiDungDAL : IUnitDAL<NguoiDungDTO>
    {
        public static NguoiDungDAL getInstance()
        {
            return new NguoiDungDAL();
        }
       
        public bool Add(NguoiDungDTO nguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO NguoiDung (MaNguoiDung, Ten, GioiTinh, NgaySinh, Avatar, SDT, NgayTao, TrangThai, is_delete) VALUES (@MaNguoiDung, @Ten, @GioiTinh, @NgaySinh, @Avatar, @SDT, @NgayTao, @TrangThai, @is_delete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
                        command.Parameters.AddWithValue("@Ten", nguoiDung.HoTen);
                        command.Parameters.AddWithValue("@GioiTinh", nguoiDung.GioiTinh);
                        command.Parameters.AddWithValue("@NgaySinh", nguoiDung.NgaySinh);
                        command.Parameters.AddWithValue("@Avatar", nguoiDung.Avatar);
                        command.Parameters.AddWithValue("@SDT", nguoiDung.SDT);
                        command.Parameters.AddWithValue("@NgayTao", nguoiDung.NgayTao);
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

        public bool Delete(NguoiDungDTO nguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    //string query = "DELETE FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
                    string query = "SELECT * FROM NguoiDung WHERE is_delete = 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
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
        public bool DeleteByMaNguoiDung(NguoiDungDTO nguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NguoiDung SET TrangThai = @TrangThai, is_delete = @is_delete WHERE MaNguoiDung = @MaNguoiDung";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
                        command.Parameters.AddWithValue("@is_delete", 1);
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
        public List<NguoiDungDTO> GetAll()
        {
            List<NguoiDungDTO> nguoiDungList = new List<NguoiDungDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NguoiDung WHERE is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NguoiDungDTO nguoiDung = new NguoiDungDTO
                            {
                                MaNguoiDung = Convert.ToInt64(reader["MaNguoiDung"]),
                                HoTen = reader["Ten"].ToString(),
                                GioiTinh = Convert.ToInt32(reader["GioiTinh"]),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                Avatar = reader["Avatar"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            nguoiDungList.Add(nguoiDung);
                        }
                    }
                }
            }
            return nguoiDungList;
        }

        //int? gioiTinh = null, int? trangThai = null

        public List<NguoiDungDTO> GetAllByCondition(int maNhomQuyen, long? Username = null)
        {
            List<NguoiDungDTO> nguoiDungList = new List<NguoiDungDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NguoiDung INNER JOIN TaiKhoan ON NguoiDung.MaNguoiDung = TaiKhoan.Username WHERE is_delete = 0";

                if (maNhomQuyen != 0)
                {
                    query += " AND TaiKhoan.MaNhomQuyen = @MaNhomQuyen";
                }

                if (Username.HasValue)
                {
                    query += " AND TaiKhoan.Username = @Username";
                }


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                  
                    if (maNhomQuyen != 0)
                    {
                        command.Parameters.AddWithValue("@MaNhomQuyen", maNhomQuyen);
                    }

                    if (Username.HasValue)
                    {
                        command.Parameters.AddWithValue("@Username", Username.Value);
                    }


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NguoiDungDTO nguoiDung = new NguoiDungDTO
                            {
                                MaNguoiDung = Convert.ToInt64(reader["MaNguoiDung"]),
                                HoTen = reader["Ten"].ToString(),
                                GioiTinh = Convert.ToInt32(reader["GioiTinh"]),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                Avatar = reader["Avatar"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                            nguoiDungList.Add(nguoiDung);
                        }
                    }
                }
            }
            return nguoiDungList;
        }



        public NguoiDungDTO GetById(NguoiDungDTO nguoiDung)
        {
            NguoiDungDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new NguoiDungDTO
                            {
                                MaNguoiDung = Convert.ToInt64(reader["MaNguoiDung"]),
                                HoTen = reader["Ten"].ToString(),
                                GioiTinh = Convert.ToInt32(reader["GioiTinh"]),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                Avatar = reader["Avatar"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }



      



        public bool Update(NguoiDungDTO nguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NguoiDung SET HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, Avatar = @Avatar, SDT = @SDT, NgayTao = @NgayTao, TrangThai = @TrangThai, is_delete = @is_delete WHERE MaNguoiDung = @MaNguoiDung";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
                        command.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                        command.Parameters.AddWithValue("@GioiTinh", nguoiDung.GioiTinh);
                        command.Parameters.AddWithValue("@NgaySinh", nguoiDung.NgaySinh);
                        command.Parameters.AddWithValue("@Avatar", nguoiDung.Avatar);
                        command.Parameters.AddWithValue("@SDT", nguoiDung.SDT);
                        command.Parameters.AddWithValue("@NgayTao", nguoiDung.NgayTao);
                        command.Parameters.AddWithValue("@TrangThai", nguoiDung.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", nguoiDung.is_delete);
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

        public bool UpdateInfo(string Ten, string SDT, string Avatar, string MaNguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE NguoiDung SET Ten = @HoTen, Avatar = @Avatar, SDT = @SDT WHERE MaNguoiDung = @MaNguoiDung";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNguoiDung", MaNguoiDung);
                        command.Parameters.AddWithValue("@HoTen", Ten);
                        command.Parameters.AddWithValue("@Avatar", Avatar);
                        command.Parameters.AddWithValue("@SDT", SDT);
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
        public string getTenQuyenByID(long id)
        {
            string tenQuyen = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT NhomQuyen.TenQuyen FROM NguoiDung INNER JOIN TaiKhoan ON NguoiDung.MaNguoiDung = TaiKhoan.Username INNER JOIN NhomQuyen ON TaiKhoan.MaNhomQuyen = NhomQuyen.MaNhomQuyen WHERE NguoiDung.MaNguoiDung = @MaNguoiDung";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNguoiDung", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tenQuyen = reader["TenQuyen"].ToString();
                        }
                    }
                }
            }
            return tenQuyen;
        }
    }
}
