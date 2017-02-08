using ElevateAppDataAccess;
using ElevateAppDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateAppLogicLayer
{
   public class ClassManager
    {
        private List<Class> _classes = null;
       /// <summary>
       /// Ariel Sigo
        /// Created 2016/11/19
       /// </summary>
        public List<Class> ActiveClass
        {
            get
            {
                if (this._classes == null)
                {
                    refreshActiveClassList();
                }
                return _classes;
            }
            private set
            {
                _classes = value;
            }

        }
       /// <summary>
       /// Ariel Sigo
       /// Created 2016/11/19
       /// 
       /// Refresh Class list of active classes
       /// </summary>
        private void refreshActiveClassList()
        {
            try
            {
                _classes = ClassAccessor.RetrieveClass();
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
       /// <returns>Full list of classes if succesful</returns>
        public List<Class> GetFullClassList()
        {
            try
            {
                return ClassAccessor.RetrieveClass();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
