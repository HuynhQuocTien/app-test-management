using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ThongKeDAL
    {
        public static ThongKeDAL GetInstance()
        {
            return new ThongKeDAL();
        }
        public int getCountCauHoi()
        {
            int result = -1;
            try
            {
                using (SqlConnection conn = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT COUNT(MaCauHoi) FROM CauHoi WHERE is_delete = 0";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        result = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

        public int getCountHs()
        {
            int result = -1;
            try
            {
                using (SqlConnection conn = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT COUNT(NguoiDung.MaNguoiDung) from NguoiDung " +
                        " JOIN TaiKhoan ON NguoiDung.MaNguoiDung = TaiKhoan.Username " +
                        " JOIN NhomQuyen ON NhomQuyen.MaNhomQuyen = TaiKhoan.MaNhomQuyen " +
                        " WHERE NhomQuyen.MaNhomQuyen = 3 AND NguoiDung.is_delete = 0";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        result = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

        public int getCountGv()
        {
            int result = -1;
            try
            {
                using (SqlConnection conn = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT COUNT(NguoiDung.MaNguoiDung) from NguoiDung " +
                        " JOIN TaiKhoan ON NguoiDung.MaNguoiDung = TaiKhoan.Username " +
                        " JOIN NhomQuyen ON NhomQuyen.MaNhomQuyen = TaiKhoan.MaNhomQuyen " +
                        " WHERE (NhomQuyen.MaNhomQuyen = 2 OR NhomQuyen.MaNhomQuyen = 4) AND NguoiDung.is_delete = 0";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        result = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
        public Dictionary<NguoiDungDTO,KetQuaDTO> GetAllDiemTBCuaHs(int maLop)
        {
            Dictionary<NguoiDungDTO, KetQuaDTO> list = new Dictionary<NguoiDungDTO, KetQuaDTO>();
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                string query = "SELECT TOP 5 NguoiDung.*, KetQua.Diem FROM NguoiDung " +
                    "JOIN KetQua ON NguoiDung.MaNguoiDung = KetQua.MaND " +
                    "JOIN GiaoDeThi ON GiaoDeThi.MaDe = KetQua.MaDe " +
                    "JOIN Lop ON GiaoDeThi.MaLop = Lop.MaLop " +
                    "JOIN DeThi ON DeThi.MaDe = KetQua.MaDe " + 
                    "ORDER BY KetQua.Diem desc";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NguoiDungDTO HS = new NguoiDungDTO
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
                            KetQuaDTO diemTBCuaHS = new KetQuaDTO
                            {
                                Diem = Convert.ToDecimal(reader["Diem"].ToString()),
                            };

                            list.Add(HS,diemTBCuaHS);
                        }
                    }
                }
            }
            return list;
        }

        public int getSlHsDaNopBai(int maLop, int maDeThi)
        {
            int result = -1;
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                string query = "SELECT count(NguoiDung.MaNguoiDUng) as sLHSDaNopBai " +
                    "FROM NguoiDung JOIN KetQua ON NguoiDung.MaNguoiDung = KetQua.MaND " +
                    "JOIN DeThi ON KetQua.MaDe = DeThi.MaDe " +
                    "JOIN GiaoDeThi ON KetQua.MaDe = GiaoDeThi.MaDe " +
                    "JOIN Lop ON Lop.MaLop = GiaoDeThi.MaLop " +
                    "WHERE Lop.MaLop = " + maLop + " AND DeThi.MaDe = " + maDeThi;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    result = (int)command.ExecuteScalar();
                }
            }
            return result;
        }

        public Dictionary<NguoiDungDTO, KetQuaDTO> GetTop5HsCoDiemCaoNhatTheoDeThi(int maLop, int maDeThi)
        {
            Dictionary<NguoiDungDTO, KetQuaDTO> list = new Dictionary<NguoiDungDTO, KetQuaDTO>();

            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                string query = "SELECT TOP 5 NguoiDung.*, KetQua.Diem FROM NguoiDung " +
               "JOIN KetQua ON NguoiDung.MaNguoiDung = KetQua.MaND " +
               "JOIN GiaoDeThi ON KetQua.MaDe = GiaoDeThi.MaDe " +
               "JOIN Lop ON Lop.MaLop = GiaoDeThi.MaLop " +
               "JOIN DeThi ON DeThi.MaDe = GiaoDeThi.MaDe " +
               "WHERE Lop.MaLop = @maLop AND DeThi.MaDe = @maDeThi " +
               "GROUP BY NguoiDung.MaNguoiDung, NguoiDung.Ten, NguoiDung.GioiTinh, NguoiDung.NgaySinh, NguoiDung.Avatar, NguoiDung.SDT, NguoiDung.NgayTao, NguoiDung.TrangThai, NguoiDung.is_delete, KetQua.Diem " +
               "ORDER BY KetQua.Diem DESC";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@maLop", maLop);
                    command.Parameters.AddWithValue("@maDeThi", maDeThi);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NguoiDungDTO HS = new NguoiDungDTO
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
                            KetQuaDTO diemTBCuaHS = new KetQuaDTO
                            {
                                Diem = Convert.ToDecimal(reader["Diem"].ToString()),
                            };
                            list.Add(HS, diemTBCuaHS);
                        }
                    }
                }
            }
            return list;
        }


        public double getDiemCuaDeThiByUserId(int maLop, int maDeThi, long userId)
        {
            double result = -1;
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                string query = "SELECT KetQua.Diem FROM Lop " +
                    "JOIN GiaoDeThi ON GiaoDeThi.MaLop = Lop.MaLop " +
                    "JOIN KetQua on KetQua.MaDe = GiaoDeThi.MaDe " +
                    "JOIN NguoiDung ON NguoiDung.MaNguoiDung = KetQua.MaND " +
                    "WHERE GiaoDeThi.MaLop = " + maLop +
                    " AND KetQua.MaDe = " + maDeThi + " AND KetQua.MaND = " + userId +
                    " GROUP BY KetQua.Diem";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    var resultObject = command.ExecuteScalar();
                    if (resultObject != null && resultObject != DBNull.Value)
                    {
                        result = Convert.ToDouble(resultObject);
                    }
                }
            }
            return result;
        }

        public int GetSoLuongDeThi(int maLop)
        {
            int result = -1;
            try
            {
                using (SqlConnection conn = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT COUNT(DeThi.MaDe) from DeThi " +
                        " JOIN GiaoDeTHi ON GiaoDeThi.MaDe = DeThi.MaDe " +
                        " WHERE DeThi.is_delete = 0 AND DeThi.MaDe = " + maLop.ToString();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        result = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
    }
}
