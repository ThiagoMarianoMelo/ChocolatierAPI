using Chocolatier.Domain.Command.Jobs;
using MediatR;

namespace Chocolatier.API.Jobs
{
    public class CashClosingJob
    {
        private readonly IMediator Mediator;

        public CashClosingJob(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task ExecuteClashClose()
        {
            await Mediator.Send(new CashCloseJobCommand(), CancellationToken.None);
        }
    }
}
