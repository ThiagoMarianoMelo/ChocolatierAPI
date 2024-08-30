using Chocolatier.Domain.Command.Recipe;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.RecipeHandlers
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, Response>
    {
        private readonly IRecipeRepository RecipeRepository;

        public DeleteRecipeHandler(IRecipeRepository recipeRepository)
        {
            RecipeRepository = recipeRepository;
        }

        public async Task<Response> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var recipe = await RecipeRepository.GetEntityById(request.Id, cancellationToken);

            if (recipe == null)
                return new Response(false, "Receita não encontrada tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            recipe.IsActive = false;

            var resultEntity = RecipeRepository.UpdateEntity(recipe, cancellationToken);

            if (resultEntity == null || resultEntity.IsActive != false)
                return new Response(false, "Falha ao deletar receita, tente novamente ou entre em contato com o suporte", HttpStatusCode.InternalServerError);

            await RecipeRepository.SaveChanges(cancellationToken);

            return new Response(true, HttpStatusCode.OK);
        }
    }
}
