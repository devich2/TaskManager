using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Seed
{
    public class UserRolesEntitiesHolder
    {
        private readonly List<UserRole> _userRoles = new List<UserRole>
        {
            new UserRole
            {
                RoleId = 1,
                UserId = 1,
            },
            new UserRole
            {
                RoleId = 2,
                UserId  = 2,
            },
            new UserRole
            {
                RoleId = 2,
                UserId = 1,
            }
        };

        public List<UserRole> GetUserRoles()
        {
            return _userRoles;
        }
    }
}
