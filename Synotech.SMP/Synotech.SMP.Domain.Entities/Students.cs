using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Domain.Entities
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }

        //[ForeignKey("Users")]
        public int UserLoginID { get; set; }


        public int ParentID { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string Gender { get; set; }


        //public virtual Users LoginAccess { get; set; }

    }
}
