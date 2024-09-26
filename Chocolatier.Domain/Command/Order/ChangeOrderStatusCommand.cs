using Chocolatier.Domain.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Command.Order
{
    public class ChangeOrderStatusCommand : BaseComamnd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public OrderStatus NewStatus { get; set; }
        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
                .IsFalse(Id == Guid.Empty, "OrderId", "Problema interno para identificação do identificador do pedido, tente novamente.")
                .IsFalse(NewStatus == OrderStatus.Canceled, "NewStatus", "Para cancelar um pedido siga o fluxo de cancelamento e informe um motivo."));
        }
    }
}
