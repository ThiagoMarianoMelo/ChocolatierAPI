using Chocolatier.Application.Queries;
using Chocolatier.Domain.Interfaces.Queries;

namespace Chocolatier.API.Configurations
{
    public static class QueriesConfiguration
    {
        public static void ConfigureQueries(this IServiceCollection services)
        {
            services.AddScoped<IEstablishmentQueries, EstablishmentQueries>();
        }
    }
}
