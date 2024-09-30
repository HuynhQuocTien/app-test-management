using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class GetConnectionDb
    {
        public static SqlConnection GetConnectionString()
        {
            string connectionsString = "Data Source=LAPTOP-C31A5C9P;Initial Catalog=Test_Management_Db;Integrated Security=True";

            var sqlConn = new SqlConnection(connectionsString);
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
    }
}
