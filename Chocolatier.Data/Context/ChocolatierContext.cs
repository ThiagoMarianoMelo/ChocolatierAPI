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
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleItem> SaleItem { get; set; }

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

            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().Property(o => o.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().HasOne(ri => ri.Establishment).WithMany().HasForeignKey(i => i.RequestedById);

            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
            modelBuilder.Entity<OrderItem>().Property(oi => oi.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Recipe).WithMany().HasForeignKey(oi => oi.RecipeId);
            modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Order).WithMany().HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderHistory>().HasKey(oh => oh.Id);
            modelBuilder.Entity<OrderHistory>().Property(oh => oh.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderHistory>().HasOne(oh => oh.Order).WithMany().HasForeignKey(oh => oh.OrderId);

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sale>().HasKey(s => s.Id);
            modelBuilder.Entity<Sale>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Sale>().HasOne(s => s.Establishment).WithMany().HasForeignKey(i => i.EstablishmentId);
            modelBuilder.Entity<Sale>().HasOne(s => s.Customer).WithMany().HasForeignKey(i => i.CustomerId);

            modelBuilder.Entity<SaleItem>().HasKey(si => si.Id);
            modelBuilder.Entity<SaleItem>().Property(si => si.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SaleItem>().HasOne(si => si.Product).WithMany().HasForeignKey(oi => oi.ProductId);
            modelBuilder.Entity<SaleItem>().HasOne(si => si.Sale).WithMany().HasForeignKey(oi => oi.SaleId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
