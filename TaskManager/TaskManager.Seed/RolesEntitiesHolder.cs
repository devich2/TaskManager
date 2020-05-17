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
                Rank = 0,
                Name = "Guest",
                NormalizedName = "GUEST",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
            },
            new Role
            {
                Id = 2,
                Rank = 1,
                Name = "Developer",
                NormalizedName = "DEVELOPER",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
            },
            new Role
            {
                Id = 3,
                Rank = 2,
                Name = "Maintainer",
                NormalizedName = "MAINTAINER",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78406aefd74b",
            },
            new Role
            {
                Id = 4,
                Rank = 3,
                Name = "Owner",
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
