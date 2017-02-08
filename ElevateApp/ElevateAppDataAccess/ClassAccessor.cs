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
    public class ClassAccessor
    {
        /// <summary>
        /// Ariel Sigo 
        /// Created 2016/11/19
        /// 
        /// </summary>
        /// <param name="active"></param>
        /// <returns>list of classes if successful</returns>
        public static List<Class> RetrieveClass(bool active = true)
        {
            var classes = new List<Class>();

            var conn = DBConnection.GetConnection();

            var cmdText = @"SELECT ClassID, ClassDate, ClassTime, SkillID, LevelID, Active " +
                          @"FROM Class " +
                          @"WHERE Active = @active";

            var cmd = new SqlCommand(cmdText, conn);

            int activeBit = active ? 1 : 0;
            cmd.Parameters.Add("@active", SqlDbType.Bit);
            cmd.Parameters["@active"].Value = activeBit;
            // aggregate count function how many times classID shows up.

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cla = new Class()
                        {
                            ClassID = reader.GetInt32(0),
                            ClassDate = reader.GetDateTime(1),
                            ClassTime = reader.GetDateTime(2),
                            SkillID = reader.GetString(3),
                            LevelID = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                        classes.Add(cla);
                    }
                    reader.Close();
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
            return classes;

        }
        /// <summary>
        /// Ariel Sigo 
        /// Created 2017/01/31
        ///  
        /// Working to insert new classes to class list
        /// </summary>
        /// <param name="oldClassID"></param>
        /// <param name="newClassID"></param>
        /// <param name="oldClassDate"></param>
        /// <param name="newClassDate"></param>
        /// <param name="oldClassTime"></param>
        /// <param name="newClassTime"></param>
        /// <param name="oldSkillID"></param>
        /// <param name="newSkillid"></param>
        /// <param name="oldLevelId"></param>
        /// <param name="newLevelID"></param>
        /// <param name="oldActive"></param>
        /// <param name="newActive"></param>
        /// <returns> count of rows affected if successful</returns>
       // public static int InsertClass(int oldClassID, int newClassID, DateTime oldClassDate, DateTime newClassDate, DateTime oldClassTime, DateTime newClassTime, string oldSkillID, string newSkillid, string oldLevelId, string newLevelID, bool oldActive, bool newActive)
        //{
        //    var count = 0;

        //    var conn = DBConnection.GetConnection();
        //    var cmdText = "sp_insert_Class";
        //    var cmd = new SqlCommand(cmdText, conn);

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@OldClassID", SqlDbType.Int);
        //    cmd.Parameters.Add("@NewClassID", SqlDbType.Int);
        //    cmd.Parameters.Add("@OldClassDate", SqlDbType.DateTime);
        //    cmd.Parameters.Add("@NewClassDate", SqlDbType.DateTime);
        //    cmd.Parameters.Add("@OldClassTime", SqlDbType.DateTime);
        //    cmd.Parameters.Add("@NewClassTime", SqlDbType.DateTime);
        //    cmd.Parameters.Add("@OldSkillID", SqlDbType.VarChar, 25);
        //    cmd.Parameters.Add("@NewSkillID", SqlDbType.VarChar, 25);
        //    cmd.Parameters.Add("@OldLevelID", SqlDbType.VarChar, 25);
        //    cmd.Parameters.Add("@NewLevelID", SqlDbType.VarChar, 25);
        //    cmd.Parameters.Add("@OldActive", SqlDbType.Bit);
        //    cmd.Parameters.Add("@NewActive", SqlDbType.Bit);

        //    cmd.Parameters["@oldClassID"].Value = oldClassID;
        //    cmd.Parameters["@newClassID"].Value = newClassID;
        //    cmd.Parameters["@oldClassDate"].Value = oldClassDate;
        //    cmd.Parameters["@newClassDate"].Value = newClass
        //    cmd.Parameters["@oldClassTime"].Value = 
        //    cmd.Parameters["@newClassTime"].Value = 
        //    cmd.Parameters["@oldSkillID"].Value = 
        //    cmd.Parameters["@newSkillID"].Value = 
        //    cmd.Parameters["@oldLevelID"].Value = 
        //    cmd.Parameters["@newLevelID"].Value = 
        //    cmd.Parameters["@oldActive"].Value = 
        //    cmd.Parameters["@newActive"].Value = 
        //    cmd.Parameters[""].Value = 


        ////    return count;
        //}
    }

}