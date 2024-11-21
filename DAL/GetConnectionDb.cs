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
            //string connectionsString = "Data Source=LAPTOP-AN515-57\\SQLEXPRESS;Initial Catalog=Test_Management_Db;Integrated Security=True";
            //string connectionsString = "Data Source=LAPTOP-3M6UG0D2\\SQLEXPRESS;Initial Catalog=app-test-management;Integrated Security=True;";
            string connectionsString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=Test_Management_Db;Integrated Security=True";
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
    }
   
}
