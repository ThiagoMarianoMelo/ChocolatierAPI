namespace Chocolatier.Domain.Responses.DataResponses
{
    public class BaseReportResponse<T>
    {
        public List<DataPerDay<T>> ReportData { get; set; } = [];
    }

    public class DataPerDay<T>
    {
        public DateTime Date { get; set; }
        public T? Amount { get; set; }
    }
}
