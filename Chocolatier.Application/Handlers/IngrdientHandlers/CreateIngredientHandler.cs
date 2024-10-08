﻿using AutoMapper;
using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.IngrdientHandlers
{
    public class CreateIngredientHandler : IRequestHandler<CreateIngredientCommand, Response>
    {
        private readonly IIngredientRepository IngredientRepository;
        private readonly IMapper Mapper;
        private readonly IIngredientTypeRepository IngredientTypeRepository;

        public CreateIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper, IIngredientTypeRepository ingredientTypeRepository)
        {
            IngredientRepository = ingredientRepository;
            Mapper = mapper;
            IngredientTypeRepository = ingredientTypeRepository;
        }

        public async Task<Response> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);

                var ingredientTypeIsActive = await IngredientTypeRepository.IsActiveById(request.IngredientTypeId, cancellationToken);

                if(!ingredientTypeIsActive)
                    return new Response(true, ["Tipo de ingrediente escolhido não é valido."], HttpStatusCode.BadRequest);

                var entity = Mapper.Map<Ingredient>(request);

                entity.ExpireAt = entity.ExpireAt.ToUniversalTime();

                var resultEntity = await IngredientRepository.Create(entity, cancellationToken);

                if (resultEntity is null)
                    return new Response(true, ["Erro ao cadastrar ingrediente."], HttpStatusCode.InternalServerError);

                var result = await IngredientRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao cadastrar ingrediente."], HttpStatusCode.InternalServerError);

                return new Response(true, resultEntity, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
