using Chocolatier.API.Authorization;
using Chocolatier.Domain.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomePageController : BaseController
    {
        private readonly IHomePageQueries HomePageQueries;
        public HomePageController(IMediator mediator, IHomePageQueries homePageQueries) : base(mediator)
        {
            HomePageQueries = homePageQueries;
        }

        [HttpGet]
        [FactoryAuthorization]
        [Route("Factory")]
        public async Task<IActionResult> GetFactoryHomeData(CancellationToken cancellationToken)
        {
            return GetActionResult(await HomePageQueries.GetHomeFactoryData(cancellationToken));
        }
    }
}
