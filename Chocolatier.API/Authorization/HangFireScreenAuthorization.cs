using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Chocolatier.API.Authorization
{
    public class HangFireScreenAuthorization : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
