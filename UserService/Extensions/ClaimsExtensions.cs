using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserService.Model;
using Microsoft.AspNetCore.Identity;

namespace UserService.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
        }


        public static async Task<bool> IfEmailExists(this string email, UserManager<AppUser> userManager){
            var user = await userManager.FindByEmailAsync(email);
            return user != null ; 
        }
    }
}