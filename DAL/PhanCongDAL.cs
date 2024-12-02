using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PhanCongDAL : IUnitDAL<PhanCongDTO>
    {
        public static PhanCongDAL getInstance()
        {
            return new PhanCongDAL();
        }

        public bool Add(PhanCongDTO phanCong)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO PhanCong (MaMonHoc, MaGV) VALUES (@MaMonHoc, @MaGV);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                        command.Parameters.AddWithValue("@MaGV", phanCong.MaGV);
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

        public bool Delete(PhanCongDTO phanCong)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM PhanCong WHERE MaPhanCong = @MaPhanCong";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
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

        public bool DeleteMAGV( string maGV)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "Delete FROM PhanCong Where MaGV=@MaGV;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaGV",maGV);
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

        public List<PhanCongDTO> GetAll()
        {
            List<PhanCongDTO> phanCongList = new List<PhanCongDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT PC.MaPhanCong  as 'Mã Phân Công',  PC.MaMonHoc as 'Mã Môn Học', PC.MaGV as 'Mã Giáo viên' FROM PhanCong PC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhanCongDTO phanCong = new PhanCongDTO
                            {
                                MaPhanCong = Convert.ToInt32(reader["Mã Phân Công"]),
                                MaMonHoc = Convert.ToInt32(reader["Mã Môn Học"]),
                                MaGV = Convert.ToInt64(reader["Mã Giáo viên"]),
                            };
                            phanCongList.Add(phanCong);
                        }
                    }
                }
            }
            return phanCongList;
        }

        public PhanCongDTO GetById(PhanCongDTO phanCong)
        {
            PhanCongDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM PhanCong WHERE MaPhanCong = @MaPhanCong";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new PhanCongDTO
                            {
                                MaPhanCong = Convert.ToInt32(reader["MaPhanCong"]),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaGV = Convert.ToInt64(reader["MaGV"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(PhanCongDTO phanCong)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE PhanCong SET MaMonHoc = @MaMonHoc, MaGV = @MaGV WHERE MaPhanCong = @MaPhanCong";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
                        command.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                        command.Parameters.AddWithValue("@MaGV", phanCong.MaGV);
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

        public List<PhanCongDTO> GetTimKiem(string timkiem)
        {
            List<PhanCongDTO> phanCongList = new List<PhanCongDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT PC.MaPhanCong  as 'Mã Phân Công',  PC.MaMonHoc as 'Mã Môn Học', PC.MaGV as 'Mã Giáo viên' FROM PhanCong PC WHERE PC.MaGV LIKE @Ten OR PC.MaMonHoc LIKE @TenMonHoc";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ten", "%" + timkiem + "%");
                    command.Parameters.AddWithValue("@TenMonHoc", "%" + timkiem + "%");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhanCongDTO phancong = new PhanCongDTO
                            {
                                MaPhanCong = Convert.ToInt32(reader["Mã Phân Công"]),
                                MaMonHoc = Convert.ToInt32(reader["Mã Môn Học"]),
                                MaGV = Convert.ToInt64(reader["Mã Giáo viên"]),
                            };
                            phanCongList.Add(phancong);
                        }
                    }
                }
            }
            return phanCongList;
        }

        public DataTable GetDataForPage(int startRecord, int recordsPerPage)
        {
            DataTable dt = new DataTable();

            // Chuỗi kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                // Truy vấn với OFFSET và FETCH NEXT để lấy dữ liệu theo trang
                string query = @"
            SELECT PC.MaPhanCong  as 'Mã Phân Công',  PC.MaMonHoc as 'Mã Môn Học', PC.MaGV as 'Mã Giáo viên' FROM PhanCong PC
            ORDER BY PC.MaPhanCong 
            OFFSET @StartRecord ROWS 
            FETCH NEXT @RecordsPerPage ROWS ONLY";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm các tham số cho OFFSET và FETCH NEXT
                    cmd.Parameters.AddWithValue("@StartRecord", startRecord);
                    cmd.Parameters.AddWithValue("@RecordsPerPage", recordsPerPage);

                    // Khởi tạo SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable loadComboBox()
        {
            DataTable dt = new DataTable();

            // Chuỗi kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                // Truy vấn với OFFSET và FETCH NEXT để lấy dữ liệu theo trang
                string query = @"
                SELECT ND.Ten as 'TenNguoiDung', ND.MaNguoiDung as 'MaNguoiDung' FROM NguoiDung ND INNER JOIN TaiKhoan TK ON ND.MaNguoiDung = TK.Username INNER JOIN NhomQuyen NQ ON NQ.MaNhomQuyen = TK.MaNhomQuyen WHERE NQ.MaNhomQuyen = 2;";
                // Khởi tạo SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                
            }
            return dt;
        }

        public DataTable loadListboxPC(string MaGV)
        {
            DataTable dt = new DataTable();

            // Chuỗi kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                // Truy vấn với OFFSET và FETCH NEXT để lấy dữ liệu theo trang
                string query = @"
                SELECT MH.TenMonHoc, MH.MaMonHoc FROM PhanCong PC INNER JOIN MonHoc MH ON MH.MaMonHoc = PC.MaMonHoc WHERE PC.MaGV = @MaGV;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", MaGV);

                    // Khởi tạo SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable loadListboxCPC(string MaGV)
        {
            DataTable dt = new DataTable();

            // Chuỗi kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                // Truy vấn với OFFSET và FETCH NEXT để lấy dữ liệu theo trang
                string query = @"
                SELECT MH.TenMonHoc, MH.MaMonHoc FROM MonHoc MH WHERE NOT EXISTS (SELECT 1 FROM PhanCong PC WHERE PC.MaMonHoc = MH.MaMonHoc AND PC.MaGV = @MaGV);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", MaGV);

                    // Khởi tạo SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public bool CheckPCExists(string id)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    string query = "SELECT COUNT(1) FROM PhanCong WHERE MaGV = @MaGV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGV", id);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Trả về true nếu có bản ghi tồn tại
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }

        public bool checkPhanCongOfMonHoc(long maGV, int maMon)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    string query = "SELECT COUNT(1) FROM PhanCong WHERE MaGV = @MaGV AND MaMonHoc = @MaMonHoc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGV", maGV);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMon);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Trả về true nếu có bản ghi tồn tại
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }
    }
}
