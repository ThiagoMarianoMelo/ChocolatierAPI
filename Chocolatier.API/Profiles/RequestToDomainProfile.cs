using AutoMapper;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Entities;

namespace Chocolatier.API.Profiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateEstablishmentCommand, Establishment>();
        }
    }
}
