using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Domain.Entities
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }

        [ForeignKey("Users")]
        public int? UserLoginID { get; set; }

        public int Surname { get; set; }   
        public int FirstName { get; set; }
        public int OtherName { get; set; }
        public int Gender { get; set; }
        public int Email { get; set; }
        public int Phone { get; set; }

       public ICollection<Users> User { get; set; }    
    }
}
