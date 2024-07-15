using Chocolatier.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Context
{
    public class ChocolatierContext : IdentityDbContext<Establishment>
    {
        public ChocolatierContext(DbContextOptions options) : base(options) { }

        public DbSet<IngredientType> IngredientTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientType>().HasKey(x => x.Id);
            modelBuilder.Entity<IngredientType>().Property(it => it.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }   
    }
}
