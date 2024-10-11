using Chocolatier.Domain.Command.Customer;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.CustomerHandlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Response>
    {
        private readonly ICustomerRepository CustomerRepository;

        public DeleteCustomerHandler(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        public async Task<Response> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var customer = await CustomerRepository.GetEntityById(request.Id, cancellationToken);

            if (customer == null)
                return new Response(false, "Customer não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            CustomerRepository.DeleteEntity(customer, cancellationToken);

            var result = await CustomerRepository.SaveChanges(cancellationToken);

            if (result <= 0)
                return new Response(false, "Customer não deletado tente novamente ou entre em contato com o suporte.", HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK);
        }
    }
}
