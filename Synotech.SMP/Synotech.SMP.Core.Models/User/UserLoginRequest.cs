using System;
using System.Collections.Generic;
using System.Text;

namespace Synotech.SMP.Core.Models.User
{
    public class UserLoginRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }


        /// <summary>
        /// Password
        /// </summary>        
        public string Password { get; set; }
    }
}
