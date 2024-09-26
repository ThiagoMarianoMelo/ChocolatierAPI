using Chocolatier.Util.EnumUtil;

namespace Chocolatier.Domain.Enum
{
    public enum OrderStatus
    {
        [StringValue("Pendente")]
        Pending = 0,
        [StringValue("Confirmado")]
        Confirmed = 1,
        [StringValue("Em Preparo")]
        OnPrepare = 2,
        [StringValue("Em Rota de Entrega")]
        OnDelivery = 3,
        [StringValue("Finalizado")]
        Done = 4,
        [StringValue("Cancelado")]
        Canceled = 5,
    }
}
