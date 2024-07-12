using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Data.Repositories;

namespace Chocolatier.API.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        }
    }
}
