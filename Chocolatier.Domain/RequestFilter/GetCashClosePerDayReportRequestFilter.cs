namespace Chocolatier.Domain.RequestFilter
{
    public class GetCashClosePerDayReportRequestFilter : BaseReportRequestFilter
    {
        public CashCloseReportType ReportType { get; set; } = CashCloseReportType.ByMoney;
    }

    public enum CashCloseReportType
    {
        ByMoney = 0,
        ByAmount = 1
    }
}
