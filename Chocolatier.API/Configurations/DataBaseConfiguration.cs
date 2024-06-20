using Chocolatier.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.API.Configurations
{
    public static class DataBaseConfiguration
    {
        public static void ConfigureDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChocolatierContext>(options => {

                options.UseLazyLoadingProxies().UseNpgsql(connectionString: configuration.GetConnectionString("DataBase"));

            });
        }

        public static void SyncMigrations(this WebApplication application)
        {
            //using var scope = application.Services.CreateScope();
            
            //var context = scope.ServiceProvider.GetRequiredService<ChocolatierContext>();

            //context.Database.Migrate();
        }
    }
}
