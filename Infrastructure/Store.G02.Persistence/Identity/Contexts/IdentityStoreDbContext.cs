using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Identity.Contexts
{
    //CLR To Make object And Allow DI For him
    public class IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //IdentityDbContext : make U Inherit 7 DbSets

            //Make the Tables With those Names
            builder.Entity<AppUser>().ToTable("Users");// 1
            builder.Entity<IdentityRole>().ToTable("Roles");// 2
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");// 3
            builder.Entity<Address>().ToTable("Addresses");

            //Ignore the other 4

            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();






        }
    }
}
