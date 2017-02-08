using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ElevateAppDataAccess;
using ElevateAppDataObjects;

namespace ElevateAppLogicLayer
{
    public class UserManager
    {
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/22
        /// 
        /// Needed for Password hash
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Hash value of source</returns>
        internal static string HashSHA256(string source)
        {
            var result = "";

            byte[] data;

            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));


            }

            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();
            return result;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/22
        /// 
        /// Making sure that login for user works
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>true if authentication succeeds</returns>
        public static bool AuthenticateUser(string email, string password)
        {
            var result = false;

            try
            {
                result = (1 == UserAccessor.VerifyEmailAndPassword(email, HashSHA256(password)));
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        /// <summary>
        /// Ariel Sigo  
        /// Created Created 2016/11/22
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="userType"></param>
        /// <returns>true if password was updated and succeeded</returns>
        public bool UpdatePassword(string userID, string oldPassword, string newPassword, string userType)
        {
            var result = false;
            if (userType == "member") // member update password
            {
                result = (1 == MemberAccessor.UpdateMemberPasswordHash(userID, HashSHA256(oldPassword), HashSHA256(newPassword)));
            }
            else if (userType == "trainer") // trainer update password
            {
                result = (1 == TrainerAccessor.UpdateTrainerPasswordHash(userID, HashSHA256(oldPassword), HashSHA256(newPassword)));
            }
            return result;
        }

       /// <summary>
       /// Ariel Sigo
        /// Created 2016/11/22
       /// </summary>
       /// <param name="oldMember"></param>
       /// <param name="newMember"></param>
       /// <returns>true if update succeeded</returns>
        public bool UpdateMember(Member oldMember, Member newMember)
        {
            var result = false;


            if (result = (1 == MemberAccessor.UpdateMember(oldMember.MemberID, newMember.MemberID, oldMember.FirstName, newMember.FirstName, oldMember.LastName, newMember.LastName, oldMember.PhoneNumber, newMember.PhoneNumber, oldMember.Status, newMember.Status, oldMember.Birthday, newMember.Birthday, oldMember.StartDate, newMember.StartDate, oldMember.MembershipTypeID, newMember.MembershipTypeID, oldMember.MemberPoleLevel, newMember.MemberPoleLevel, oldMember.MemberAcroLevel, newMember.MemberAcroLevel, oldMember.MemberSilksLevel, newMember.MemberSilksLevel, oldMember.MemberLyraLevel, newMember.MemberLyraLevel)))
            {
                return result;
            }

            else
            {
                result = false;
                return result;
            }

        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/22
        /// </summary>
        /// <param name="oldTrainer"></param>
        /// <param name="newTrainer"></param>
        /// <returns>true if update succeeded for trainer</returns>
        public bool UpdateTrainer(Trainer oldTrainer, Trainer newTrainer)
        {
            var result = false;

            if (result = (1 == TrainerAccessor.UpdateTrainer(oldTrainer.TrainerID, newTrainer.TrainerID, oldTrainer.TrainerFirstName, newTrainer.TrainerFirstName, oldTrainer.TrainerLastName, newTrainer.TrainerLastName, oldTrainer.TrainerPhoneNumber, newTrainer.TrainerPhoneNumber, oldTrainer.TrainerStatus, newTrainer.TrainerStatus, oldTrainer.TrainerPoleLevel, newTrainer.TrainerPoleLevel, oldTrainer.TrainerAcroLevel, newTrainer.TrainerAcroLevel, oldTrainer.TrainerSilksLevel, newTrainer.TrainerSilksLevel, oldTrainer.TrainerLyraLevel, newTrainer.TrainerLyraLevel)))
            {
                return result;
            }
            else
            {
                result = false;
                return result;
            }


        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/23
        /// </summary>
        /// <param name="oldMemberID"></param>
        /// <param name="oldFirstName"></param>
        /// <param name="oldLastName"></param>
        /// <param name="oldPhoneNumber"></param>
        /// <param name="oldStatus"></param>
        /// <param name="oldBirthday"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldMembershipTypeID"></param>
        /// <param name="oldMemberPoleLevel"></param>
        /// <param name="oldMemberAcroLevel"></param>
        /// <param name="oldMemberSilksLevel"></param>
        /// <param name="oldMemberLyraLevel"></param>
        /// <returns>a new instance of Member</returns>
        public Member makeMember(string oldMemberID, string oldFirstName, string oldLastName, string oldPhoneNumber, bool oldStatus, DateTime oldBirthday, DateTime oldStartDate, string oldMembershipTypeID, string oldMemberPoleLevel, string oldMemberAcroLevel, string oldMemberSilksLevel, string oldMemberLyraLevel)
        {
            Member mumber = new Member()
            {

                MemberID = oldMemberID,
                FirstName = oldFirstName,
                LastName = oldLastName,
                PhoneNumber = oldPhoneNumber,
                Status = oldStatus,
                Birthday = oldBirthday,
                StartDate = oldStartDate,
                MembershipTypeID = oldMembershipTypeID,
                MemberPoleLevel = oldMemberPoleLevel,
                MemberAcroLevel = oldMemberAcroLevel,
                MemberSilksLevel = oldMemberSilksLevel,
                MemberLyraLevel = oldMemberLyraLevel
            };
            return mumber;
        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/23
        /// 
        /// </summary>
        /// <param name="OldTrainerID"></param>
        /// <param name="OldTrainerFirstName"></param>
        /// <param name="OldTrainerLastName"></param>
        /// <param name="OldTrainerPhoneNumber"></param>
        /// <param name="OldTrainerStatus"></param>
        /// <param name="OldTrainerPoleLevel"></param>
        /// <param name="OldTrainerAcroLevel"></param>
        /// <param name="OldTrainerSilksLevel"></param>
        /// <param name="OldTrainerLyraLevel"></param>
        /// <returns>a new instance of Trainer</returns>
        public Trainer createTrainer(string OldTrainerID, string OldTrainerFirstName, string OldTrainerLastName, string OldTrainerPhoneNumber, bool OldTrainerStatus, string OldTrainerPoleLevel, string OldTrainerAcroLevel, string OldTrainerSilksLevel, string OldTrainerLyraLevel)
        {
            Trainer truner = new Trainer()
            {

                TrainerID = OldTrainerID,
                TrainerFirstName = OldTrainerFirstName,
                TrainerLastName = OldTrainerLastName,
                TrainerPhoneNumber = OldTrainerPhoneNumber,
                TrainerStatus = OldTrainerStatus,
                TrainerPoleLevel = OldTrainerPoleLevel,
                TrainerAcroLevel = OldTrainerAcroLevel,
                TrainerSilksLevel = OldTrainerSilksLevel,
                TrainerLyraLevel = OldTrainerLyraLevel
            };
            return truner;

        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/23
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
        /// <returns>a new member if succeeds</returns>
        public Member newMember(string MemberID, string FirstName, string LastName, string PhoneNumber, bool Status, DateTime Birthday, DateTime StartDate, string MembershipTypeID, string MemberPoleLevel, string MemberAcroLevel, string MemberSilksLevel, string MemberLyraLevel)
        {
            Member nember = new Member()
            {

                MemberID = MemberID,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Status = Status,
                Birthday = Birthday,
                StartDate = StartDate,
                MembershipTypeID = MembershipTypeID,
                MemberPoleLevel = MemberPoleLevel,
                MemberAcroLevel = MemberAcroLevel,
                MemberSilksLevel = MemberSilksLevel,
                MemberLyraLevel = MemberLyraLevel
            };
            return nember;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/23
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
        /// <returns>true if new member was added</returns>
        public bool createNewMember(string MemberID, string FirstName, string LastName, string PhoneNumber, bool Status, DateTime Birthday, DateTime StartDate, string MembershipTypeID, string MemberPoleLevel, string MemberAcroLevel, string MemberSilksLevel, string MemberLyraLevel)
        {
            bool result = false;
            try
            {
                result = (1 == MemberAccessor.InsertMember(MemberID, FirstName, LastName, PhoneNumber, Status, Birthday, StartDate, MembershipTypeID, MemberPoleLevel, MemberAcroLevel, MemberSilksLevel, MemberLyraLevel));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

    }
}



