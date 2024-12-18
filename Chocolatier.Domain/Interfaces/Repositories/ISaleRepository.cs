﻿using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        IQueryable<Sale> GetQueryableSalesByFilter(PaymentMethod? paymentMethod, Guid customerId, DateTime initialDateCreatedAt, DateTime finalDateCreatedAt);
        Task<List<Sale>> GetSalesFromEstablishmentFromDay(string EstablishmentId, DateTime dayFilter, CancellationToken cancellationToken);
        Task<int> GetTotalSalesFromToday(CancellationToken cancellationToken);
        Task<double> GetTotalBillingFromToday(CancellationToken cancellationToken);
    }
}
