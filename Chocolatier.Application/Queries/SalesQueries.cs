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
    public class SalesQueries : BasePaginationQueries<Sale, SalesListDataResponse>, ISalesQueries
    {
        private readonly ISaleRepository SaleRepository;
        public SalesQueries(IMapper mapper, ISaleRepository saleRepository) : base(mapper)
        {
            SaleRepository = saleRepository;
        }

        public async Task<Response> GetSalesPagination(GetSalesPaginationsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CurrentPage <= 0)
                    return new Response(false, "A página não pode ser anterior a pagina inicial 0.", HttpStatusCode.BadRequest);

                var queryableData = SaleRepository.GetQueryableSalesByFilter(request.PaymentMethod, request.SaleId, request.InitialCreatedAtDate, request.FinalCreatedAtDate);

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
