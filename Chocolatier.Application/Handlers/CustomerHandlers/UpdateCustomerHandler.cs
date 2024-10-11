using Chocolatier.Domain.Command.Customer;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.CustomerHandlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Response>
    {
        private readonly ICustomerRepository CustomerRepository;

        public UpdateCustomerHandler(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        public async Task<Response> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var customer = await CustomerRepository.GetEntityById(request.Id, cancellationToken);

            if (customer is null)
                return new Response(false, "Cliente não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            var dataChanged = await ChangeDataFromCustomer(customer, request, cancellationToken);

            if (!dataChanged)
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK);
        }

        private async Task<bool> ChangeDataFromCustomer(Customer customer, UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
                customer.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Email))
                customer.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Phone))
                customer.Phone = request.Phone;

            if (!string.IsNullOrWhiteSpace(request.Address))
                customer.Address = request.Address;

            CustomerRepository.UpdateEntity(customer, cancellationToken);

            var result = await CustomerRepository.SaveChanges(cancellationToken);

            return result > 0;
        }
    }
}
