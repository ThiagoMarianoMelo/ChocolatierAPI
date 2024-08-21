using Chocolatier.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Context
{
    public class ChocolatierContext : IdentityDbContext<Establishment>
    {
        public ChocolatierContext(DbContextOptions options) : base(options) { }

        public DbSet<IngredientType> IngredientType { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientType>().HasKey(it => it.Id);
            modelBuilder.Entity<IngredientType>().Property(it => it.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            modelBuilder.Entity<Ingredient>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Ingredient>().HasOne(i => i.IngredientType).WithMany().HasForeignKey(i => i.IngredientTypeId);


            base.OnModelCreating(modelBuilder);
        }   
    }
}
