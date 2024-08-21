using AutoMapper;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Responses.DataResponses;

namespace Chocolatier.API.Profiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Establishment, EstablishmentListDataResponse>();
            CreateMap<IngredientType, IngredientTypeListDataResponse>();

            CreateMap<Ingredient, IngredientListDataResponse>()
                .ForMember(dest => dest.ExpireAt, opt => opt.MapFrom(src => src.ExpireAt.ToLocalTime()));
        }
    }
}
