using Chocolatier.Domain.Command.Product;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.ProductsHandlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Response>
    {
        private readonly IProductRepository ProductRepository;
        private readonly IAuthEstablishment AuthEstablishment;

        public DeleteProductHandler(IProductRepository productRepository, IAuthEstablishment authEstablishment)
        {
            ProductRepository = productRepository;
            AuthEstablishment = authEstablishment;
        }

        public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var product = await ProductRepository.GetEntityById(request.Id, cancellationToken);

            if (product == null)
                return new Response(false, "Produto não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            if (product.CurrentEstablishmentId != AuthEstablishment.Id)
                return new Response(false, "Você não possui permissão para modificar o produto, ele não está em seu estabelecimento.", HttpStatusCode.BadRequest);

            ProductRepository.DeleteEntity(product, cancellationToken);

            var result = await ProductRepository.SaveChanges(cancellationToken);

            if (result <= 0)
                return new Response(false, "Ingrediente não deletado tente novamente ou entre em contato com o suporte.", HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK); ;
        }
    }
}
