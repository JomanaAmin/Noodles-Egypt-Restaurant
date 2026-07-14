using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entities;

namespace NoodlesEgypt.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Define your DbSets (tables) here
        //public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminRoleId = "3A3B4F5E-8F61-49D3-9A57-7F74A1B1C001";
            var admin = new IdentityRole {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
            };
            builder.Entity<IdentityRole>().HasData(admin);

            builder.Entity<ProductVariant>().Property(v => v.VariantPrice).HasPrecision(6,2);
        }
    }
}
