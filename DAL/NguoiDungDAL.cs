using DTO;
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
                    string query = "INSERT INTO NguoiDung (HoTen, GioiTinh, NgaySinh, Avatar, SDT, NgayTao, TrangThai, is_delete) VALUES (@HoTen, @GioiTinh, @NgaySinh, @Avatar, @SDT, @NgayTao, @TrangThai, @is_delete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
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

        public bool Delete(NguoiDungDTO nguoiDung)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
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

        public List<NguoiDungDTO> GetAll()
        {
            List<NguoiDungDTO> nguoiDungList = new List<NguoiDungDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM NguoiDung";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NguoiDungDTO nguoiDung = new NguoiDungDTO
                            {
                                MaNguoiDung = Convert.ToInt32(reader["MaNguoiDung"]),
                                HoTen = reader["HoTen"].ToString(),
                                GioiTinh = Convert.ToInt32( reader["GioiTinh"].ToString()),
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
                                GioiTinh = Convert.ToInt32(reader["GioiTinh"].ToString()),
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
    }
}
