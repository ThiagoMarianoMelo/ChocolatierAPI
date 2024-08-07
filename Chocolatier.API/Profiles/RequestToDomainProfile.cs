﻿using AutoMapper;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Command.IngredientType;
using Chocolatier.Domain.Entities;

namespace Chocolatier.API.Profiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateEstablishmentCommand, Establishment>();
            CreateMap<CreateIngredientTypeCommand, IngredientType>();
        }
    }
}
