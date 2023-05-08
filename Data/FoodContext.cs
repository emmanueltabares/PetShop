using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class FoodContext : DbContext
    {
        public FoodContext (DbContextOptions<FoodContext> options)
            : base(options)
        {
        }

        public DbSet<PetShop.Models.Food> Food { get; set; } = default!;
    }
}
