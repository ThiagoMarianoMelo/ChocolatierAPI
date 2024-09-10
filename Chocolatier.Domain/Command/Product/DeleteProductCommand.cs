using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Product
{
    public class DeleteProductCommand : BaseComamnd
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação do Produto, tente novamente."));
        }
    }
}
