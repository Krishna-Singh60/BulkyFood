using Bookie.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Bookie.DataAcess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Category>  Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add it for Identity will check it later
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1001,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Good",
                    ISBN = "SWD9999901",
                    ListPrice = 100,
                    Price = 80,
                    CategoryId = 5,
                    ImageUrl = "",

                },
                new Product
                {
                    Id = 1002,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Present",
                    ISBN = "CAw777701",
                    ListPrice = 80,
                    Price = 50,
                    CategoryId = 2,
                    ImageUrl = "",

                }
                );
        }
    }
}
