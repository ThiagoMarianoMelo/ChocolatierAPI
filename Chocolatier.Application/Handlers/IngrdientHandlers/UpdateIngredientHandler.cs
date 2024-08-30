using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.IngrdientHandlers
{
    public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand, Response>
    {
        private readonly IIngredientRepository IngredienteRepository;

        public UpdateIngredientHandler(IIngredientRepository ingredienteRepository)
        {
            IngredienteRepository = ingredienteRepository;
        }

        public async Task<Response> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var ingredient = await IngredienteRepository.GetEntityById(request.Id, cancellationToken);

            if (ingredient is null)
                return new Response(false, "Ingrediente não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            var dataChanged = await ChangeDataFromIngredient(ingredient, request, cancellationToken);

            if (!dataChanged)
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK);
        }

        private async Task<bool> ChangeDataFromIngredient(Ingredient ingredient, UpdateIngredientCommand request, CancellationToken cancellationToken)
        {

            if (request.IngredientTypeId != Guid.Empty)
                ingredient.IngredientTypeId = request.IngredientTypeId;

            if (request.Amount > 0)
                ingredient.Amount = request.Amount;

            if (request.ExpireAt != DateTime.MinValue)
                ingredient.ExpireAt = request.ExpireAt.ToUniversalTime();

            IngredienteRepository.UpdateEntity(ingredient, cancellationToken);

            var result = await IngredienteRepository.SaveChanges(cancellationToken);

            return result > 0;
        }
    }
}
