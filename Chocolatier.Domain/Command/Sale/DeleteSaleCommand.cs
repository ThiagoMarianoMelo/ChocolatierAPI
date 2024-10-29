using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Sale
{
    public class DeleteSaleCommand : BaseComamnd
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação da venda, tente novamente."));
        }
    }
}
