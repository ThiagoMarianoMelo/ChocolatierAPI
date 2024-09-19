using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Order
{
    public class CreateOrderCommand : BaseComamnd
    {
        public DateTime DeadLine { get; set; }
        public List<OrderItemCommand>? OrderItens { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
                .IsFalse(DeadLine == DateTime.MinValue, "DeadLine", "Problema interno para identificação do prazo do pedido, tente novamente.")
                .IsFalse(OrderItens is null || OrderItens.Count == 0, "OrderItens", "Problema interno para identificação dos itens do pedido, tente novamente."));
        }
    }
}
