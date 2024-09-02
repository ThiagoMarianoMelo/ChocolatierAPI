using AutoMapper;
using Chocolatier.Domain.Command.Recipe;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.RecipeHandlers
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, Response>
    {
        private readonly IRecipeItemRepository RecipeItemRepository;
        private readonly IIngredientTypeRepository IngredientTypeRepository;
        private readonly IRecipeRepository RecipeRepository;
        private readonly IMapper Mapper;

        public CreateRecipeHandler(IRecipeItemRepository recipeItemRepository, IRecipeRepository recipeRepository, IMapper mapper, IIngredientTypeRepository ingredientTypeRepository)
        {
            RecipeItemRepository = recipeItemRepository;
            RecipeRepository = recipeRepository;
            Mapper = mapper;
            IngredientTypeRepository = ingredientTypeRepository;
        }

        public async Task<Response> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);

                var ingredientTypeVerifiyResponse = await VerifyIngredientTypes(request.RecipeItems!.Select(rt => rt.IngredientTypeId), cancellationToken);

                if (!ingredientTypeVerifiyResponse.Success)
                    return ingredientTypeVerifiyResponse;

                var recipe = Mapper.Map<Recipe>(request);

                recipe.IsActive = true;

                var recipeResult = await RecipeRepository.Create(recipe, cancellationToken);

                if (recipeResult is null)
                    return new Response(true, ["Erro ao cadastrar receita."], HttpStatusCode.InternalServerError);

                var recipeItens = Mapper.Map<List<RecipeItem>>(request.RecipeItems);

                recipeItens.ForEach(ri => ri.RecipeId = recipeResult.Id);

                recipeItens.ForEach(async ri => await RecipeItemRepository.Create(ri, cancellationToken));

                var result = await RecipeRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao cadastrar receita."], HttpStatusCode.InternalServerError);

                return new Response(true, recipeResult.Id ,HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }

        private async Task<Response> VerifyIngredientTypes(IEnumerable<Guid> ingrendientTypeIds, CancellationToken cancellationToken)
        {
            foreach (var itId in ingrendientTypeIds)
            {
                var isActive = await IngredientTypeRepository.IsActiveById(itId, cancellationToken);

                if (!isActive) return new Response(false, [$"O tipo de ingrediente {itId} não está ativo, entre em contato com o suporte"], HttpStatusCode.BadRequest);
            }

            return new Response(true);
        }
    }
}
