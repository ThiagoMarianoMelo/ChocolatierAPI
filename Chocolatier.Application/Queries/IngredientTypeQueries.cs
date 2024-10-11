using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using System.Net;

namespace Chocolatier.Application.Queries
{
    public class IngredientTypeQueries : IIngredientTypeQueries
    {
        private readonly IIngredientTypeRepository IngredientTypeRepository;

        public IngredientTypeQueries(IIngredientTypeRepository ingredientTypeRepository)
        {
            IngredientTypeRepository = ingredientTypeRepository; 
        }

        public async Task<Response> GetIngredientTypes(CancellationToken cancellationToken)
        {
            var ingredientsTypes = await IngredientTypeRepository.GetIngredientTypes(cancellationToken);

            return new Response(true, ingredientsTypes, HttpStatusCode.OK);
        }
    }
}
