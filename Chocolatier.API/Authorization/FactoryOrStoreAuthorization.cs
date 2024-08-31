using Chocolatier.Domain.Enum;
using Chocolatier.Util.EnumUtil;
using Microsoft.AspNetCore.Authorization;

namespace Chocolatier.API.Authorization
{
    public class FactoryOrStoreAuthorization : AuthorizeAttribute
    {
        public FactoryOrStoreAuthorization()
        {
            Roles = $"{RolesEnum.Factory.GetStringValue()},{RolesEnum.Store.GetStringValue()}";
        }
    }
}
