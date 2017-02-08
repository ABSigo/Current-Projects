using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateAppDataObjects;
using System.Data;




namespace ElevateAppDataAccess
{
 
    public class TrainerAccessor
    {
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/07
        /// </summary>
        /// <returns>List of trainers</returns>
        public static List<Trainer> RetrieveTrainers()
        {
            var trainers = new List<Trainer>();

            var conn = DBConnection.GetConnection();

            var cmdText = @"SELECT TrainerID, TrainerFirstName, TrainerLastName, TrainerPhoneNumber, " +
                        @"TrainerStatus, TrainerPoleLevel, TrainerAcroLevel, TrainerSilksLevel, " +
                        @"TrainerLyraLevel " +
                        @"FROM Trainer ";

            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var tra = new Trainer()
                            {
                                TrainerID = reader.GetString(0),
                                TrainerFirstName = reader.GetString(1),
                                TrainerLastName = reader.GetString(2),
                                TrainerPhoneNumber = reader.GetString(3),
                                TrainerStatus = reader.GetBoolean(4),
                                TrainerPoleLevel = reader.GetString(5),
                                TrainerAcroLevel = reader.GetString(6),
                                TrainerSilksLevel = reader.GetString(7),
                                TrainerLyraLevel = reader.GetString(8)
                            };
                        trainers.Add(tra);
                    }
                    reader.Close();
                }
                else
                {
                    throw new ApplicationException("No Trainers Found");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retreiving your Trainer Data", ex);
            }
            finally
            {
                conn.Close();
            }
            return trainers;
        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/07
        /// </summary>
        /// <param name="TrainerStatus"></param>
        /// <returns> count for active Trainers</returns>

        public static int RetrieveTrainerCount(bool TrainerStatus = true)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();

            var activeState = TrainerStatus ? "1" : "0";

            var cmdText = @"SELECT COUNT(TrainerID) " +
                        @"FROM Trainer " +
                        @"WHERE TrainerStatus = " + activeState;

            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();

                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem accessing the count", ex);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        /// <summary>
        /// Ariel Sigo  
        /// Created 2016/11/07
        /// </summary>
        /// <param name="trainerID"></param>
        /// <param name="newTrainerID"></param>
        /// <returns>count for Updating email for Trainer (trainerID)</returns>
 
        public static int UpdateTrainerEmail(string trainerID, string newTrainerID)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_update_trainerID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TrainerID", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@NewTrainerID", SqlDbType.VarChar, 100);

            cmd.Parameters["@TrainerID"].Value = trainerID;
            cmd.Parameters["@NewTrainerID"].Value = newTrainerID;

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

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/07
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Trainer by trainer email if successful</returns>
    public static Trainer getTrainer(string email)
    {
        Trainer trainer = null;

        var conn = DBConnection.GetConnection();
        var cmdText = @"sp_retrieve_trainer_by_trainerID";
        var cmd = new SqlCommand(cmdText, conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@TrainerID", SqlDbType.VarChar, 100);

        cmd.Parameters["@TrainerID"].Value = email;
        try
        {
            conn.Open();

            var reader = cmd.ExecuteReader();
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trainer = new Trainer()
                        {
                            TrainerID = reader.GetString(0),
                            TrainerFirstName = reader.GetString(1),
                            TrainerLastName = reader.GetString(2),
                            TrainerPhoneNumber = reader.GetString(3),
                            TrainerStatus = reader.GetBoolean(4),
                            TrainerPoleLevel = reader.GetString(5),
                            TrainerAcroLevel = reader.GetString(6),
                            TrainerSilksLevel = reader.GetString(7),
                            TrainerLyraLevel = reader.GetString(8),

                        };
                        return trainer;
                    }
                }
            }
        }
	catch (Exception)
	{
		
		throw;

	}
    finally
        {
            conn.Close();
        }
    return trainer;
    }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/07
        /// </summary>
        /// <param name="trainerID"></param>
        /// <param name="oldPasswordHash"></param>
        /// <param name="newPasswordHash"></param>
        /// <returns>count of rows affected for trainer passwords</returns>
    public static int UpdateTrainerPasswordHash(string trainerID, string oldPasswordHash, string newPasswordHash)
    {
        var count = 0;

        var conn = DBConnection.GetConnection();
        var cmdText = @"sp_update_trainer_passwordHash";
        var cmd = new SqlCommand(cmdText, conn);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@TrainerID", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@OldPasswordHash", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@NewPasswordHash", SqlDbType.VarChar, 100);

        cmd.Parameters["@TrainerID"].Value = trainerID;
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
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/07
        /// </summary>
        /// <param name="OldTrainerID"></param>
        /// <param name="NewTrainerID"></param>
        /// <param name="OldTrainerFirstName"></param>
        /// <param name="NewTrainerFirstName"></param>
        /// <param name="OldTrainerLastName"></param>
        /// <param name="NewTrainerLastName"></param>
        /// <param name="OldTrainerPhoneNumber"></param>
        /// <param name="NewTrainerPhoneNumber"></param>
        /// <param name="OldTrainerStatus"></param>
        /// <param name="NewTrainerStatus"></param>
        /// <param name="OldTrainerPoleLevel"></param>
        /// <param name="NewTrainerPoleLevel"></param>
        /// <param name="OldTrainerAcroLevel"></param>
        /// <param name="NewTrainerAcroLevel"></param>
        /// <param name="OldTrainerSilksLevel"></param>
        /// <param name="NewTrainerSilksLevel"></param>
        /// <param name="OldTrainerLyraLevel"></param>
        /// <param name="NewTrainerLyraLevel"></param>
        /// <returns>Count of rows affected and trainers updated if successful</returns>
    public static int UpdateTrainer(string OldTrainerID, string NewTrainerID, string OldTrainerFirstName, string NewTrainerFirstName, string OldTrainerLastName, string NewTrainerLastName, string OldTrainerPhoneNumber, string NewTrainerPhoneNumber, bool OldTrainerStatus, bool NewTrainerStatus, string OldTrainerPoleLevel, string NewTrainerPoleLevel, string OldTrainerAcroLevel, string NewTrainerAcroLevel, string OldTrainerSilksLevel, string NewTrainerSilksLevel, string OldTrainerLyraLevel, string NewTrainerLyraLevel)
    {
        var count = 0;

        var conn = DBConnection.GetConnection();
        var cmdText = "sp_update_trainer";
        var cmd = new SqlCommand(cmdText, conn);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@OldTrainerID", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@NewTrainerID", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@OldTrainerFirstName", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@NewTrainerFirstName", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@OldTrainerLastName", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@NewTrainerLastName", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@OldTrainerPhoneNumber", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@NewTrainerPhoneNumber", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@OldTrainerStatus", SqlDbType.Bit);
        cmd.Parameters.Add("@NewTrainerStatus", SqlDbType.Bit);
        cmd.Parameters.Add("@OldTrainerPoleLevel", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@NewTrainerPoleLevel", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@OldTrainerAcroLevel", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@NewTrainerAcroLevel", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@OldTrainerSilksLevel", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@NewTrainerSilksLevel", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@OldTrainerLyraLevel", SqlDbType.VarChar, 15);
        cmd.Parameters.Add("@NewTrainerLyraLevel", SqlDbType.VarChar, 15);

        cmd.Parameters["@OldTrainerID"].Value = OldTrainerID;
        cmd.Parameters["@NewTrainerID"].Value = NewTrainerID;
        cmd.Parameters["@OldTrainerFirstName"].Value = OldTrainerFirstName;
        cmd.Parameters["@NewTrainerFirstName"].Value = NewTrainerFirstName;
        cmd.Parameters["@OldTrainerLastName"].Value = OldTrainerLastName;
        cmd.Parameters["@NewTrainerLastName"].Value = NewTrainerLastName;
        cmd.Parameters["@OldTrainerPhoneNumber"].Value = OldTrainerPhoneNumber;
        cmd.Parameters["@NewTrainerPhoneNumber"].Value = NewTrainerPhoneNumber;
        cmd.Parameters["@OldTrainerStatus"].Value = OldTrainerStatus;
        cmd.Parameters["@NewTrainerStatus"].Value = NewTrainerStatus;
        cmd.Parameters["@OldTrainerPoleLevel"].Value = OldTrainerPoleLevel;
        cmd.Parameters["@NewTrainerPoleLevel"].Value = NewTrainerPoleLevel;
        cmd.Parameters["@OldTrainerAcroLevel"].Value = OldTrainerAcroLevel;
        cmd.Parameters["@NewTrainerAcroLevel"].Value = NewTrainerAcroLevel;
        cmd.Parameters["@OldTrainerSilksLevel"].Value = OldTrainerSilksLevel;
        cmd.Parameters["@NewTrainerSilksLevel"].Value = NewTrainerSilksLevel;
        cmd.Parameters["@OldTrainerLyraLevel"].Value = OldTrainerLyraLevel;
        cmd.Parameters["@NewTrainerLyraLevel"].Value = NewTrainerLyraLevel;

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
