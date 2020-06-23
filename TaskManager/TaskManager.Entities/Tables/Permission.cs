using System.Security.Permissions;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class Permission
    {
        public int Id {get;set;}
        public int RoleId {get;set;}
        public Role Role {get; set;}
        public PermissionType PermissionType {get; set;}
    }
}