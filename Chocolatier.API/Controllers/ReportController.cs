using Chocolatier.API.Authorization;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : BaseController
    {
        private readonly IReportQueries ReportQueries;
        public ReportController(IMediator mediator, IReportQueries reportQueries) : base(mediator)
        {
            ReportQueries = reportQueries;
        }

        [HttpGet]
        [StoreAuthorization]
        [Route("NewCustomers")]
        public async Task<IActionResult> GetList([FromQuery] BaseReportRequestFilter request, CancellationToken cancellationToken)
        {
            return GetActionResult(await ReportQueries.GetCustomerPerDayReport(request, cancellationToken));
        }
    }
}
