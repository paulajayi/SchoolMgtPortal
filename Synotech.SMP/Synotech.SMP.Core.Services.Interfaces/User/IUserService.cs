using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Core.Services.Interfaces.User
{
    public interface IUserService
    {
        bool AuthenticateUser(string username, string password);
    }
}
