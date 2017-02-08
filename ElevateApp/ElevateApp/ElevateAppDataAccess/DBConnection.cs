using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateAppDataAccess
{
    static class DBConnection
    {
        internal static SqlConnection GetConnection()
        {

            // this is our connection string

            // this string is for @School
            // var connString = @"Data Source=localhost; Initial Catalog=gymDB;Integrated Security=True";

            // this string is for @Personal
            var connString = @"Data Source=localhost\SQLEXPRESS; Initial Catalog=gymDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
