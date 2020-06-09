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
                Name = "Guest",
                Rank = 1,
                NormalizedName = "GUEST",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
            },
            new Role
            {
                Id = 2,
                Name = "Developer",
                Rank = 2,
                NormalizedName = "DEVELOPER",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
            },
            new Role
            {
                Id = 3,
                Name = "Maintainer",
                Rank = 3,
                NormalizedName = "MAINTAINER",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78406aefd74b",
            },
            new Role
            {
                Id = 4,
                Name = "Owner",
                Rank = 4,
                NormalizedName = "OWNER",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78406aefd74b",
            }
        };

        public List<Role> GetRoles()
        {
            return _roles;
        }
    }
}
