using Chocolatier.API.Configurations;
using Chocolatier.API.Profiles;
using Microsoft.OpenApi.Models;
using Hangfire;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureDataBase(builder.Configuration);

        builder.Services.ConfigureMediator();
        builder.Services.ConfigureServices();
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureQueries();
        builder.Services.ConfigureQueues(builder.Configuration);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAutoMapper([typeof(RequestToDomainProfile), typeof(DomainToResponseProfile)]);
        builder.Services.AddIdentityConfiguration(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Chocolatier API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
            });
        });

        builder.Services.ConfigureHangFire(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.SyncMigrations();

        app.UseHangfireDashboard("/hangfire");

        JobConfiguration.SetHangFireJobs(app.Services.GetRequiredService<IServiceScopeFactory>());

        QueuesConfiguration.StartQueuesConsumer(app.Services.GetRequiredService<IServiceScopeFactory>());

        app.Run();
    }
}