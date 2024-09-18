using Chocolatier.Domain.Command.Recipe;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using Chocolatier.Domain.Responses.DataResponses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.RecipeHandlers
{
    public class MakeRecipeHandler : IRequestHandler<MakeRecipeCommand, Response>
    {
        private readonly IRecipeItemRepository RecipeItemRepository;
        private readonly IIngredientRepository IngredientRepository;
        private readonly IRecipeQueries RecipeQueries;

        public MakeRecipeHandler(IRecipeItemRepository recipeItemRepository, IIngredientRepository ingredientRepository, IRecipeQueries recipeQueries)
        {
            RecipeItemRepository = recipeItemRepository;
            IngredientRepository = ingredientRepository;
            RecipeQueries = recipeQueries;
        }

        public async Task<Response> Handle(MakeRecipeCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var missingItens = (await RecipeQueries.GetMissingIngredientsFromRecipe(request.Id, cancellationToken)).Data as List<MissingItensFromRecipeDataResponse>;

            if (missingItens == null || missingItens.Count > 0)
                return new Response(false, "Não há ingredientes suficiente em estoque para realizar a receita.", HttpStatusCode.InternalServerError);

            var recipteItens = await RecipeItemRepository.GetItensFromRecipe(request.Id, cancellationToken);

            foreach (var recipeItem in recipteItens)
            {
                var ingredientsOnStorage = await IngredientRepository.GetDisponibleIngredientsByIngredientType(recipeItem.IngredientTypeId, cancellationToken);

                await RemoveIngredientsFromStorage(ingredientsOnStorage, recipeItem.Quantity, cancellationToken);
            }

            await IngredientRepository.SaveChanges(cancellationToken);

            return new Response(true, HttpStatusCode.OK);
        }


        private Task RemoveIngredientsFromStorage(List<Ingredient> ingredientsOnStorage, int quantity, CancellationToken cancellationToken)
        {
            var missingAmount = quantity;

            foreach (var ingredient in ingredientsOnStorage)
            {
                if (ingredient.Amount > missingAmount)
                {
                    ingredient.Amount = ingredient.Amount - missingAmount;

                    IngredientRepository.UpdateEntity(ingredient, cancellationToken);

                    return Task.CompletedTask;
                }

                missingAmount = missingAmount - ingredient.Amount;

                IngredientRepository.DeleteEntity(ingredient, cancellationToken);

                if (missingAmount <= 0)
                    return Task.CompletedTask;
            }

            throw new PathTooLongException("ERRO AO REMOVER ITENS DO ESTOQUE AO REALIZAR RECEITA, METODO RemoveIngredientsFromStorage NAO RETORNOU");
        }
    }
}
