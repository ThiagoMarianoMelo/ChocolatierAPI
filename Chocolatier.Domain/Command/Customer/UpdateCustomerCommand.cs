using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Command.Customer
{
    public class UpdateCustomerCommand : BaseComamnd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação do cliente, tente novamente."));
        }
    }
}
