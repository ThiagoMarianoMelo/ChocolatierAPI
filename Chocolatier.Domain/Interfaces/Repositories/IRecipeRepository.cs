﻿using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        IQueryable<Recipe> GetQueryableRecipesFilter(string name);
    }
}