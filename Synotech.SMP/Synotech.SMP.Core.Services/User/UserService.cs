using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Synotech.SMP.Core.Interfaces.User;
using Synotech.SMP.Core.Models.User;

namespace Synotech.SMP.Core.Services.User
{
   

    public class UserService : IUserService
    {
        
        public async Task<UserLoginResponse> AuthenticateUser(string username, string password)
        {

            throw new NotImplementedException();
        }
    }
}
