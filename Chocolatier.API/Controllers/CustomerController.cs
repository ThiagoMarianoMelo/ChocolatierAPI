using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Customer;
using Chocolatier.Domain.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerQueries CustomerQueries;

        public CustomerController(IMediator mediator, ICustomerQueries customerQueries) : base(mediator)
        {
            CustomerQueries = customerQueries;
        }

        [HttpPost]
        [StoreAuthorization]
        public async Task<IActionResult> Post(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [StoreAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            return GetActionResult(await CustomerQueries.GetCustomersList(cancellationToken));
        }

        [HttpDelete]
        [StoreAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var request = new DeleteCustomerCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
