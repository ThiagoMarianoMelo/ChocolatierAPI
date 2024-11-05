using Chocolatier.Domain.Command.Jobs;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.JobHandlers
{
    public class CashCloseJobHandler : IRequestHandler<CashCloseJobCommand, Response>
    {
        private readonly IEstablishmentRepository EstablishmentRepository;
        private readonly ISaleRepository SaleRepository;
        private readonly ICashCloseRepository CashCloseRepository;

        public CashCloseJobHandler(IEstablishmentRepository establishmentRepository, ICashCloseRepository cashCloseRepository, ISaleRepository saleRepository)
        {
            EstablishmentRepository = establishmentRepository;
            CashCloseRepository = cashCloseRepository;
            SaleRepository = saleRepository;
        }

        public async Task<Response> Handle(CashCloseJobCommand request, CancellationToken cancellationToken)
        {

            var stores = await EstablishmentRepository.GetStores(cancellationToken);

            foreach (var store in stores)
            {
                var salesFromStore = await SaleRepository.GetSalesFromEstablishmentFromDay(store.Id, DateTime.Now.Date, cancellationToken);

                var cashCloseEntity = new CashClose()
                {
                    SaleQuantity = salesFromStore.Count,
                    Billing = salesFromStore.Sum(s => s.TotalPrice),
                    Date = DateTime.UtcNow.Date,
                    EstablishmentId = store.Id
                };

                await CashCloseRepository.Create(cashCloseEntity, cancellationToken);

                await CashCloseRepository.SaveChanges(cancellationToken);
            }

            return new Response(true, $"Rotina de fechamento de caixa executada {DateTime.UtcNow}", HttpStatusCode.OK);
        }
    }
}
