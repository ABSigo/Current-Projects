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
        private List<Member> _members = null;

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
        public bool UpdateMemberPassword(string memberID, string oldPassword, string newPassword)
        {
            var result = false;
            try
            {
                if (1 == UserAccessor.UpdateMemberPasswordHash(memberID, HashSHA256(oldPassword), HashSHA256(newPassword)))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;



        }
    }
}

