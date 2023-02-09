using System;
using System.Collections.Generic;
using System.Text;

namespace Synotech.SMP.Core.Interfaces.User
{
    public interface IUserService
    {
        bool AuthenticateUser(string username, string password);
    }
}
