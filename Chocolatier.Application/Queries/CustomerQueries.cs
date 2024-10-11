using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using System.Net;

namespace Chocolatier.Application.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly ICustomerRepository CustomerRepository;

        public CustomerQueries(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        public async Task<Response> GetCustomersList(CancellationToken cancellationToken)
        {
            var customers = await CustomerRepository.GetCustomers(cancellationToken);

            return new Response(true, customers, HttpStatusCode.OK);
        }
    }
}
