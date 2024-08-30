using AutoMapper;
using Chocolatier.Domain.Command.Recipe;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.RecipeHandlers
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, Response>
    {
        private readonly IRecipeItemRepository RecipeItemRepository;
        private readonly IRecipeRepository RecipeRepository;
        private readonly IMapper Mapper;

        public UpdateRecipeHandler(IRecipeItemRepository recipeItemRepository, IRecipeRepository recipeRepository, IMapper mapper)
        {
            RecipeItemRepository = recipeItemRepository;
            RecipeRepository = recipeRepository;
            Mapper = mapper;
        }

        public async Task<Response> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);

                var recipe = await RecipeRepository.GetEntityById(request.Id, cancellationToken);

                if(recipe == null)
                    return new Response(true, ["Receita não encontrada."], HttpStatusCode.BadRequest);

                RecipeItemRepository.DeleteItensFromRecipe(request.Id);

                recipe.Name = request.Name!;

                RecipeRepository.UpdateEntity(recipe, cancellationToken);

                var recipeItens = Mapper.Map<List<RecipeItem>>(request.RecipeItems);

                recipeItens.ForEach(ri => ri.RecipeId = recipe.Id);

                recipeItens.ForEach(async ri => await RecipeItemRepository.Create(ri, cancellationToken));

                var result = await RecipeRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao atualizar receita."], HttpStatusCode.InternalServerError);

                return new Response(true, recipe.Id, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
