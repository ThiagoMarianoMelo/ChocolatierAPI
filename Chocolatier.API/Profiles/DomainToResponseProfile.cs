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
               .ForMember(dest => dest.IngredientTypeName, opt => opt.MapFrom(src => src.IngredientType!.Name))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IngredientType!.Id))
               .ForMember(dest => dest.MeasurementeUnit, opt => opt.MapFrom(src => src.IngredientType!.MeasurementeUnit));

            CreateMap<Product, ProductListDataResponse>()
                .ForMember(dest => dest.ExpireAt, opt => opt.MapFrom(src => src.ExpireAt.ToLocalTime()));

            CreateMap<Order, OrdersListDataResponse>()
                .ForMember(dest => dest.DeadLine, opt => opt.MapFrom(src => src.DeadLine.ToLocalTime()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime()));

            CreateMap<OrderItem, OrderItensDataResponse>()
               .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.Recipe!.Name));

            CreateMap<OrderHistory, OrderHistoryDataResponse>()
              .ForMember(dest => dest.ChangedAt, opt => opt.MapFrom(src => src.ChangedAt.ToLocalTime()));
        }
    }
}
