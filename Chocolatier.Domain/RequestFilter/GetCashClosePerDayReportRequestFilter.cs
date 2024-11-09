namespace Chocolatier.Domain.RequestFilter
{
    public class GetCashClosePerDayReportRequestFilter : BaseReportRequestFilter
    {
        public CashClosReportType ReportType { get; set; } = CashClosReportType.ByMoney;
    }

    public enum CashClosReportType
    {
        ByMoney = 0,
        ByAmount = 1
    }
}
