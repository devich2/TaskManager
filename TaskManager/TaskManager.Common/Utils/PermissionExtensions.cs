using System;
using System.Security.Claims;
using TaskManager.Entities.Enum;

namespace TaskManager.Common.Utils
{
    public static class PermissionExtensions
    {
        public const string PackedPermissionClaimType = "role";

        public static Tuple<int, string> UnpackRole(this Claim claim)
        {
            string[] projectRole = claim.Value.Split("_", StringSplitOptions.RemoveEmptyEntries);
            if (projectRole.Length != 2 || !int.TryParse(projectRole[1], out int projectId))
                return null;
            return new Tuple<int, string>(projectId, projectRole[0]);
        }

        public static PermissionType? FindPermissionViaName(this string permissionName)
        {
            return Enum.TryParse(permissionName, out PermissionType permission)
                ? (PermissionType?) permission
                : null;
        }
    }
}