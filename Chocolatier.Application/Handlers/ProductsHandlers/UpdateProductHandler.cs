using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Command.Product;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.ProductsHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly IRecipeRepository RecipeRepository;
        private readonly IProductRepository ProductRepository;
        private readonly IAuthEstablishment AuthEstablishment;

        public UpdateProductHandler(IRecipeRepository recipeRepository, IProductRepository productRepository, IAuthEstablishment authEstablishment)
        {
            RecipeRepository = recipeRepository;
            ProductRepository = productRepository;
            AuthEstablishment = authEstablishment;
        }

        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            if (request.RecipeId != Guid.Empty)
            {
                var recipeIsActive = await RecipeRepository.IsActiveById(request.RecipeId, cancellationToken);

                if (!recipeIsActive)
                    return new Response(true, ["Receita escolhida não é valida."], HttpStatusCode.BadRequest);
            }

            var product = await ProductRepository.GetEntityById(request.Id, cancellationToken);

            if (product is null)
                return new Response(false, "Produto não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            if(product.CurrentEstablishmentId != AuthEstablishment.Id)
                return new Response(false, "Você não possui permissão para modificar o produto, ele não está em seu estabelecimento.", HttpStatusCode.BadRequest);

            var dataChanged = await ChangeDataFromProduct(product, request, cancellationToken);

            if (!dataChanged)
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK);
        }
        private async Task<bool> ChangeDataFromProduct(Product product, UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
                product.Name = request.Name;

            if (request.Price > 0)
                product.Price = request.Price;

            if (request.RecipeId != Guid.Empty)
                product.RecipeId = request.RecipeId;

            if (request.ExpireAt != DateTime.MinValue)
                product.ExpireAt = request.ExpireAt.ToUniversalTime();

            ProductRepository.UpdateEntity(product, cancellationToken);

            var result = await ProductRepository.SaveChanges(cancellationToken);

            return result > 0;
        }
    }
}
