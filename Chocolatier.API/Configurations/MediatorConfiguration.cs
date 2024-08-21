using Chocolatier.Application.Handlers;
using Chocolatier.Domain.Command.Auth;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Command.IngredientType;
using Chocolatier.Domain.Responses;
using MediatR;

namespace Chocolatier.API.Configurations
{
    public static class MediatorConfiguration
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateEstablishmentCommand, Response>, CreateEstablishmentHandler>();
            services.AddScoped<IRequestHandler<UpdateEstablishmentCommand, Response>, UpdateEstablishmentHandler>();
            services.AddScoped<IRequestHandler<DeleteEstablishmentCommand, Response>, DeleteEstablishmentHandler>();

            services.AddScoped<IRequestHandler<CreateIngredientTypeCommand, Response>, CreateIngredientTypeHandler>();
            services.AddScoped<IRequestHandler<DeleteIngredientTypeCommand, Response>, DeleteIngredientTypeHandler>();

            services.AddScoped<IRequestHandler<CreateIngredientCommand, Response>, CreateIngredientHandler>();
            services.AddScoped<IRequestHandler<DeleteIngredientCommand, Response>, DeleteIngredientHandler>();

            services.AddScoped<IRequestHandler<LoginCommand, Response>, LoginHandler>();

            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR();

        }
    }
}
