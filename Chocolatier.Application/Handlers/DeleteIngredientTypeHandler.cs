using Chocolatier.Domain.Command.IngredientType;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers
{
    public class DeleteIngredientTypeHandler : IRequestHandler<DeleteIngredientTypeCommand, Response>
    {
        private readonly IIngredientTypeRepository IngredientTypeRepository;

        public DeleteIngredientTypeHandler(IIngredientTypeRepository ingredientTypeRepository)
        {
            IngredientTypeRepository = ingredientTypeRepository;
        }

        public async Task<Response> Handle(DeleteIngredientTypeCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var ingredientType = await IngredientTypeRepository.GetEntityById(request.Id, cancellationToken);

            if (ingredientType == null)
                return new Response(false, "Tipo de Ingrediente não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            ingredientType.IsActive = false;

            var resultEntity = IngredientTypeRepository.UpdateEntity(ingredientType, cancellationToken);

            if (resultEntity == null || resultEntity.IsActive != false)
                return new Response(false, "Falha ao deletar tipo de ingrediente, tente novamente ou entre em contato com o suporte", HttpStatusCode.InternalServerError);

            await IngredientTypeRepository.SaveChanges(cancellationToken);

            return new Response(true, HttpStatusCode.OK);
        }
    }
}
