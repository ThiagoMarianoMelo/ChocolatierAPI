﻿using AutoMapper;
using Chocolatier.Domain.Command.Product;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.ProductsHandlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Response>
    {
        private readonly IAuthEstablishment AuthEstablishment;
        private readonly IMapper Mapper;
        private readonly IProductRepository ProductRepository;

        public CreateProductHandler(IAuthEstablishment authEstablishment, IMapper mapper, IProductRepository productRepository)
        {
            AuthEstablishment = authEstablishment;
            Mapper = mapper;
            ProductRepository = productRepository;
        }

        public async Task<Response> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);

                var product = Mapper.Map<Product>(request);

                product.CurrentEstablishmentId = AuthEstablishment.Id;

                var resultEntity = await ProductRepository.Create(product, cancellationToken);

                if (resultEntity is null)
                    return new Response(true, ["Erro ao cadastrar produto."], HttpStatusCode.InternalServerError);

                var result = await ProductRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao cadastrar produto."], HttpStatusCode.InternalServerError);

                return new Response(true, HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
