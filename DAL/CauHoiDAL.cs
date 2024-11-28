using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CauHoiDAL : IUnitCTL<CauHoiDTO>
    {
        public static CauHoiDAL getInstance()
        {
            return new CauHoiDAL();
        }
        public List<CauHoiDTO> GetTimKiem(string timkiem, long MaNguoiTao)
        {
            List<CauHoiDTO> cauHoiList = new List<CauHoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoi WHERE (NoiDung LIKE @NoiDung OR LoaiCauHoi LIKE @LoaiCauHoi) AND NguoiTao=@MaNguoiTao AND is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NoiDung", "%" + timkiem + "%");
                    command.Parameters.AddWithValue("@NguoiTao", "%" + timkiem + "%");
                    command.Parameters.AddWithValue("@LoaiCauHoi", "%" + timkiem + "%");
                    command.Parameters.AddWithValue("@MaNguoiTao", MaNguoiTao);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDTO cauHoi = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
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

        public List<CauHoiDTO> GetTimKiemSelect(int dokho, string MaMonHoc, long MaNguoiTao)
        {
            List<CauHoiDTO> cauHoiList = new List<CauHoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoi WHERE DoKho = @DoKho AND MaMonHoc = @MaMonHoc AND NguoiTao=@MaNguoiTao AND is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoKho", dokho);
                    command.Parameters.AddWithValue("@MaMonHoc", MaMonHoc);
                    command.Parameters.AddWithValue("@MaNguoiTao", MaNguoiTao);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDTO cauHoi = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
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

        public DataTable GetDataForPage(int startRecord, int recordsPerPage, long MaNguoiTao)
        {
            DataTable dt = new DataTable();

            // Chuỗi kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                // Truy vấn với OFFSET và FETCH NEXT để lấy dữ liệu theo trang
                string query = @"
                SELECT MaCauHoi, NoiDung, NguoiTao AS MaNguoiTao,MaMonHoc,DoKho,TrangThai,is_delete, LoaiCauHoi 
                FROM CauHoi
                where is_delete=0 AND NguoiTao=@MaNguoiTao
                ORDER BY MaCauHoi 
                OFFSET @StartRecord ROWS 
                FETCH NEXT @RecordsPerPage ROWS ONLY";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm các tham số cho OFFSET và FETCH NEXT
                    cmd.Parameters.AddWithValue("@StartRecord", startRecord);
                    cmd.Parameters.AddWithValue("@RecordsPerPage", recordsPerPage);
                    cmd.Parameters.AddWithValue("@MaNguoiTao", MaNguoiTao);


                    // Khởi tạo SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public int Add(CauHoiDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauHoi (NoiDung, NguoiTao, MaMonHoc, DoKho, TrangThai, is_delete, LoaiCauHoi)" +
                        "VALUES (@NoiDung, @NguoiTao, @MaMonHoc, @DoKho, @TrangThai, @is_delete, @LoaiCauHoi); " +
                        "SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@NguoiTao", cauHoi.MaNguoiTao);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
                        command.Parameters.AddWithValue("@TrangThai", cauHoi.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", 0);
                        command.Parameters.AddWithValue("@LoaiCauHoi", cauHoi.LoaiCauHoi);
                        int maCauHoi = Convert.ToInt32(command.ExecuteScalar());
                        return maCauHoi;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public bool Delete(CauHoiDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauHoi SET is_delete = @is_delete WHERE MaCauHoi = @MaCauHoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@is_delete", 1);
                        command.Parameters.AddWithValue("@MaCauHoi", cauHoi.MaCauHoi);
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

        public List<CauHoiDTO> GetAll(long MaNguoiTao)
        {
            List<CauHoiDTO> cauHoiList = new List<CauHoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoi WHERE is_delete = 0 AND NguoiTao=@MaNguoiTao";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNguoiTao", MaNguoiTao);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauHoiDTO cauHoi = new CauHoiDTO
                            {                            
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
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

        public CauHoiDTO GetById(CauHoiDTO cauHoi)
        {
            CauHoiDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauHoi WHERE MaCauHoi = @id AND TrangThai = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cauHoi.MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauHoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaNguoiTao = Convert.ToInt64(reader["NguoiTao"]),
                                DoKho = Convert.ToInt32(reader["DoKho"]),
                                LoaiCauHoi = reader["LoaiCauHoi"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                is_delete = Convert.ToInt32(reader["is_delete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(CauHoiDTO cauHoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauHoi SET NoiDung = @NoiDung, MaMonHoc = @MaMonHoc, NguoiTao = @MaNguoiTao, DoKho = @DoKho WHERE MaCauHoi = @MaCauHoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", cauHoi.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@MaNguoiTao", cauHoi.MaNguoiTao);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
                        int rowsChanged = command.ExecuteNonQuery();
                        if (rowsChanged > 0)
                        {
                            if(cauHoi.LoaiCauHoi=="Trắc nghiệm")
                            {
                                string query1 = "DELETE FROM CauTraLoi WHERE MaCauHoi = @MaCauHoi";

                                using (SqlCommand command1 = new SqlCommand(query1, connection))
                                {
                                    command1.Parameters.AddWithValue("@MaCauHoi", cauHoi.MaCauHoi);
                                    int rowsChanged1 = command1.ExecuteNonQuery();
                                }
                            }
                            else if(cauHoi.LoaiCauHoi == "Điền từ")
                            {

                                string query1 = "DELETE FROM CauTraLoiDienChoTrong WHERE MaCauHoi = @MaCauHoi";

                                using (SqlCommand command1 = new SqlCommand(query1, connection))
                                {
                                    command1.Parameters.AddWithValue("@MaCauHoi", cauHoi.MaCauHoi);
                                    int rowsChanged1 = command1.ExecuteNonQuery();
                                }
                            }
                            return true;
                        } else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Import(CauHoiDTO cauHoi)
        {
           try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauHoi (NoiDung, NguoiTao, MaMonHoc, DoKho, TrangThai, is_delete, LoaiCauHoi)" +
                        "VALUES (@NoiDung, @NguoiTao, @MaMonHoc, @DoKho, @TrangThai, @is_delete, @LoaiCauHoi);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoiDung", cauHoi.NoiDung);
                        command.Parameters.AddWithValue("@NguoiTao", cauHoi.MaNguoiTao);
                        command.Parameters.AddWithValue("@MaMonHoc", cauHoi.MaMonHoc);
                        command.Parameters.AddWithValue("@DoKho", cauHoi.DoKho);
                        command.Parameters.AddWithValue("@TrangThai", cauHoi.TrangThai);
                        command.Parameters.AddWithValue("@is_delete", 0);
                        command.Parameters.AddWithValue("@LoaiCauHoi", cauHoi.LoaiCauHoi);
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
        public int GetAutoIncrement()//lay MaCauHoi
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT MaCauHoi FROM CauHoi";
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

        public int DemCauHoi()
        {
            int count = 0; // Biến lưu số lượng câu hỏi
            try
            {
                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    // Câu truy vấn để đếm số lượng câu hỏi
                    string query = "SELECT COUNT(*) AS TONGSOCAUHOI FROM CauHoi";

                    // Tạo SqlCommand với câu truy vấn và kết nối
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Thực thi truy vấn và lấy giá trị trả về
                        count = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại thông báo lỗi nếu xảy ra lỗi
                Console.WriteLine("Lỗi khi đếm số câu hỏi: " + ex.Message);
            }

            // Trả về số lượng câu hỏi
            return count;
        }

    }
}
