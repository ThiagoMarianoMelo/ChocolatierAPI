using AutoMapper;
using Chocolatier.Domain.Command.Sale;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.SaleHandlers
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Response>
    {
        private readonly IProductRepository ProductRepository;
        private readonly IMapper Mapper;
        private readonly IAuthEstablishment AuthEstablishment;
        private readonly ISaleRepository SaleRepository;
        private readonly ISaleItemRepository SaleItemRepository;
        private readonly ICustomerRepository CustomerRepository;

        public CreateSaleHandler(IProductRepository productRepository, IMapper mapper, IAuthEstablishment authEstablishment, ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, ICustomerRepository customerRepository)
        {
            ProductRepository = productRepository;
            Mapper = mapper;
            AuthEstablishment = authEstablishment;
            SaleRepository = saleRepository;
            SaleItemRepository = saleItemRepository;
            CustomerRepository = customerRepository;
        }

        public async Task<Response> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);

                var customer = await CustomerRepository.GetEntityById(request.CustomerId, cancellationToken);

                if (customer is null)
                    return new Response(true, ["Erro ao encontrar cliente."], HttpStatusCode.InternalServerError);

                var productsVerifyResponse = VerifyProductsAreAvaibles(request.SaleItens!);

                if (!productsVerifyResponse.Success)
                    return productsVerifyResponse;

                var sale = Mapper.Map<Sale>(request);

                sale.EstablishmentId = AuthEstablishment.Id;
                sale.TotalPrice = request.SaleItens!.Sum(si => ProductRepository.GetProductPriceByRecipeId(si.RecipeId) * si.Quantity);
                sale.SaleDate = DateTime.UtcNow;

                var saleResult = await SaleRepository.Create(sale, cancellationToken);

                if (saleResult is null)
                    return new Response(true, ["Erro ao cadastrar venda."], HttpStatusCode.InternalServerError);

                var saleItens = Mapper.Map<List<SaleItem>>(request.SaleItens);


                saleItens.ForEach(async saleItem => {

                    saleItem.SaleId = saleResult.Id;

                    saleItem.UnityPrice = ProductRepository.GetProductPriceByRecipeId(saleItem.RecipeId);

                    await SaleItemRepository.Create(saleItem, cancellationToken);
                });

                var result = await SaleRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao cadastrar venda, entre em contato com o suporte."], HttpStatusCode.InternalServerError);

                await DeleteSaledProducts(request.SaleItens!, cancellationToken);

                return new Response(true, saleResult.Id, HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }

        private Response VerifyProductsAreAvaibles(IEnumerable<SaleItemCommand> saleItens)
        {
            foreach (var item in saleItens)
            {
                var productQuantityInStorage = ProductRepository.GetProductQuantityInStorageByRecipeId(item.RecipeId);

                if (productQuantityInStorage < item.Quantity)
                    return new Response(false, [$"Não há produtos suficientes em estoque."], HttpStatusCode.BadRequest);
            }

            if (saleItens.Select(Si => Si.RecipeId).Count() != saleItens.Select(Si => Si.RecipeId).Distinct().Count())
                return new Response(false, ["Existem itens duplicados na venda."], HttpStatusCode.BadRequest);

            return new Response(true);
        }

        private async Task DeleteSaledProducts(IEnumerable<SaleItemCommand> saleItens, CancellationToken cancellationToken)
        {

            foreach (var item in saleItens)
            {
                var productOnStorage = await ProductRepository.GetProductsOnStorageByRecipeId(item.RecipeId, cancellationToken);

                var missingAmount = item.Quantity;

                foreach (var product in productOnStorage)
                {

                    if (product.Quantity > missingAmount)
                    {
                        product.Quantity = product.Quantity - missingAmount;

                        ProductRepository.UpdateEntity(product, cancellationToken);
                    }
                    else
                    {

                        missingAmount = missingAmount - product.Quantity;

                        ProductRepository.DeleteEntity(product, cancellationToken);
                    }

                    if (missingAmount <= 0)
                        break;
                }
            }

            await ProductRepository.SaveChanges(cancellationToken);
        }
    }
}
