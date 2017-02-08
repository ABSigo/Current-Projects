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
        public static int UpdateTrainerPasswordHash(string trainerId, string oldPasswordHash, string newPasswordHash)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_trainer_passwordhash";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TrainerID", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.VarChar, 100);

            cmd.Parameters["@TrainerID"].Value = trainerId;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public static int UpdateMemberPasswordHash(string memberId, string oldPasswordHash, string newPasswordHash)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_member_passwordhash";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.VarChar, 100);

            cmd.Parameters["@MemberID"].Value = memberId;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
         
        
        




    }
}
