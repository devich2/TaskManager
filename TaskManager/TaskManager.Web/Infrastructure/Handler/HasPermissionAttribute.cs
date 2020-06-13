using System;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Entities.Enum;

namespace TaskManager.Web.Infrastructure.Handler
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionType permission) : base(permission.ToString())
        { }
    }
}