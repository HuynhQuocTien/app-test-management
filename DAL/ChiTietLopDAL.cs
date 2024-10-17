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
    public class ChiTietLopDAL : IUnitDAL<ChiTietLopDTO>
    {
        public static ChiTietLopDAL GetInstance()
        {
            return new ChiTietLopDAL();
        }
        public bool Add(ChiTietLopDTO ctl)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "INSERT INTO ChiTietLop (MaLop, MaSV, TrangThai,is_delete)" +
                        "VALUES (@MaLop, @MaSV, @TrangThai,@is_delete)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", ctl.MaLop);
                        command.Parameters.AddWithValue("@MaSV", ctl.MaSV);
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

        public bool Delete(ChiTietLopDTO ctl)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE ChiTietLop SET is_delete = 1 WHERE MaLop = @MaLop AND MaSV = @MaSV";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", ctl.MaLop);
                        command.Parameters.AddWithValue("@MaSV", ctl.MaSV);
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

        public List<ChiTietLopDTO> GetAll()
        {
            List<ChiTietLopDTO> ctlList = new List<ChiTietLopDTO>();
            try
            {
                using (SqlConnection conn = GetConnectionDb.GetConnection())
                {
                    string query = "SELECT * FROM ChiTietLop WHERE is_delete = 0";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChiTietLopDTO ctl = new ChiTietLopDTO
                                {
                                    MaChiTietLop = Convert.ToInt32(reader["MaChiTietLop"]),
                                    MaLop = Convert.ToInt32(reader["MaLop"]),
                                    MaSV = Convert.ToInt64(reader["MaSV"]),
                                    TrangThai = Convert.ToInt32(reader["TrangThai"])
                                };
                                ctlList.Add(ctl);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ctlList;
        }

        public ChiTietLopDTO GetById(ChiTietLopDTO ctl)
        {
            ChiTietLopDTO result = null;
            using (SqlConnection connection = GetConnectionDb.GetConnection())
            {
                string query = "SELECT * FROM ChiTietLop WHERE MaChiTietLop = @MaChiTietLop and is_delete = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaChiTietLop", ctl.MaChiTietLop);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new ChiTietLopDTO
                            {
                                MaChiTietLop = Convert.ToInt32(reader["MaChiTietLop"]),
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                MaSV = Convert.ToInt64(reader["MaSV"]),
                                TrangThai = Convert.ToInt32(reader["TrangThai"])
                            };
                        }
                    }
                }
            }
            return result;
        }

        public bool Update(ChiTietLopDTO ctl)
        {
            try
            {
                using (SqlConnection connection = GetConnectionDb.GetConnection())
                {
                    string query = "UPDATE ChiTietLop SET MaLop = @MaLop, MaSV = @MaSV where MaChiTietLop = @MaChiTietLop";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChiTietLop", ctl.MaChiTietLop);
                        command.Parameters.AddWithValue("@MaLop", ctl.MaLop);
                        command.Parameters.AddWithValue("@MaSV", ctl.MaSV);
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
