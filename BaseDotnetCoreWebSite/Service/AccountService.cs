using BaseDotnetCoreWebSite.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BaseDotnetCoreWebSite.Service
{
    public class AccountService
    {

        public ClaimsIdentity GetClaimsIdentity(AccountModel accountModel)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, accountModel.UID),
                new Claim("UserName", accountModel.UserName),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }
    }
}
