using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class CauTraLoiDienChoTrongDAL : IUnitCTLCT<CauTraLoiDienChoTrongDTO>
    {
        public static CauTraLoiDienChoTrongDAL getInstance()
        {
            return new CauTraLoiDienChoTrongDAL();
        }

        public bool Add(CauTraLoiDienChoTrongDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauTraLoiDienChoTrong (MaCauHoi, ViTri, DapAnText, IsDelete) VALUES (@MaCauHoi, @ViTri, @DapAnText, @IsDelete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", cauTraLoi.MaCauHoi);
                        command.Parameters.AddWithValue("@ViTri", cauTraLoi.ViTri);
                        command.Parameters.AddWithValue("@DapAnText", cauTraLoi.DapAnText);
                        command.Parameters.AddWithValue("@IsDelete", cauTraLoi.IsDelete);
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

        public bool Delete(CauTraLoiDienChoTrongDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM CauTraLoiDienChoTrong WHERE MaCauTLiDienChoTrong = @MaCauTLiDienChoTrong";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTLiDienChoTrong", cauTraLoi.MaCauTLiDienChoTrong);
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

        public List<CauTraLoiDienChoTrongDTO> GetAll(int MaCauHoi)
        {
            List<CauTraLoiDienChoTrongDTO> cauTraLoiList = new List<CauTraLoiDienChoTrongDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDienChoTrong where MaCauHoi=@MaCauHoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauHoi", MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauTraLoiDienChoTrongDTO cauTraLoi = new CauTraLoiDienChoTrongDTO
                            {
                                MaCauTLiDienChoTrong = Convert.ToInt32(reader["MaCauTLiDienChoTrong"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                ViTri = Convert.ToInt32(reader["ViTri"]),
                                DapAnText = reader["DapAnText"].ToString(),
                                IsDelete = Convert.ToInt32(reader["IsDelete"])
                            };
                            cauTraLoiList.Add(cauTraLoi);
                        }
                    }
                }
            }
            return cauTraLoiList;
        }

        public CauTraLoiDienChoTrongDTO GetById(CauTraLoiDienChoTrongDTO cauTraLoi)
        {
            CauTraLoiDienChoTrongDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDienChoTrong WHERE MaCauTLiDienChoTrong = @MaCauTLiDienChoTrong";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauTLiDienChoTrong", cauTraLoi.MaCauTLiDienChoTrong);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauTraLoiDienChoTrongDTO
                            {
                                MaCauTLiDienChoTrong = Convert.ToInt32(reader["MaCauTLiDienChoTrong"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                ViTri = Convert.ToInt32(reader["ViTri"]),
                                DapAnText = reader["DapAnText"].ToString(),
                                IsDelete = Convert.ToInt32(reader["IsDelete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public CauTraLoiDienChoTrongDTO GetCauTraLoiByMaCauHoiAndViTri(int maCauHoi, int vitri)
        {
            CauTraLoiDienChoTrongDTO result = new CauTraLoiDienChoTrongDTO();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDienChoTrong WHERE MaCauHoi = @MaCauHoi AND ViTri = @ViTri";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauHoi", maCauHoi);
                    command.Parameters.AddWithValue("@ViTri", vitri);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauTraLoiDienChoTrongDTO
                            {
                                MaCauTLiDienChoTrong = Convert.ToInt32(reader["MaCauTLiDienChoTrong"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                ViTri = Convert.ToInt32(reader["ViTri"]),
                                DapAnText = reader["DapAnText"].ToString(),
                                IsDelete = Convert.ToInt32(reader["IsDelete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(CauTraLoiDienChoTrongDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauTraLoiDienChoTrong SET MaCauHoi = @MaCauHoi, ViTri = @ViTri, DapAnText = @DapAnText, IsDelete = @IsDelete WHERE MaCauTLiDienChoTrong = @MaCauTLiDienChoTrong";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTLiDienChoTrong", cauTraLoi.MaCauTLiDienChoTrong);
                        command.Parameters.AddWithValue("@MaCauHoi", cauTraLoi.MaCauHoi);
                        command.Parameters.AddWithValue("@ViTri", cauTraLoi.ViTri);
                        command.Parameters.AddWithValue("@DapAnText", cauTraLoi.DapAnText);
                        command.Parameters.AddWithValue("@IsDelete", cauTraLoi.IsDelete);
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

