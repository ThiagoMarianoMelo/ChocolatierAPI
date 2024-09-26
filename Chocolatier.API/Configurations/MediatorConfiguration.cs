using Chocolatier.Application.Handlers.AuthHandlers;
using Chocolatier.Application.Handlers.EstablishmentHandlers;
using Chocolatier.Application.Handlers.IngrdientHandlers;
using Chocolatier.Application.Handlers.IngredientTypeHandlers;
using Chocolatier.Application.Handlers.OrdersHandlers;
using Chocolatier.Application.Handlers.ProductsHandlers;
using Chocolatier.Application.Handlers.RecipeHandlers;
using Chocolatier.Domain.Command.Auth;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Command.IngredientType;
using Chocolatier.Domain.Command.Order;
using Chocolatier.Domain.Command.Product;
using Chocolatier.Domain.Command.Recipe;
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
            services.AddScoped<IRequestHandler<DisableEstablishmentCommand, Response>, DisableEstablishmentHandler>();
            services.AddScoped<IRequestHandler<EnableEstablishmentCommand, Response>, EnableEstablishmentHandler>();

            services.AddScoped<IRequestHandler<CreateIngredientTypeCommand, Response>, CreateIngredientTypeHandler>();
            services.AddScoped<IRequestHandler<DeleteIngredientTypeCommand, Response>, DeleteIngredientTypeHandler>();

            services.AddScoped<IRequestHandler<CreateIngredientCommand, Response>, CreateIngredientHandler>();
            services.AddScoped<IRequestHandler<DeleteIngredientCommand, Response>, DeleteIngredientHandler>();
            services.AddScoped<IRequestHandler<UpdateIngredientCommand, Response>, UpdateIngredientHandler>();

            services.AddScoped<IRequestHandler<CreateRecipeCommand, Response>, CreateRecipeHandler>();
            services.AddScoped<IRequestHandler<UpdateRecipeCommand, Response>, UpdateRecipeHandler>();
            services.AddScoped<IRequestHandler<DeleteRecipeCommand, Response>, DeleteRecipeHandler>();
            services.AddScoped<IRequestHandler<MakeRecipeCommand, Response>, MakeRecipeHandler>();

            services.AddScoped<IRequestHandler<CreateProductCommand, Response>, CreateProductHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, Response>, UpdateProductHandler>();
            services.AddScoped<IRequestHandler<DeleteProductCommand, Response>, DeleteProductHandler>();

            services.AddScoped<IRequestHandler<CreateOrderCommand, Response>, CreateOrderHandler>();
            services.AddScoped<IRequestHandler<ChangeOrderStatusCommand, Response>, ChangeOrderStatusHandler>();

            services.AddScoped<IRequestHandler<LoginCommand, Response>, LoginHandler>();

            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR();

        }
    }
}
