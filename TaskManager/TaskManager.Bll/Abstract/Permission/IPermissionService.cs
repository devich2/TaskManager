using System.Collections.Generic;
using System.Security.Claims;
using TaskManager.Entities.Enum;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Permission
{
    public interface IPermissionService
    {
        public bool HasAccess(IEnumerable<Claim> claims, PermissionType permissionType, string projectId);
        
        public bool HasAccessByTypeAndProcessToState(IEnumerable<Claim> claims, int projectId, UnitType unitType, ModelState state);
    }
}