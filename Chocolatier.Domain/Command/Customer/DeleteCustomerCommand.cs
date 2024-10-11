using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Customer
{
    public class DeleteCustomerCommand : BaseComamnd
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Name", "O Nome do cliente é obrigatório."));
        }
    }
}

