using Chocolatier.Util.EnumUtil;

namespace Chocolatier.Domain.Enum
{
    public enum EmailTemplate
    {
        [StringValue("Pedido Criado")]
        OrderCreated = 0,
        [StringValue("Pedido Cancelado")]
        OrderCanceled = 1,
        [StringValue("Pedido Alterado")]
        OrderUpdated = 2
    }
}
