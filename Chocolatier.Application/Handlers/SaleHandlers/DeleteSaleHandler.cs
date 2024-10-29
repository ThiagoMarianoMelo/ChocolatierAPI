using Chocolatier.Domain.Command.Sale;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.SaleHandlers
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, Response>
    {
        private readonly ISaleRepository SaleRepository;

        public DeleteSaleHandler(ISaleRepository saleRepository)
        {
            SaleRepository = saleRepository;
        }

        public async Task<Response> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var sale = await SaleRepository.GetEntityById(request.Id, cancellationToken);

            if (sale == null)
                return new Response(false, "Venda não encontrada tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            SaleRepository.DeleteEntity(sale, cancellationToken);

            var result = await SaleRepository.SaveChanges(cancellationToken);

            if (result <= 0)
                return new Response(false, "Venda não deletada tente novamente ou entre em contato com o suporte.", HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK);
        }
    }
}
