using AutoMapper;
using Chocolatier.Domain.Command.IngredientType;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers
{
    public class CreateIngredientTypeHandler : IRequestHandler<CreateIngredientTypeCommand, Response>
    {
        private readonly IIngredientTypeRepository IngredientTypeRepository;
        private readonly IMapper Mapper;

        public CreateIngredientTypeHandler(IIngredientTypeRepository ingredientTypeRepository, IMapper mapper)
        {
            IngredientTypeRepository = ingredientTypeRepository;
            Mapper = mapper;
        }

        public async Task<Response> Handle(CreateIngredientTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);

                var entity = Mapper.Map<IngredientType>(request);
                entity.IsActive = true;

                var resultEntity = await IngredientTypeRepository.Create(entity, cancellationToken);

                if (resultEntity is null)
                    return new Response(true, ["Erro ao cadastrar tipo de ingrediente."], HttpStatusCode.InternalServerError);

                var result = await IngredientTypeRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao cadastrar tipo de ingrediente."], HttpStatusCode.InternalServerError);

                return new Response(true, resultEntity, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
