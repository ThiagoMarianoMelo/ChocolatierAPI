using Chocolatier.Domain.Enum;
using Chocolatier.Util.EnumUtil;
using Microsoft.AspNetCore.Authorization;

namespace Chocolatier.API.Authorization
{
    public class FactoryAuthorization : AuthorizeAttribute
    {
        public FactoryAuthorization()
        {
            Roles = RolesEnum.Factory.GetStringValue();
        }
    }
}
