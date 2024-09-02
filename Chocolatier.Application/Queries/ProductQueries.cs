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
    public class ProductQueries : BaseQueries<Product, ProductListDataResponse>, IProductQueries
    {
        private readonly IProductRepository ProductRepository;

        public ProductQueries(IMapper mapper, IProductRepository productRepository) : base(mapper)
        {
            ProductRepository = productRepository;
        }
        public async Task<Response> GetIngredientPagination(GetProductsPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CurrentPage <= 0)
                    return new Response(false, "A página não pode ser anterior a pagina inicial 0.", HttpStatusCode.BadRequest);

                var queryableData = ProductRepository.GetQueryableProductsByFilter(request.InitialExpiratonDate, request.FinalExpiratonDate, request.Name);

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
