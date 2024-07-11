using Chocolatier.Domain.Enum;
using Chocolatier.Util.EnumUtil;
using Microsoft.AspNetCore.Authorization;

namespace Chocolatier.API.Authorization
{
    public class HeadquarterAuthorization : AuthorizeAttribute
    {
        public HeadquarterAuthorization()
        {
            Roles = RolesEnum.Headquarter.GetStringValue();
        }
    }
}
