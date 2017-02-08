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

    }
}
