using System;
using System.Collections.Generic;
using System.Text;

namespace Synotech.SMP.Core.Models.Parent
{
    public class ParentModel
    {
        public int ParentID { get; set; }        
        public int? UserLoginID { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
