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
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeItem> RecipeItem { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientType>().HasKey(it => it.Id);
            modelBuilder.Entity<IngredientType>().Property(it => it.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            modelBuilder.Entity<Ingredient>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Ingredient>().HasOne(i => i.IngredientType).WithMany().HasForeignKey(i => i.IngredientTypeId);

            modelBuilder.Entity<Recipe>().HasKey(r => r.Id);
            modelBuilder.Entity<Recipe>().Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<RecipeItem>().HasKey(ri => ri.Id);
            modelBuilder.Entity<RecipeItem>().Property(ri => ri.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RecipeItem>().HasOne(ri => ri.IngredientType).WithMany().HasForeignKey(i => i.IngredientTypeId);
            modelBuilder.Entity<RecipeItem>().HasOne(i => i.Recipe).WithMany().HasForeignKey(i => i.RecipeId);

            modelBuilder.Entity<Product>().HasKey(ri => ri.Id);
            modelBuilder.Entity<Product>().Property(ri => ri.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().HasOne(ri => ri.Establishment).WithMany().HasForeignKey(i => i.CurrentEstablishmentId);
            modelBuilder.Entity<Product>().HasOne(i => i.Recipe).WithMany().HasForeignKey(i => i.RecipeId);

            base.OnModelCreating(modelBuilder);
        }   
    }
}
