using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Seed
{
    public class RolesEntitiesHolder
    {
        private readonly List<Role> _roles = new List<Role>
        {
            new Role
            {
                Id = 1,
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
            },
            new Role
            {
                Id = 2,
                Name = "Developer",
                NormalizedName = "DEVELOPER",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
            },
            new Role
            {
                Id = 3,
                Name = "Guest",
                NormalizedName = "GUEST",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78406aefd74b",
            }
        };

        public List<Role> GetRoles()
        {
            return _roles;
        }
    }
}
