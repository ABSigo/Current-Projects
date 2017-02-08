using ElevateAppDataAccess;
using ElevateAppDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateAppLogicLayer
{
    public class MemberManager
    {
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/23
        /// 
        /// </summary>
        private List<Member> _members = null;
       
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/23
        /// 
        /// Retrieves list of active members
        /// </summary>
        public List<Member> ActiveMember
        {
            get
            {
                if (this._members == null)
                {
                    refreshActiveMemberList();
                }
                return _members;
            }
            private set
            {
                _members = value;
            }
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/23
        /// 
        /// Retrieves Member list
        /// </summary>
        private void refreshActiveMemberList()
        {
            try
            {
                _members = MemberAccessor.RetrieveMembers();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Ariel Sigo  
        ///  
        /// Created 2016/11/19
        /// </summary>
        /// <returns>full Member list if succesful</returns>
        public List<Member> GetFullMemberList()
        {
            try
            {
                return MemberAccessor.RetrieveMembers();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Count when status is true</returns>
        public int GetMemberCount(bool status = true)
        {
            try
            {
                return MemberAccessor.RetrieveMemberCount();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Ariel Sigo
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
        /// <returns>true if update was successful</returns>
        public bool UpdateMember(string oldMemberID, string newMemberID, string oldFirstName, string newFirstName, string oldLastName, string newLastName, string oldPhoneNumber, string newPhoneNumber, bool oldStatus, bool newStatus, DateTime oldBirthday, DateTime newBirthday, DateTime oldStartDate, DateTime newStartDate, string oldMembershipTypeID, string newMembershipTypeID, string oldMemberPoleLevel, string newMemberPoleLevel, string oldMemberAcroLevel, string newMemberAcroLevel, string oldMemberSilksLevel, string newMemberSilksLevel, string oldMemberLyraLevel, string newMemberLyraLevel)
        {
            var result = false;
            try
            {
                result = (1 == MemberAccessor.UpdateMember(oldMemberID, newMemberID, oldFirstName, newFirstName, oldLastName, newLastName, oldPhoneNumber, newPhoneNumber, oldStatus, newStatus, oldBirthday, newBirthday, oldStartDate, newStartDate, oldMembershipTypeID, newMembershipTypeID, oldMemberPoleLevel, newMemberPoleLevel, oldMemberAcroLevel, newMemberAcroLevel, oldMemberSilksLevel, newMemberSilksLevel, oldMemberLyraLevel, newMemberLyraLevel));
                refreshActiveMemberList();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
           
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="password"></param>
        /// <returns>True if verification for member was successful</returns>
        public static bool VerifyMember(string memberID, string password)
        {
            bool isTrainer = false;
            var result = false;
            if (1 == UserAccessor.VerifyEmailAndPassword(memberID, UserManager.HashSHA256(password), isTrainer))
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// 
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns>Member if succeeds</returns>
        public static Member getMember(string MemberID)
        {
            Member member = null;
            try
            {
                member = MemberAccessor.getMember(MemberID);

            }
            catch (Exception)
            {

                throw;
            }
            return member;

        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>true if members password was updated successfully</returns>
        public bool UpdateMemberPassword(string memberId, string oldPassword, string newPassword)
        {
            var result = false;
            try
            {
                if (1 == MemberAccessor.UpdateMemberPasswordHash(memberId, UserManager.HashSHA256(oldPassword), UserManager.HashSHA256(newPassword)))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could Not Find Stored Procedure Update Member Password", ex);
            }
            return result;
        }
    }

}

