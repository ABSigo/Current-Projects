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
    public class MemberAccessor
    {
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/03
        /// </summary>
        /// <param name="status"></param>
        /// <returns>List of members if successful</returns>
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
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/03
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Count of members if succesful</returns>
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
        /// <summary>
        ///  Ariel Sigo
        /// Created 2016/11/20
        /// </summary>
        /// <param name="oldMemberID"></param>
        /// <param name="newMemberID"></param>
        /// <param name="oldFirstName"></param>
        /// <param name="newFirstName"></param>
        /// <param name="oldLastName"></param>
        /// <param name="newLastName"></param>
        /// <param name="oldPhoneNumber"></param>
        /// <param name="newPhoneNumber"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <param name="oldBirthday"></param>
        /// <param name="newBirthday"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="newStartDate"></param>
        /// <param name="oldMembershipTypeID"></param>
        /// <param name="newMembershipTypeID"></param>
        /// <param name="oldMemberPoleLevel"></param>
        /// <param name="newMemberPoleLevel"></param>
        /// <param name="oldMemberAcroLevel"></param>
        /// <param name="newMemberAcroLevel"></param>
        /// <param name="oldMemberSilksLevel"></param>
        /// <param name="newMemberSilksLevel"></param>
        /// <param name="oldMemberLyraLevel"></param>
        /// <param name="newMemberLyraLevel"></param>
        /// <returns>count of member (rows) affected if successful</returns>
        public static int UpdateMember(string oldMemberID, string newMemberID, string oldFirstName, string newFirstName, string oldLastName, string newLastName, string oldPhoneNumber, string newPhoneNumber, bool oldStatus, bool newStatus, DateTime oldBirthday, DateTime newBirthday, DateTime oldStartDate, DateTime newStartDate, string oldMembershipTypeID, string newMembershipTypeID, string oldMemberPoleLevel, string newMemberPoleLevel, string oldMemberAcroLevel, string newMemberAcroLevel, string oldMemberSilksLevel, string newMemberSilksLevel, string oldMemberLyraLevel, string newMemberLyraLevel)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_member";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 100);
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

            cmd.Parameters["@MemberID"].Value = oldMemberID;
            cmd.Parameters["@OldMemberID"].Value = oldMemberID;
            cmd.Parameters["@NewMemberID"].Value = newMemberID;
            cmd.Parameters["@OldFirstName"].Value = oldFirstName;
            cmd.Parameters["@NewFirstName"].Value = newFirstName;
            cmd.Parameters["@OldLastName"].Value = oldLastName;
            cmd.Parameters["@NewLastName"].Value = newLastName;
            cmd.Parameters["@OldPhoneNumber"].Value = oldPhoneNumber;
            cmd.Parameters["@NewPhoneNumber"].Value = newPhoneNumber;
            cmd.Parameters["@OldStatus"].Value = oldStatus;
            cmd.Parameters["@NewStatus"].Value = newStatus;
            cmd.Parameters["@OldBirthday"].Value = oldBirthday;
            cmd.Parameters["@NewBirthday"].Value = newBirthday;
            cmd.Parameters["@OldStartDate"].Value = oldStartDate;
            cmd.Parameters["@NewStartDate"].Value = newStartDate;
            cmd.Parameters["@OldMembershipTypeID"].Value = oldMembershipTypeID;
            cmd.Parameters["@NewMembershipTypeID"].Value = newMembershipTypeID;
            cmd.Parameters["@OldMemberPoleLevel"].Value = oldMemberPoleLevel;
            cmd.Parameters["@NewMemberPoleLevel"].Value = newMemberPoleLevel;
            cmd.Parameters["@OldMemberAcroLevel"].Value = oldMemberAcroLevel;
            cmd.Parameters["@NewMemberAcroLevel"].Value = newMemberAcroLevel;
            cmd.Parameters["@OldMemberSilksLevel"].Value = oldMemberSilksLevel;
            cmd.Parameters["@NewMemberSilksLevel"].Value = newMemberSilksLevel;
            cmd.Parameters["@OldMemberLyraLevel"].Value = oldMemberLyraLevel;
            cmd.Parameters["@NewMemberLyraLevel"].Value = newMemberLyraLevel;

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
        /// Created 2016/11/03
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return Member by email if successful</returns>
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

         /// <summary>
        ///  Ariel Sigo
        /// Created 2016/11/04
         /// </summary>
         /// <param name="memberID"></param>
         /// <param name="oldPasswordHash"></param>
         /// <param name="newPasswordHash"></param>
         /// <returns>count of rows affected for members password being updated</returns>
        public static int UpdateMemberPasswordHash(string memberID, string oldPasswordHash, string newPasswordHash)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_member_passwordHash";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.VarChar, 100);

            cmd.Parameters["@MemberID"].Value = memberID;
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
        /// Created 2016/11/05
        /// 
        /// 
        /// </summary>
        /// <param name="MemberID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Status"></param>
        /// <param name="Birthday"></param>
        /// <param name="StartDate"></param>
        /// <param name="MembershipTypeID"></param>
        /// <param name="MemberPoleLevel"></param>
        /// <param name="MemberAcroLevel"></param>
        /// <param name="MemberSilksLevel"></param>
        /// <param name="MemberLyraLevel"></param>
        /// <returns>Count of rows affected if succesful</returns>
        public static int InsertMember(string MemberID, string FirstName, string LastName, string PhoneNumber, bool Status, DateTime Birthday, DateTime StartDate, string MembershipTypeID, string MemberPoleLevel, string MemberAcroLevel, string MemberSilksLevel, string MemberLyraLevel)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_insert_member";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("MemberID", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("FirstName", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("LastName", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("PhoneNumber", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("Status", SqlDbType.Bit);
            cmd.Parameters.Add("Birthday", SqlDbType.Date);
            cmd.Parameters.Add("StartDate", SqlDbType.Date);
            cmd.Parameters.Add("MembershipTypeID", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("MemberPoleLevel", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("MemberAcroLevel", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("MemberSilksLevel", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("MemberLyraLevel", SqlDbType.VarChar, 15);

            cmd.Parameters[@"MemberID"].Value = MemberID;
            cmd.Parameters[@"FirstName"].Value = FirstName;
            cmd.Parameters[@"LastName"].Value = LastName;
            cmd.Parameters[@"PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters[@"Status"].Value = Status;
            cmd.Parameters[@"Birthday"].Value = Birthday;
            cmd.Parameters[@"StartDate"].Value = StartDate;
            cmd.Parameters[@"MembershipTypeID"].Value = MembershipTypeID;
            cmd.Parameters[@"MemberPoleLevel"].Value = MemberPoleLevel;
            cmd.Parameters[@"MemberAcroLevel"].Value = MemberAcroLevel;
            cmd.Parameters[@"MemberSilksLevel"].Value = MemberSilksLevel;
            cmd.Parameters[@"MemberLyraLevel"].Value = MemberLyraLevel;

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
