namespace Chocolatier.Domain.ValueObjects
{

    public class Pagination<T> where T : class
    {
        public Pagination(IEnumerable<T> data, int total, int currentPage, int pageSize)
        {
            Data = data;
            Total = total;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
