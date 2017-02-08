using ElevateAppDataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateAppDataAccess
{
    public class UserAccessor
    {
        /// <summary>
        /// Ariel Sigo  
        /// Created 2016/11/07
        /// </summary>
        /// <param name="email"></param>
        /// <param name="PasswordHash"></param>
        /// <param name="isTrainer"></param>
        /// <returns>result if user is member or trainer</returns>
        public static int VerifyEmailAndPassword(string email, string PasswordHash, bool isTrainer = true)
        {
            var result = 0;

            var conn = DBConnection.GetConnection();
            SqlCommand cmd;
            if (isTrainer == true)
            {
                var cmdtext = @"sp_authenticate_trainer";
                cmd = new SqlCommand(cmdtext, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TrainerID", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);

                cmd.Parameters["@TrainerID"].Value = email;
                cmd.Parameters["@PasswordHash"].Value = PasswordHash;
            }
            else 
            {
                var cmdtext = @"sp_authenticate_member";
                cmd = new SqlCommand(cmdtext, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);

                cmd.Parameters["@MemberID"].Value = email;
                cmd.Parameters["@PasswordHash"].Value = PasswordHash;
            }

            try
            {
                conn.Open();

                result = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
     




    }
}
