using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.IngrdientHandlers
{
    public class DeleteIngredientHandler : IRequestHandler<DeleteIngredientCommand, Response>
    {
        private readonly IIngredientRepository IngredientRepository;

        public DeleteIngredientHandler(IIngredientRepository ingredientRepository)
        {
            IngredientRepository = ingredientRepository;
        }

        public async Task<Response> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var ingredient = await IngredientRepository.GetEntityById(request.Id, cancellationToken);

            if (ingredient == null)
                return new Response(false, "Ingrediente não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            IngredientRepository.DeleteEntity(ingredient, cancellationToken);

            var result = await IngredientRepository.SaveChanges(cancellationToken);

            if (result <= 0)
                return new Response(false, "Ingrediente não deletado tente novamente ou entre em contato com o suporte.", HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK);
        }
    }
}
