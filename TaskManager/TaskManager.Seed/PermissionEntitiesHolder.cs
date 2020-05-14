using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Tables;

namespace TaskManager.Seed
{
    public class PermissionEntitiesHolder
    {
        public List<Permission> GetPermissions()
        {
            return new List<Permission>()
            {
                new Permission()
                {
                    ProjectMemberId = 1,
                    RoleId = 2
                },
                new Permission()
                {
                    ProjectMemberId = 10,
                    RoleId = 3
                },
                new Permission()
                {
                    ProjectMemberId = 15,
                    RoleId = 4
                },
            };
        }
    }
}
