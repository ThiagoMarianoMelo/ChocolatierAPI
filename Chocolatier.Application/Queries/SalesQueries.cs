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
        private readonly ISaleItemRepository SaleItemRepository;

        public SalesQueries(IMapper mapper, ISaleRepository saleRepository, ISaleItemRepository saleItemRepository) : base(mapper)
        {
            SaleRepository = saleRepository;
            SaleItemRepository = saleItemRepository;
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

        public async Task<Response> GetSaleItens(Guid saleId, CancellationToken cancellationToken)
        {
            try
            {
                if (saleId == Guid.Empty)
                    return new Response(false, "Venda informada inválida", HttpStatusCode.BadRequest);

                var saleItens = await SaleItemRepository.GetItensFromSales(saleId, cancellationToken);

                var resultData = new List<SaleItensDataResponse>();

                saleItens.ForEach(ri => resultData.Add(Mapper.Map<SaleItensDataResponse>(ri)));

                return new Response(true, resultData, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
