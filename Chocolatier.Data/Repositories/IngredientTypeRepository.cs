﻿using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chocolatier.Data.Repositories
{
    public class IngredientTypeRepository : BaseRepository<IngredientType>, IIngredientTypeRepository
    {
        public IngredientTypeRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public IQueryable<IngredientType> GetQueryableIngredientTypesByFilter(string name)
        {
            var queryCondiction = BuildQueryIngredientTypeFilter(name);

            return DbSet.AsNoTracking()
                    .Where(queryCondiction)
                    .Select(es => new IngredientType()
                    {
                        Id = es.Id,
                        Name = es.Name,
                        MeasurementeUnit = es.MeasurementeUnit
                    })
                    .OrderBy(it => it.Name);
        }

        private Expression<Func<IngredientType, bool>> BuildQueryIngredientTypeFilter(string name)
        {

            return it => it.IsActive &&
                         (string.IsNullOrWhiteSpace(name) || it.Name!.Contains(name));
        }
    }
}
