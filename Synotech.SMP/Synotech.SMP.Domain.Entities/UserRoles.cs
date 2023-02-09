using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Domain.Entities
{
    public class UserRoles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

       public ICollection<UsersInRoles> UsersInRoles { get; set; }
    }
}
