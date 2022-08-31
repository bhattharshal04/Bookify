using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BookifyNew.Models;
using BookifyNew.ViewModel;
using System.Linq;

#nullable disable

namespace BookifyNew.Models
{
    public partial class BFDBContext : DbContext
    {
        public BFDBContext()
        {
        }

        public BFDBContext(DbContextOptions<BFDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BFDBFinal;Trusted_Connection=True;");
            }
            //optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
