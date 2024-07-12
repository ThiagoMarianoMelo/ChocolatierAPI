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
        }
    }
}
