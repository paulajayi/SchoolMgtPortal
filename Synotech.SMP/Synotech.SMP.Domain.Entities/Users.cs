using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Domain.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
       

        public ICollection<UsersInRoles> UsersInRoles { get; set; }
    }
}
