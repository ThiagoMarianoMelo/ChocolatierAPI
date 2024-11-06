using AutoMapper;
using Chocolatier.Domain.Command.Customer;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.CustomerHandlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response>
    {
        private readonly ICustomerRepository CustomerRepository;
        private readonly IMapper Mapper;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            CustomerRepository = customerRepository;
            Mapper = mapper;
        }

        public async Task<Response> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);
                
                var entity = Mapper.Map<Customer>(request);

                entity.CreatedAt = DateTime.UtcNow;

                var resultEntity = await CustomerRepository.Create(entity, cancellationToken);

                if (resultEntity is null)
                    return new Response(true, ["Erro ao cadastrar cliente."], HttpStatusCode.InternalServerError);

                var result = await CustomerRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao cadastrar cliente."], HttpStatusCode.InternalServerError);

                return new Response(true, resultEntity, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
