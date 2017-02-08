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
  public  class MemberAccessor
    {
      public static List<Member> RetrieveMembers(bool status = true)
      {
          var members = new List<Member>();

          // get a connection
          var conn = DBConnection.GetConnection();

          //command text
          var cmdText = @"SELECT MemberID, FirstName, LastName, PhoneNumber, Status, Birthday, StartDate, " +
                        @"MembershipTypeID, MemberPoleLevel, MemberAcroLevel, MemberSilksLevel, MemberLyraLevel " +
                        @"FROM Member " +
                        @"WHERE Status = @status ";

          // create a command object

          var cmd = new SqlCommand(cmdText, conn);

          // add parameters

          int activeBit = status ? 1 : 0;
          cmd.Parameters.Add("@status", SqlDbType.Bit);
          cmd.Parameters["@status"].Value = activeBit;

          try
          {
              //open connection
              conn.Open();

              // data reader
              var reader = cmd.ExecuteReader();

              // check if reader has data
              if (reader.HasRows)
              {
                  while (reader.Read())
                  {
                      var mem = new Member()
                      {
                          MemberID = reader.GetString(0),
                          FirstName = reader.GetString(1),
                          LastName = reader.GetString(3),
                          PhoneNumber = reader.GetString(4),
                          Status = reader.GetBoolean(5),
                          Birthday = reader.GetDateTime(6),
                          StartDate = reader.GetDateTime(7),
                          MembershipTypeID = reader.GetString(8),
                          MemberPoleLevel = reader.GetString(9),
                          MemberAcroLevel = reader.GetString(10),
                          MemberSilksLevel = reader.GetString(11),
                          MemberLyraLevel = reader.GetString(12)
                      };

                      // save the member before leaving the loop
                      members.Add(mem);
                  }
                  reader.Close();
              }
          }
          catch (Exception ex)
          {

              throw new ApplicationException("There was a problem retreiveing your Member Data.", ex);
          }
          finally
          { 
            //close connection
              conn.Close();
          }
          return members;
      }

      public static int RetrieveMemberCount(bool status = true)
      {
          var count = 0;

          // connection
          var conn = DBConnection.GetConnection();

          // variable for clause
          var activeState = status ? "1" : "0";

          // command text
          var cmdText = @"SELECT COUNT(MemberID) " +
                        @"FROM Member " +
                        @"WHERE Status = " + activeState;

          // command object 

          var cmd = new SqlCommand(cmdText, conn);

          try
          {
              //open connection
              conn.Open();

              // exectute command
              count = (int)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {

              throw new ApplicationException("There was a problem accessing the member count", ex);

          }
          finally
          { 
            //close the connection
              conn.Close();
          }

          return count;
      }
      public static int UpdateMember(string oldMemberID, string newMemberID, string oldFirstName, string newFirstName, string oldLastName, string newLastName, string oldPhoneNumber, string newPhoneNumber, bool oldStatus, bool newStatus, DateTime oldBirthday, DateTime newBirthday, DateTime oldStartDate, DateTime newStartDate, string oldMembershipTypeID, string newMembershipTypeID, string oldMemberPoleLevel, string newMemberPoleLevel, string oldMemberAcroLevel, string newMemberAcroLevel, string oldMemberSilksLevel, string newMemberSilksLevel, string oldMemberLyraLevel, string newMemberLyraLevel)
      {
          var count = 0;

          var conn = DBConnection.GetConnection();
          var cmdText = "sp_update_member";
          var cmd = new SqlCommand(cmdText, conn);

          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@OldMemberID", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@NewMemberID", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@OldFirstName", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@NewFirstName", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@OldLastName", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@NewLastName", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@OldStatus", SqlDbType.Bit);
          cmd.Parameters.Add("@NewStatus", SqlDbType.Bit);
          cmd.Parameters.Add("@OldBirthday", SqlDbType.Date);
          cmd.Parameters.Add("@NewBirthday", SqlDbType.Date);
          cmd.Parameters.Add("@OldStartDate", SqlDbType.Date);
          cmd.Parameters.Add("@NewStartDate", SqlDbType.Date);
          cmd.Parameters.Add("@OldMembershipTypeID", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@NewMembershipTypeID", SqlDbType.VarChar, 100);
          cmd.Parameters.Add("@OldMemberPoleLevel", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@NewMemberPoleLevel", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@OldMemberAcroLevel", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@NewMemberAcroLevel", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@OldMemberSilksLevel", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@NewMemberSilksLevel", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@OldMemberLyraLevel", SqlDbType.VarChar, 15);
          cmd.Parameters.Add("@NewMemberLyraLevel", SqlDbType.VarChar, 15);

          cmd.Parameters["@oldMemberID"].Value = oldMemberID;
          cmd.Parameters["@newMemberID"].Value = newMemberID;
          cmd.Parameters["@oldFirstName"].Value = oldFirstName;
          cmd.Parameters["@newFirstName"].Value = newFirstName;
          cmd.Parameters["@oldLastName"].Value = oldLastName;
          cmd.Parameters["@newLastName"].Value = newLastName;
          cmd.Parameters["@oldPhoneNumber"].Value = oldPhoneNumber;
          cmd.Parameters["@newPhoneNumber"].Value = newPhoneNumber;
          cmd.Parameters["@oldStatus"].Value = oldStatus;
          cmd.Parameters["@newStaus"].Value = newStatus;
          cmd.Parameters["@oldBirthday"].Value = oldBirthday;
          cmd.Parameters["@newBirthday"].Value = newBirthday;
          cmd.Parameters["@oldStartDate"].Value = oldStartDate;
          cmd.Parameters["@newStartDate"].Value = newStartDate;
          cmd.Parameters["@oldMembershipTypeID"].Value = oldMembershipTypeID;
          cmd.Parameters["@newMembershipTypeID"].Value = newMembershipTypeID;
          cmd.Parameters["@oldMemberPoleLevel"].Value = oldMemberPoleLevel;
          cmd.Parameters["@newMemberPoleLevel"].Value = newMemberPoleLevel;
          cmd.Parameters["@oldMemberAcroLevel"].Value = oldMemberAcroLevel;
          cmd.Parameters["@newMemberAcroLevel"].Value = newMemberAcroLevel;
          cmd.Parameters["@oldMemberSilksLevel"].Value = oldMemberSilksLevel;
          cmd.Parameters["@newMemberSilksLevel"].Value = newMemberSilksLevel;
          cmd.Parameters["@oldMemberLyraLevel"].Value = oldMemberLyraLevel;
          cmd.Parameters["@newMemberLyraLevel"].Value = newMemberLyraLevel;

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
      public static Member getMember(string email)
      {
          Member member = null;

          var conn = DBConnection.GetConnection();
          var cmdText = @"sp_retrieve_member_by_memberID";
          var cmd = new SqlCommand(cmdText, conn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 100);

          cmd.Parameters["@MemberID"].Value = email;
          try
          {
              conn.Open();

              var reader = cmd.ExecuteReader();
              {
                  if (reader.HasRows)
                  {
                      while (reader.Read())
                      {
                          member = new Member()
                          {
                              MemberID = reader.GetString(0),
                              FirstName = reader.GetString(1),
                              LastName = reader.GetString(2),
                              PhoneNumber = reader.GetString(3),
                              Status = reader.GetBoolean(4),
                              Birthday = reader.GetDateTime(5),
                              StartDate = reader.GetDateTime(6),
                              MembershipTypeID = reader.GetString(7),
                              MemberPoleLevel = reader.GetString(8),
                              MemberAcroLevel = reader.GetString(9),
                              MemberSilksLevel = reader.GetString(10),
                              MemberLyraLevel = reader.GetString(11)

                          };
                          return member;
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
          return member;
      }
                    
          
          
         
  }
}
