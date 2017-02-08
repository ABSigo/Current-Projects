using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateAppDataObjects;
using ElevateAppDataAccess;


namespace ElevateAppLogicLayer
{
    public class TrainerManager
    {
        private List<Trainer> _trainers = null;
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        public List<Trainer> ActiveTrainer
        {
            get
            {
                if (this._trainers == null)
                {
                    refreshActiveTrainerList();
                }
                return _trainers;
            }
            private set
            {
                _trainers = value;
            }
        }
        private void refreshActiveTrainerList()
        {
            try
            {
                _trainers = TrainerAccessor.RetrieveTrainers();
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
        /// <returns>List of trainers</returns>
        public List<Trainer> GetFullTrainerList()
        {
            try
            {
                return TrainerAccessor.RetrieveTrainers();
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
        /// <param name="active"></param>
        /// <returns>count of active trainers</returns>
        public int GetTrainerCount(bool active = true)
        {
            try
            {
                return TrainerAccessor.RetrieveTrainerCount();
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
        /// <param name="trainerID"></param>
        /// <param name="newTrainerID"></param>
        /// <returns>true if update was successful</returns>
        public bool UpdateTrainerEmail(string trainerID, string newTrainerID)
        {
            var result = false;
            try
            {
                result = (1 == TrainerAccessor.UpdateTrainerEmail(trainerID, newTrainerID));
                refreshActiveTrainerList();
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
        /// <param name="trainerID"></param>
        /// <param name="password"></param>
        /// <returns>true if Trainer is verified and able to log in </returns>
        public static bool VerifyTrainer(string trainerID, string password)
        {
            var result = false;
            if (1 == UserAccessor.VerifyEmailAndPassword(trainerID, UserManager.HashSHA256(password)))
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        /// <param name="trainerID"></param>
        /// <returns>Trainer from trainerId</returns>
        public static Trainer getTrainer(string trainerID)
        {
            Trainer trainer = null;
            try
            {
                trainer = TrainerAccessor.getTrainer(trainerID);
            }
            catch (Exception)
            {

                throw;
            }
            return trainer;

        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        /// <param name="trainerId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>True if update on password was successful</returns>
        public bool UpdateTrainerPassword(string trainerId, string oldPassword, string newPassword)
        {
            var result = false;
            try
            {
                if (1 == TrainerAccessor.UpdateTrainerPasswordHash(trainerId, UserManager.HashSHA256(oldPassword), UserManager.HashSHA256(newPassword)))
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

                throw new ApplicationException("Could Not Find Stored Procedure 'Update Trainer Password'", ex);
            }
            return result;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        /// <param name="oldTrainerID"></param>
        /// <param name="newTrainerID"></param>
        /// <param name="oldTrainerFirstName"></param>
        /// <param name="newTrainerFirstName"></param>
        /// <param name="oldTrainerLastName"></param>
        /// <param name="newTrainerLastName"></param>
        /// <param name="oldTrainerPhoneNumber"></param>
        /// <param name="newTrainerPhoneNumber"></param>
        /// <param name="oldTrainerStatus"></param>
        /// <param name="newTrainerStatus"></param>
        /// <param name="oldTrainerPoleLevel"></param>
        /// <param name="newTrainerPoleLevel"></param>
        /// <param name="oldTrainerAcroLevel"></param>
        /// <param name="newTrainerAcroLevel"></param>
        /// <param name="oldTrainerSilksLevel"></param>
        /// <param name="newTrainerSilksLevel"></param>
        /// <param name="oldTrainerLyraLevel"></param>
        /// <param name="newTrainerLyraLevel"></param>
        /// <returns>True if update on trainer was successsful </returns>
        public bool UpdateTrainer(string oldTrainerID, string newTrainerID, string oldTrainerFirstName, string newTrainerFirstName, string oldTrainerLastName, string newTrainerLastName, string oldTrainerPhoneNumber, string newTrainerPhoneNumber, bool oldTrainerStatus, bool newTrainerStatus, string oldTrainerPoleLevel, string newTrainerPoleLevel, string oldTrainerAcroLevel, string newTrainerAcroLevel, string oldTrainerSilksLevel, string newTrainerSilksLevel, string oldTrainerLyraLevel, string newTrainerLyraLevel)
        {
            var result = false;
            try
            {
                result = (1 == TrainerAccessor.UpdateTrainer(oldTrainerID, newTrainerID, oldTrainerFirstName, newTrainerFirstName, oldTrainerLastName, newTrainerLastName, oldTrainerPhoneNumber, newTrainerPhoneNumber, oldTrainerStatus, newTrainerStatus, oldTrainerPoleLevel, newTrainerPoleLevel, oldTrainerAcroLevel, newTrainerAcroLevel, oldTrainerSilksLevel, newTrainerSilksLevel, oldTrainerLyraLevel, newTrainerLyraLevel));
                refreshActiveTrainerList();
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
        }
    }
}