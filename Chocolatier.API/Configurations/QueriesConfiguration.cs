using Chocolatier.Application.Queries;
using Chocolatier.Domain.Interfaces.Queries;

namespace Chocolatier.API.Configurations
{
    public static class QueriesConfiguration
    {
        public static void ConfigureQueries(this IServiceCollection services)
        {
            services.AddScoped<IEstablishmentQueries, EstablishmentQueries>();
            services.AddScoped<IIngredientTypeQueries, IngredientTypeQueries>();
            services.AddScoped<IIngredientQueries, IngredientQueries>();
            services.AddScoped<IRecipeQueries, RecipeQueries>();
            services.AddScoped<IProductQueries, ProductQueries>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<ICustomerQueries, CustomerQueries>();
            services.AddScoped<ISalesQueries, SalesQueries>();
        }
    }
}
