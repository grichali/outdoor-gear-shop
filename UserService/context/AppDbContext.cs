using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions){

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> Roles = new List<IdentityRole> { 
                new IdentityRole(){
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole(){
                    Name="User",
                    NormalizedName="USER"
                },
             };
            builder.Entity<IdentityRole>().HasData(Roles);
        }
    }
}