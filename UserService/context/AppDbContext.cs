using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UserService.context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        
    }
}