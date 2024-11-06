namespace Chocolatier.Domain.RequestFilter
{
    public class BaseReportRequestFilter
    {
        public DateTime StartDate { get => _startDate; set { _startDate = value.ToUniversalTime(); } }
        public DateTime EndDate { get => _endDate; set { _endDate = value.ToUniversalTime(); } }

        private DateTime _startDate { get; set; } = DateTime.UtcNow.AddDays(-7);
        private DateTime _endDate { get; set; } = DateTime.UtcNow;

    }
}
