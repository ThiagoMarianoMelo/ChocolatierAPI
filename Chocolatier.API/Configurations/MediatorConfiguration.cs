using Chocolatier.Application.Handlers;
using Chocolatier.Domain.Command;
using Chocolatier.Domain.Responses;
using MediatR;

namespace Chocolatier.API.Configurations
{
    public static class MediatorConfiguration
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateEstablishmentCommand, Response>, CreateEstablishmentHandler>();

            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR();

        }
    }
}
