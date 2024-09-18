using AutoMapper;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;
using Chocolatier.Domain.Responses.DataResponses;
using System.Net;

namespace Chocolatier.Application.Queries
{
    public class RecipeQueries : BasePaginationQueries<Recipe, RecipesListDataResponse>, IRecipeQueries
    {
        private readonly IRecipeRepository RecipeRepository;
        private readonly IRecipeItemRepository RecipeItemRepository;
        private readonly IIngredientRepository IngredientRepository;

        public RecipeQueries(IMapper mapper, IRecipeRepository recipeRepository, IRecipeItemRepository recipeItemRepository, IIngredientRepository ingredientRepository) : base(mapper)
        {
            RecipeRepository = recipeRepository;
            RecipeItemRepository = recipeItemRepository;
            IngredientRepository = ingredientRepository;
        }

        public async Task<Response> GetRecipesPagination(GetRecipesPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CurrentPage <= 0)
                    return new Response(false, "A página não pode ser anterior a pagina inicial 0.", HttpStatusCode.BadRequest);

                var queryableData = RecipeRepository.GetQueryableRecipesFilter(request.Name);

                var result = await BaseGetPaginantionDataByQueryable(queryableData, request, cancellationToken);

                return new Response(true, result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response> GetRecipeItens(Guid recipeId, CancellationToken cancellationToken)
        {
            try
            {
                if (recipeId == Guid.Empty)
                    return new Response(false, "Receita informada inválida", HttpStatusCode.BadRequest);

                var recipteItens = await RecipeItemRepository.GetItensFromRecipe(recipeId, cancellationToken);
                
                var resultData = new List<RecipeItensDataResponse>();

                recipteItens.ForEach(ri => resultData.Add(Mapper.Map<RecipeItensDataResponse>(ri)));

                return new Response(true, resultData, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response> GetMissingIngredientsFromRecipe(Guid recipeId, CancellationToken cancellationToken)
        {
            try
            {
                if (recipeId == Guid.Empty)
                    return new Response(false, "Receita informada inválida", HttpStatusCode.BadRequest);

                var recipteItens = await RecipeItemRepository.GetItensFromRecipe(recipeId, cancellationToken);

                var missingIngredients = new List<MissingItensFromRecipeDataResponse>();

                foreach (var item in recipteItens)
                {
                    var quantityOfIngredientsOnStorage = await IngredientRepository.GetDisponibleAmountIngredientsByIngredientType(item.IngredientTypeId, cancellationToken);

                    if (item.Quantity > quantityOfIngredientsOnStorage)
                        missingIngredients.Add(new MissingItensFromRecipeDataResponse
                        {
                            MissingAmout = item.Quantity - quantityOfIngredientsOnStorage,
                            MeasurementeUnit = item.IngredientType!.MeasurementeUnit,
                            IngredientName = item.IngredientType!.Name
                        });
                }

                return new Response(true, missingIngredients, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
