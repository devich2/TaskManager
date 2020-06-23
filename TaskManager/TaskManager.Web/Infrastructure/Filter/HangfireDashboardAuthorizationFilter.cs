using System.Linq;
using System.Security.Claims;
using Hangfire.Dashboard;
using TaskManager.Common.Security;

namespace TaskManager.Web.Infrastructure.Filter
{
    public class HangfireDashboardAuthorizationFilter: IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var userRole  = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            return true;//userRole == RoleNames.WebMaster;
        }
    }
}