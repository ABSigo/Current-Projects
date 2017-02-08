using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateAppDataObjects
{
   public class Member
    {
       public string MemberID { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string PhoneNumber { get; set; }
       public bool Status { get; set; }
       public DateTime Birthday { get; set; }
       public DateTime StartDate { get; set; }
       public string MembershipTypeID { get; set; }
       public string MemberPoleLevel { get; set; }
       public string MemberAcroLevel { get; set; }
       public string MemberSilksLevel { get; set; }
       public string MemberLyraLevel { get; set; }
   }
}
