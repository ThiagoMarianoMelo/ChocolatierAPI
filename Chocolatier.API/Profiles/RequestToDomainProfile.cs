﻿using AutoMapper;
using Chocolatier.Domain.Command.Customer;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Command.IngredientType;
using Chocolatier.Domain.Command.Order;
using Chocolatier.Domain.Command.Product;
using Chocolatier.Domain.Command.Recipe;
using Chocolatier.Domain.Command.Sale;
using Chocolatier.Domain.Entities;

namespace Chocolatier.API.Profiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateEstablishmentCommand, Establishment>();
            CreateMap<CreateIngredientTypeCommand, IngredientType>();
            CreateMap<CreateIngredientCommand, Ingredient>();
            CreateMap<CreateRecipeCommand, Recipe>();
            CreateMap<RecipteItemCommand, RecipeItem>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<OrderItemCommand, OrderItem>();
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<CreateSaleCommand, Sale>();
            CreateMap<SaleItemCommand, SaleItem>();
        }
    }
}
