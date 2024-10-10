using System;
using System.Data.SqlClient;

namespace DAL
{
    public static class GetConnectionDb
    {
        // Phương thức trả về SqlConnection đã được mở
        public static SqlConnection GetConnectionString()
        {
            string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=Database;Integrated Security=True";

            var sqlConn = new SqlConnection(connectionString);
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            return sqlConn; // Trả về kết nối đã được mở
        }
    }
}