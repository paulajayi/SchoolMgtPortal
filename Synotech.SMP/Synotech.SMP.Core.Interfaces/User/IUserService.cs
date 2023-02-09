using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Synotech.SMP.Core.Models.User;

namespace Synotech.SMP.Core.Interfaces.User
{
    public interface IUserService
    {
        Task<UserLoginResponse> AuthenticateUser(string username, string password);
    }
}
