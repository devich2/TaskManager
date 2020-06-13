using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using TaskManager.Bll.Abstract.Permission;

namespace TaskManager.Web.Infrastructure.Handler
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionHandler(IPermissionService permissionService, IHttpContextAccessor httpContextAccessor)
        {
            _permissionService = permissionService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (!requirement.PermissionName.HasValue)
                return Task.CompletedTask;

            var routeData = _httpContextAccessor.HttpContext.GetRouteData();
            string value = routeData.Values["projectId"].ToString();
            if (_permissionService.HasAccess(context.User.Claims, requirement.PermissionName.Value, value))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}