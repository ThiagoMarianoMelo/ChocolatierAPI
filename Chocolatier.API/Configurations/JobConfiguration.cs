using Chocolatier.API.Consumer;
using Chocolatier.API.Jobs;
using Hangfire;
using Hangfire.PostgreSql;

namespace Chocolatier.API.Configurations
{
    public static class JobConfiguration
    {
        public static void ConfigureHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(hangFireOptions =>
            {
                hangFireOptions.UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(configuration.GetConnectionString("DataBase"));
                });
            });

            services.AddHangfireServer();

            services.AddScoped<CashClosingJob>();
        }

        public static void SetHangFireJobs(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            
            var serviceProvider = scope.ServiceProvider;

            var clashClosingInstace = (CashClosingJob)serviceProvider.GetRequiredService(typeof(CashClosingJob));

            RecurringJob.AddOrUpdate("CashCloseJob", () => clashClosingInstace.ExecuteClashClose(), "0 23 * * *");
        }
    }
}
