using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Command.Order
{
    public class CancelOrderCommand : BaseComamnd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string CancelReason { get; set; } = string.Empty;
        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
                .IsFalse(Id == Guid.Empty, "OrderId", "Problema interno para identificação do pedido, tente novamente.")
                .IsFalse(string.IsNullOrWhiteSpace(CancelReason), "CancelReason", "O motivo do cancelamento é obrigatório."));
        }

    }
}
