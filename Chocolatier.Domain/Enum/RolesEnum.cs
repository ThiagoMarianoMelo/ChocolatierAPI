using Chocolatier.Util.EnumUtil;

namespace Chocolatier.Domain.Enum
{
    public enum RolesEnum
    {
        [StringValue("Headquarter")]
        Headquarter = 0,
        [StringValue("Factory")]
        Factory = 1,
        [StringValue("Store")]
        Store = 2,
    }
}
