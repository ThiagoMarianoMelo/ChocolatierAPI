using Chocolatier.Application.Handlers;
using Chocolatier.Domain.Command.Auth;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Responses;
using MediatR;

namespace Chocolatier.API.Configurations
{
    public static class MediatorConfiguration
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateEstablishmentCommand, Response>, CreateEstablishmentHandler>();
            services.AddScoped<IRequestHandler<LoginCommand, Response>, LoginHandler>();

            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR();

        }
    }
}
