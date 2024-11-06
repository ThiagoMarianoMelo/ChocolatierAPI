using AutoMapper;
using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Application.Queries
{
    public abstract class BasePaginationQueries<Entity, Response>
    {
        protected readonly IMapper Mapper;

        protected BasePaginationQueries(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected async Task<Pagination<Response>> BaseGetPaginantionDataByQueryable(IQueryable<Entity> queryableData, BasePaginationRequestFilter baseRequestFilter, CancellationToken cancellationToken)
        {
            var total = queryableData.Count();

            PageData(ref queryableData, baseRequestFilter.CurrentPage, baseRequestFilter.PageSize);

            var resultData = await queryableData.ToListAsync(cancellationToken);
            var responseData = Mapper.Map<List<Response>>(resultData);

            return new Pagination<Response>(responseData, total, baseRequestFilter.CurrentPage, baseRequestFilter.PageSize);
        }
        private static void PageData(ref IQueryable<Entity> queryableData, int pageIndex, int pageSize)
        {
            queryableData = queryableData.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
