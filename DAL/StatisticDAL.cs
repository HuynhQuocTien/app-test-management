using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StatisticDAL
    {

        public int TongCauHoi()
        {
            int tong = 0;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT COUNT(*) as TongCauHoi FROM CauHoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tong = Convert.ToInt32(reader["TongCauHoi"]);
                        }
                    }
                }
            }
            return tong;
        }

        public int TongTaiKhoan(int maNhomQuyen)
        {
            int tong = 0;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT COUNT(*) as Tong FROM TaiKhoan WHERE MaNhomQuyen = " + maNhomQuyen;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tong = Convert.ToInt32(reader["Tong"]);
                        }
                    }
                }
            }
            return tong;
        }

        public int ThongKe(int maNhomQuyen)
        {
            int tong = 0;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT COUNT(*) as Tong FROM TaiKhoan WHERE MaNhomQuyen = " + maNhomQuyen;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tong = Convert.ToInt32(reader["Tong"]);
                        }
                    }
                }
            }
            return tong;
        }

        public List<ThongKeDiemTheoLopDTO> ThongKeDiemTheoLop()
        {
            List<ThongKeDiemTheoLopDTO> list = new List<ThongKeDiemTheoLopDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT \r\n    l.MaLop,\r\n    l.TenLop,\r\n    AVG(kq.Diem) AS DiemTrungBinh,\r\n    COUNT(kq.Diem) AS SoBaiThi   \r\nFROM \r\n    KetQua kq\r\nJOIN \r\n    DeThi dt ON dt.MaDe = kq.MaDe\r\nJOIN \r\n    MonHoc mh ON mh.MaMonHoc = dt.MaMonHoc\r\nJOIN \r\n    ChiTietLop ctl ON ctl.MaSV = kq.MaND\r\nJOIN \r\n    Lop l ON l.MaLop = ctl.MaLop\r\nGROUP BY \r\n    l.MaLop, l.TenLop;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeDiemTheoLopDTO thongKe = new ThongKeDiemTheoLopDTO
                            {
                                maLop = Convert.ToInt32(reader["MaLop"]),
                                tenLop = reader["TenLop"].ToString(),
                                diemTrungBinh = Convert.ToDecimal(reader["DiemTrungBinh"]),
                                soBaiThi = Convert.ToInt32(reader["SoBaiThi"])
                            };
                            list.Add(thongKe);
                        }
                    }
                }
            }
            return list;
        }

        public List<ThongKeDiemTheoMonDTO> ThongKeDiemTheoMon()
        {
            List<ThongKeDiemTheoMonDTO> list = new List< ThongKeDiemTheoMonDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "\tSELECT \r\n    mh.TenMonHoc,\r\n    mh.MaMonHoc,\r\n    AVG(kq.Diem) AS DiemTrungBinh, \r\n    COUNT(kq.Diem) AS SoBaiThi \r\nFROM \r\n    KetQua kq\r\nJOIN \r\n    DeThi dt ON dt.MaDe = kq.MaDe\r\nJOIN \r\n    MonHoc mh ON mh.MaMonHoc = dt.MaMonHoc\r\nGROUP BY \r\n    mh.TenMonHoc, mh.MaMonHoc;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeDiemTheoMonDTO thongKe = new  ThongKeDiemTheoMonDTO
                            {
                                maMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                tenMonHoc = reader["TenMonHoc"].ToString(),
                                diemTrungBinh = Convert.ToDecimal(reader["DiemTrungBinh"]),
                                soBaiThi = Convert.ToInt32(reader["SoBaiThi"])
                            };
                            list.Add(thongKe);
                        }
                    }
                }
            }
            return list;
        }

        public List<ThongKeSVThamGiaThiTheoLopDTO> ThongKeSVThamGiaThiTheoLop()
        {
            List<ThongKeSVThamGiaThiTheoLopDTO> list = new List<ThongKeSVThamGiaThiTheoLopDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT l.MaLop, l.TenLop, COUNT(MaND) as SoLuongSV\r\nFROM KetQua kq\r\nJOIN \r\n\tDeThi dt ON dt.MaDe = kq.MaDe\r\nJOIN \r\n    ChiTietLop ctl ON ctl.MaSV = kq.MaND\r\nJOIN \r\n    Lop l ON l.MaLop = ctl.MaLop\r\nGROUP BY l.TenLop, l.MaLop;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeSVThamGiaThiTheoLopDTO thongKe = new ThongKeSVThamGiaThiTheoLopDTO
                            {
                                maLop = Convert.ToInt32(reader["MaLop"]),
                                tenLop = reader["TenLop"].ToString(),
                                soLuongSinhVien = Convert.ToInt32(reader["SoLuongSV"])
                            };
                            list.Add(thongKe);
                        }
                    }
                }
            }
            return list;
        }

        public List<ThongKeSVThamGiaThiTheoMonDTO> ThongKeSVThamGiaThiTheoMon()
        {
            List<ThongKeSVThamGiaThiTheoMonDTO> list = new List<ThongKeSVThamGiaThiTheoMonDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT mh.MaMonHoc, mh.TenMonHoc, COUNT(MaND) as SoLuongSV\r\n\r\nFROM KetQua kq\r\n\r\nJOIN DeThi dt ON \r\ndt.MaDe = kq.MaDe\r\nJOIN MonHoc mh ON\r\nmh.MaMonHoc = dt.MaMonHoc\r\nGROUP BY mh.TenMonHoc, mh.MaMonHoc;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeSVThamGiaThiTheoMonDTO thongKe = new ThongKeSVThamGiaThiTheoMonDTO
                            {
                                maMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                tenMonHoc = reader["TenMonHoc"].ToString(),
                                soLuongSV = Convert.ToInt32(reader["SoLuongSV"])
                            };
                            list.Add(thongKe);
                        }
                    }
                }
            }
            return list;
        }

        public List<ThongKeDTBTheoMon> ThongKeDTBTheoMon()
        {
            List<ThongKeDTBTheoMon> list = new List<ThongKeDTBTheoMon>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT mh.MaMonHoc, mh.TenMonHoc, AVG(kq.Diem) AS DiemTrungBinh\r\nFROM KetQua kq\r\nJOIN DeThi dt ON \r\ndt.MaDe = kq.MaDe\r\nJOIN MonHoc mh ON\r\nmh.MaMonHoc = dt.MaMonHoc\r\nGROUP BY mh.TenMonHoc, mh.MaMonHoc;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeDTBTheoMon thongKe = new ThongKeDTBTheoMon
                            {
                                maMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                tenMonHoc = reader["TenMonHoc"].ToString(),
                                diemTrungBinh = Convert.ToDecimal(reader["DiemTrungBinh"])
                            };
                            list.Add(thongKe);
                        }
                    }
                }
            }
            return list;
        }

        public List<ThongKeDTBTheoLop> ThongKeDTBTheoLop()
        {
            List<ThongKeDTBTheoLop> list = new List<ThongKeDTBTheoLop>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT l.MaLop, l.TenLop, AVG(kq.dIEM) AS DiemTrungBinh\r\nFROM KetQua kq\r\nJOIN \r\n\tDeThi dt ON dt.MaDe = kq.MaDe\r\nJOIN \r\n    ChiTietLop ctl ON ctl.MaSV = kq.MaND\r\nJOIN \r\n    Lop l ON l.MaLop = ctl.MaLop\r\nGROUP BY l.TenLop, l.MaLop";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeDTBTheoLop thongKe = new ThongKeDTBTheoLop
                            {
                                maLop = Convert.ToInt32(reader["MaLop"]),
                                tenLop = reader["TenLop"].ToString(),
                                diemTrungBinh = Convert.ToDecimal(reader["DiemTrungBinh"])
                            };
                            list.Add(thongKe);
                        }
                    }
                }
            }
            return list;
        }

        public int SoLuongCauHoi()
        {
            int soLuongCauHoi = 0;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT COUNT(*) AS SoLuongCauHoi FROM CauHoi;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            soLuongCauHoi = Convert.ToInt32(reader["SoLuongCauHoi"]);
                        }
                    }
                }
            }
            return soLuongCauHoi;
        }
    }
} 
