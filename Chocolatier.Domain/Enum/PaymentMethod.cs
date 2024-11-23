using Chocolatier.Util.EnumUtil;

namespace Chocolatier.Domain.Enum
{
    public enum PaymentMethod
    {
        [StringValue("Cartão de Credito")]
        CreditCard = 0,
        [StringValue("Dinheiro")]
        Cash = 1,
        [StringValue("Cartão de Debito")]
        DebitCard = 2,
        [StringValue("Pix")]
        Pix = 3
    }
}
