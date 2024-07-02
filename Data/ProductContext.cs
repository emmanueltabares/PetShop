using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ProductCategory> Category { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<AnimalCategory> AnimalCategory { get; set; } = default!;
        public DbSet<Make> Make { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<Product>()
                .HasOne(p => p.AnimalCategory)
                .WithMany(ac => ac.Products)
                .HasForeignKey(p => p.AnimalCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Make)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.MakeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasOne<IdentityRole>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            // // Configuración para que al eliminar un usuario se eliminen sus relaciones con roles
            // modelBuilder.Entity<IdentityUserRole<string>>()
            //     .HasOne<IdentityUser>()
            //     .WithMany()
            //     .HasForeignKey(ur => ur.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);   

            base.OnModelCreating(modelBuilder);

        }
    }
}
