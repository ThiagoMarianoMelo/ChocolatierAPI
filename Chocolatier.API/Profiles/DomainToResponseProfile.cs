using AutoMapper;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Responses.DataResponses;

namespace Chocolatier.API.Profiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Establishment, EstablishmentListDataResponse>()
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => !src.LockoutEnabled));

            CreateMap<IngredientType, IngredientTypeListDataResponse>();

            CreateMap<Ingredient, IngredientListDataResponse>()
               .ForMember(dest => dest.ExpireAt, opt => opt.MapFrom(src => src.ExpireAt.ToLocalTime()));

            CreateMap<Recipe, RecipesListDataResponse>();

            CreateMap<RecipeItem, RecipeItensDataResponse>()
               .ForMember(dest => dest.IngredientTypeName, opt => opt.MapFrom(src => src.IngredientType.Name))
               .ForMember(dest => dest.MeasurementeUnit, opt => opt.MapFrom(src => src.IngredientType.MeasurementeUnit));
        }
    }
}
