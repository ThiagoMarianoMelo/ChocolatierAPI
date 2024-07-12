using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;
using Chocolatier.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Application.Queries
{
    public class EstablishmentQueries : IEstablishmentQueries
    {
        private readonly IEstablishmentRepository EstablishmentRepository;

        public EstablishmentQueries(IEstablishmentRepository establishmentRepository)
        {
            EstablishmentRepository = establishmentRepository;
        }

        public async Task<Response> GetEstablishmentsPaginations(GetEstablishmentsPaginationsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CurrentPage <= 0)
                    return new Response(false, "A página não pode ser anterior a pagina inicial 0.");

                var queryableData = EstablishmentRepository.GetQueryableEstablishmentsByFilter(request.Name, request.Email);

                //CONSULTAR APENAS DADOS NECESSÁRIOS (SELECT)

                var total = queryableData.Count();

                PageData(ref queryableData, request.CurrentPage, request.PageSize);

                var resultData = await queryableData.ToListAsync(cancellationToken);

                var result = new Pagination<Establishment>(resultData, total, request.CurrentPage, request.PageSize);

                //TRANSFORMAR EM RESPONSE PARA ENVIAR APENAS DADOS NECESSÁRIOS

                return new Response(true, result);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.");
            }

        }

        private void PageData(ref IQueryable<Establishment> queryableData, int pageIndex, int pageSize)
        {
             queryableData = queryableData.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
