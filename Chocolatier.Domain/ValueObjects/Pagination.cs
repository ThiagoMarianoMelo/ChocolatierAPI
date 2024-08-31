namespace Chocolatier.Domain.ValueObjects
{

    public class Pagination<T>
    {
        public Pagination(IEnumerable<T> data, int total, int currentPage, int pageSize)
        {
            PaginationData = data;
            Total = total;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public IEnumerable<T> PaginationData { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
