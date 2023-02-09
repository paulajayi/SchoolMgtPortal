﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Domain.Entities
{
    public class UsersInRoles
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Users User { get; set; }
        public UserRoles Role { get; set; }

    }
}
