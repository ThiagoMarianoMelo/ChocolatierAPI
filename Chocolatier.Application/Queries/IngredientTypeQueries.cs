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
    public class IngredientTypeQueries : BaseQueries<IngredientType, IngredientTypeListDataResponse>, IIngredientTypeQueries
    {
        private readonly IIngredientTypeRepository IngredientTypeRepository;

        public IngredientTypeQueries(IMapper mapper, IIngredientTypeRepository ingredientTypeRepository) :base(mapper)
        {
            IngredientTypeRepository = ingredientTypeRepository; 
        }

        public async Task<Response> GetIngredientTypesPagination(GetIngredientTypesPaginationsRequest request, CancellationToken cancellationToken)
        {
            try
            {   
                if (request.CurrentPage <= 0)
                    return new Response(false, "A página não pode ser anterior a pagina inicial 0.", HttpStatusCode.BadRequest);

                var queryableData = IngredientTypeRepository.GetQueryableIngredientTypesByFilter(request.Name);

                var result = await BaseGetPaginantionDataByQueryable(queryableData, request, cancellationToken);

                return new Response(true, result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
