using System;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Common.Utils;
using TaskManager.Entities.Enum;

namespace TaskManager.Web.Infrastructure.Handler
{
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName.FindPermissionViaName();
        }

        public PermissionType? PermissionName { get; }
    }
}