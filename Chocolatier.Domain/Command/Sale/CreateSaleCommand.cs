using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Sale
{
    public class CreateSaleCommand : BaseComamnd
    {
        public Guid CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<SaleItemCommand>? SaleItens { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
            .IsFalse(CustomerId == Guid.Empty, "CustomerId", "Problema interno para identificação do cliente, tente novamente.")
            .IsFalse(SaleItens is null || SaleItens.Count == 0, "SaleItens", "Problema interno para identificação dos itens da venda, tente novamente."));
        }

    }
}
