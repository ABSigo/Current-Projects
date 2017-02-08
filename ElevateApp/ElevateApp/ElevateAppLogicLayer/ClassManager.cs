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
