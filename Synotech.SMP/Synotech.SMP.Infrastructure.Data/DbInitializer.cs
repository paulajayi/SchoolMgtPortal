using Synotech.SMP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synotech.SMP.Util;
namespace Synotech.SMP.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if(context.Users.Any())
            {
                return;
            }

            var users = new Users[]
            {
                new Users{Email="admin@admin.com",Password=Encryptor.SHA512("admin"),IsActive=true}
            };

            foreach(var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
