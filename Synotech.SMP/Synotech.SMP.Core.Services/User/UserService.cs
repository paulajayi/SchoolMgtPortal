using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Synotech.SMP.Core.Interfaces.User;
using Synotech.SMP.Core.Models.User;
using Synotech.SMP.Infrastructure.Data;
using Synotech.SMP.Util;
namespace Synotech.SMP.Core.Services.User
{


    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserLoginResponse> AuthenticateUser(string username, string password)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == username && x.Password == Encryptor.SHA512(password));
          
            if (result != null)
            {
                var retVal = new UserLoginResponse
                {
                    Email = result.Email,
                    Password = result.Password,
                    IsActive = result.IsActive,
                };

                return retVal;
            }

            return null;

            

           
        }
    }
}
