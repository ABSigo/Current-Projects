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
       public static List<Class> RetrieveClass(bool active = true)
       {
           var classes = new List<Class>();

           var conn = DBConnection.GetConnection();

           var cmdText = "@SELECT ClassID, ClassDate, ClassTime, SkillID, LEvelID " +
                         "@FROM Class " +
                         "@WHERE Active = @active";

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
    }
}
