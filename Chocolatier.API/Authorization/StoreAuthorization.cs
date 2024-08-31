using Chocolatier.Domain.Enum;
using Chocolatier.Util.EnumUtil;
using Microsoft.AspNetCore.Authorization;

namespace Chocolatier.API.Authorization
{
    public class StoreAuthorization : AuthorizeAttribute
    {
        public StoreAuthorization()
        {
            Roles = RolesEnum.Store.GetStringValue();
        }
    }
}
