using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext (DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<PetShop.Models.Product> Product { get; set; } = default!;

        public DbSet<Category> Category { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasMany(p => p.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        }
    }
}
