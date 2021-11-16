using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model.Data.EfCore
{
    public class SocialContext:IdentityDbContext<User,Role,int>
    {   
        public SocialContext(DbContextOptions<SocialContext> options):base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }


    }
}
