using DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class CauTraLoiDAL : IUnitCTLCT<CauTraLoiDTO>
    {
        public static CauTraLoiDAL getInstance()
        {
            return new CauTraLoiDAL();
        }

        public bool Add(CauTraLoiDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO CauTraLoi (MaCauHoi, NoiDung, is_DapAn)" +
                        "VALUES (@MaCauHoi, @NoiDung, @IsDapAn);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauHoi", cauTraLoi.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", cauTraLoi.NoiDung);
                        command.Parameters.AddWithValue("@IsDapAn", cauTraLoi.IsDapAn);
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
        public List<CauTraLoiDTO> getByMaCauHoi(int mch)
        {
            List<CauTraLoiDTO> cauTraLoiList = new List<CauTraLoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * from CauTraLoi where MaCauHoi = " + mch + " ORDER BY MaCauTL ASC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauTraLoiDTO cauTraLoi = new CauTraLoiDTO
                            {
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                MaCauTL = Convert.ToInt32(reader["MaCauTL"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IsDapAn = Convert.ToInt32(reader["is_DapAn"]),
                            };
                            cauTraLoiList.Add(cauTraLoi);
                        }
                    }

                }
            }
            return cauTraLoiList;
        }

        public bool Delete(CauTraLoiDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "DELETE FROM CauTraLoi WHERE MaCauTL = @MaCauTL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTL", cauTraLoi.MaCauTL);
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

        public List<CauTraLoiDTO> GetAll(int MaCauHoi)
        {
            List<CauTraLoiDTO> cauTraLoiList = new List<CauTraLoiDTO>();
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoi where MaCauHoi=@MaCauHoi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaCauHoi", MaCauHoi);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CauTraLoiDTO cauTraLoi = new CauTraLoiDTO
                            {
                                MaCauTL = Convert.ToInt32(reader["MaCauTL"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IsDapAn = Convert.ToInt32(reader["is_DapAn"])
                            };
                            cauTraLoiList.Add(cauTraLoi);
                        }
                    }
                }
            }
            return cauTraLoiList;
        }

        public CauTraLoiDTO GetById(CauTraLoiDTO cauTraLoi)
        {
            CauTraLoiDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM CauTraLoi WHERE MaCauTL = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cauTraLoi.MaCauTL);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new CauTraLoiDTO
                            {
                                MaCauTL = Convert.ToInt32(reader["MaCauTL"]),
                                MaCauHoi = Convert.ToInt32(reader["MaCauHoi"]),
                                NoiDung = reader["NoiDung"].ToString(),
                                IsDapAn = Convert.ToInt32(reader["IsDapAn"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(CauTraLoiDTO cauTraLoi)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE CauTraLoi SET MaCauHoi = @MaCauHoi, NoiDung = @NoiDung, IsDapAn = @IsDapAn WHERE MaCauTL = @MaCauTL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCauTL", cauTraLoi.MaCauTL);
                        command.Parameters.AddWithValue("@MaCauHoi", cauTraLoi.MaCauHoi);
                        command.Parameters.AddWithValue("@NoiDung", cauTraLoi.NoiDung);
                        command.Parameters.AddWithValue("@IsDapAn", cauTraLoi.IsDapAn);
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

