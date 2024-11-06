namespace Chocolatier.Domain.Responses.DataResponses
{
    public class BaseReportResponse
    {
        public List<DataPerDay> ReportData { get; set; } = [];
    }

    public class DataPerDay
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }
    }
}
