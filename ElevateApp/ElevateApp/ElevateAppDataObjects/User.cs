using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateAppDataObjects
{
    public class User : Member
    {
        public List<Role> Roles { get; set; }
    }
}
