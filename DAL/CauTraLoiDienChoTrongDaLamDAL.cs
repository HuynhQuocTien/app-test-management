using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class CauTraLoiDienChoTrongDaLamDAL : IUnitDAL<CauTraLoiDienChoTrongDaLamDTO>
    {
        public static CauTraLoiDienChoTrongDaLamDAL getInstance()
        {
            return new CauTraLoiDienChoTrongDaLamDAL();
        }

        public bool Add(CauTraLoiDienChoTrongDaLamDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauTraLoiDienChoTrongDaLam (MaCauHoi, ViTri, CauTraLoiText, DapAnText, IsDelete) VALUES (@MaCauHoi, @ViTri, @CauTraLoiText, @DapAnText, @IsDelete);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", cauTraLoi.MaCauHoi);
                        command.Parameters.AddWithValue("@ViTri", cauTraLoi.ViTri);
                        command.Parameters.AddWithValue("@CauTraLoiText", cauTraLoi.CauTraLoiText);
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

        public bool Delete(CauTraLoiDienChoTrongDaLamDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM CauTraLoiDienChoTrongDaLam WHERE MaCauTLDienChoTrongDaLam = @MaCauTLDienChoTrongDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTLDienChoTrongDaLam", cauTraLoi.MaCauTLDienChoTrongDaLam);
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

        public List<CauTraLoiDienChoTrongDaLamDTO> GetAll()
        {
            List<CauTraLoiDienChoTrongDaLamDTO> cauTraLoiList = new List<CauTraLoiDienChoTrongDaLamDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDienChoTrongDaLam";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauTraLoiDienChoTrongDaLamDTO cauTraLoi = new CauTraLoiDienChoTrongDaLamDTO
                            {
                                MaCauTLDienChoTrongDaLam = Convert.ToInt32(reader["MaCauTLDienChoTrongDaLam"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                ViTri = Convert.ToInt32(reader["ViTri"]),
                                CauTraLoiText = reader["CauTraLoiText"].ToString(),
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

        public CauTraLoiDienChoTrongDaLamDTO GetById(CauTraLoiDienChoTrongDaLamDTO cauTraLoi)
        {
            CauTraLoiDienChoTrongDaLamDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoiDienChoTrongDaLam WHERE MaCauTLDienChoTrongDaLam = @MaCauTLDienChoTrongDaLam";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauTLDienChoTrongDaLam", cauTraLoi.MaCauTLDienChoTrongDaLam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauTraLoiDienChoTrongDaLamDTO
                            {
                                MaCauTLDienChoTrongDaLam = Convert.ToInt32(reader["MaCauTLDienChoTrongDaLam"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                ViTri = Convert.ToInt32(reader["ViTri"]),
                                CauTraLoiText = reader["CauTraLoiText"].ToString(),
                                DapAnText = reader["DapAnText"].ToString(),
                                IsDelete = Convert.ToInt32(reader["IsDelete"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(CauTraLoiDienChoTrongDaLamDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauTraLoiDienChoTrongDaLam SET MaCauHoi = @MaCauHoi, ViTri = @ViTri, CauTraLoiText = @CauTraLoiText, DapAnText = @DapAnText, IsDelete = @IsDelete WHERE MaCauTLDienChoTrongDaLam = @MaCauTLDienChoTrongDaLam";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTLDienChoTrongDaLam", cauTraLoi.MaCauTLDienChoTrongDaLam);
                        command.Parameters.AddWithValue("@MaCauHoi", cauTraLoi.MaCauHoi);
                        command.Parameters.AddWithValue("@ViTri", cauTraLoi.ViTri);
                        command.Parameters.AddWithValue("@CauTraLoiText", cauTraLoi.CauTraLoiText);
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

