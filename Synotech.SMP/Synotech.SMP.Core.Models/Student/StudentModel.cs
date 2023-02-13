using System;
using System.Collections.Generic;
using System.Text;

namespace Synotech.SMP.Core.Models.Student
{
    public class StudentModel
    {
        public int StudentId { get; set; }      
        public int UserLoginID { get; set; }
        public int ParentID { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string Gender { get; set; }
    }
}
