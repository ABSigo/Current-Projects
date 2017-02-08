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
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/01
        /// 
        /// one string for school, one string for personal
        /// </summary>
        /// <returns> A connection to DB if successful</returns>
        internal static SqlConnection GetConnection()
        {

            // this is our connection string

            // this string is for @School
           var connString = @"Data Source=localhost; Initial Catalog=elevateDB;Integrated Security=True";

            // this string is for @Personal
           // var connString = @"Data Source=localhost\SQLEXPRESS; Initial Catalog=elevateDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
