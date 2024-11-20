using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Model;

namespace UserService.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}