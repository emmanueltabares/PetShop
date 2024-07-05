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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ProductDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            // Configuración de restricción para evitar eliminación de productos con órdenes asociadas
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de restricción para evitar eliminación de órdenes con detalles asociados
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

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

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
