using Synotech.SMP.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Synotech.SMP.Core.Models.User;

namespace Synotech.SMP.Web.Classes
{
   
    public class IdentityManager
    {
        public const string AppTokenCookie = "AppTokenCookie";
        private const string AppUserIDCookie = "AppUserIDCookie";


        private readonly HttpContext _context;

        public IdentityManager(HttpContext context)
        {
            _context = context;
        }

        public async Task SigninUser(UserLoginResponse user)
        {
            var userIdentityClaims = new ClaimsIdentity("Identity");
            userIdentityClaims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            userIdentityClaims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            userIdentityClaims.AddClaim(new Claim(ClaimTypes.Role, "User"));
            userIdentityClaims.Label = "Identity";
            var claimsPrincipal = new ClaimsPrincipal(userIdentityClaims);
            _context.User = claimsPrincipal;

            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);


        }




        public async Task SignOutUser()
        {
            await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void RemoveCookieAuth()
        {
            _context.Response.Cookies.Delete(AppTokenCookie);
            _context.Response.Cookies.Delete(AppUserIDCookie);
        }

        public void StoreCookieAuth(string token, int userId)
        {
            _context.Response.Cookies.Append(AppTokenCookie, token);
            _context.Response.Cookies.Append(AppUserIDCookie, userId.ToString());
        }

        public (string, string) LoadCookieAuth()
        {
            var token = _context.Request.Cookies[AppTokenCookie] ?? null;
            var userId = _context.Request.Cookies[AppUserIDCookie] ?? null;

            return (token, userId);
        }

        public bool CheckAuhentication()
        {
            return _context.User.Identity.IsAuthenticated;
        }




    }
}
