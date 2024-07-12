using Chocolatier.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Context
{
    public class ChocolatierContext : IdentityDbContext<Establishment>
    {
        public ChocolatierContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        }   
    }
}
