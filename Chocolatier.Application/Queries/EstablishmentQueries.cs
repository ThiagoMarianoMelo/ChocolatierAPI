using AutoMapper;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;
using Chocolatier.Domain.Responses.DataResponses;
using Chocolatier.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Chocolatier.Application.Queries
{
    public class EstablishmentQueries : IEstablishmentQueries
    {
        private readonly IEstablishmentRepository EstablishmentRepository;
        private readonly IMapper Mapper;

        public EstablishmentQueries(IEstablishmentRepository establishmentRepository, IMapper mapper)
        {
            EstablishmentRepository = establishmentRepository;
            Mapper = mapper;
        }

        public async Task<Response> GetEstablishmentsPaginations(GetEstablishmentsPaginationsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CurrentPage <= 0)
                    return new Response(false, "A página não pode ser anterior a pagina inicial 0.", HttpStatusCode.BadRequest);

                var queryableData = EstablishmentRepository.GetQueryableEstablishmentsByFilter(request.Name, request.Email);

                var total = queryableData.Count();

                PageData(ref queryableData, request.CurrentPage, request.PageSize);

                var resultData = await queryableData.ToListAsync(cancellationToken);

                var responseData = Mapper.Map<List<EstablishmentListDataResponse>>(resultData);

                var result = new Pagination<EstablishmentListDataResponse>(responseData, total, request.CurrentPage, request.PageSize);

                return new Response(true, result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }

        }

        private void PageData(ref IQueryable<Establishment> queryableData, int pageIndex, int pageSize)
        {
             queryableData = queryableData.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
