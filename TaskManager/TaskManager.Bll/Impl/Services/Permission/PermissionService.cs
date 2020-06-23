using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TaskManager.Bll.Abstract.Cache;
using TaskManager.Bll.Abstract.Permission;
using TaskManager.Common.Security;
using TaskManager.Common.Utils;
using TaskManager.Entities.Enum;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Impl.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionCache _permissionCache;

        public PermissionService(IPermissionCache permissionCache)
        {
            _permissionCache = permissionCache;
        }

        public bool HasAccess(IEnumerable<Claim> claims, PermissionType permissionType, string id)
        {
            return int.TryParse(id, out int projectId) && IsAuthorized(claims, permissionType, projectId);
        }

        public bool HasAccessByTypeAndProcessToState(IEnumerable<Claim> claims, int projectId, UnitType unitType, ModelState state)
        {
            string operationKey = unitType.ToString() + state;
            PermissionType? permissionType = operationKey.FindPermissionViaName();
            return permissionType.HasValue && IsAuthorized(claims, permissionType.Value, projectId);
        }
        
        private bool IsAuthorized(IEnumerable<Claim> claims, PermissionType permissionType, int projectId)
        {
            var roleClaims = claims.Where(x => x.Type == PermissionExtensions.PackedPermissionClaimType).ToList();
            if (!roleClaims.Any())
                return false;

            var allowedRoles = _permissionCache.GetFromCache(permissionType);
            return roleClaims.Select(x => x.UnpackRole())
                .Any(x => x.Item1 == projectId && allowedRoles.Contains(x.Item2));
        }
    }
}