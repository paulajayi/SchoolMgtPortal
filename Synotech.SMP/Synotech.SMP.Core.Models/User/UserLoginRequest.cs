using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
