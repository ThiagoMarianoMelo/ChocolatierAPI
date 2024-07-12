using AutoMapper;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Responses.DataResponses;
using Microsoft.AspNetCore.Identity;

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
