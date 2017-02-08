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
        public static bool VerifyTrainer(string trainerID, string password)
        {
            var result = false;
            if (1 == UserAccessor.VerifyEmailAndPassword(trainerID, UserManager.HashSHA256(password)))
            {
                result = true;
            }
            return result;
        }
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
        public bool UpdateTrainerPassword(string trainerID, string oldPassword, string newPassword)
        {
            var result = false;
            try
            {
                if (1 == UserAccessor.UpdateTrainerPasswordHash(trainerID, HashSHA256(oldPassword), HashSHA256(newPassword)))
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