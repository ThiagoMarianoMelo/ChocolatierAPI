﻿using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IIngredientTypeQueries
    {
        Task<Response> GetIngredientTypes(CancellationToken cancellationToken);
    }
}
