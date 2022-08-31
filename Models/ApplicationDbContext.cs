using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookifyNew.ViewModel;
using BookifyNew.Models;
using Microsoft.AspNetCore.Identity;

namespace BookifyNew.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
            
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<AppUser>(entity =>
        //    {
        //        entity.ToTable(name: "User");
        //    });
        //}

    }
}
