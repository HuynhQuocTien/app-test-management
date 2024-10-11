using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class GetConnectionDb
    {
        public static SqlConnection GetConnection()
        {
            string connectionsString = "Data Source=localhost;Initial Catalog=Test_Management_Db;User ID=sa;Password=123456;Integrated Security=True";

            SqlConnection sqlConn = new SqlConnection(connectionsString);
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            else
            {
                sqlConn.Close();
            }

            return sqlConn;
        }
        private static bool IsServerConnected()
        {
            string connectionsString = "Data Source=localhost;Initial Catalog=Test_Management_Db;User ID=sa;Password=123456;Trust Server Certificate=True";

            using (SqlConnection connection = new SqlConnection(connectionsString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        public static DataTable ExecuteQuery(string query)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }                    
                }
            }
        }

        public static int ExecuteNonQuery(string query)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
   
}
