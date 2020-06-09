using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Seed
{
    public class UserClaimsHolder
    {
        private readonly List<IdentityUserClaim<int>> _userClaims = new List<IdentityUserClaim<int>>()
        {
            new IdentityUserClaim<int>()
            {
                Id = 1,
                UserId = 1,
                ClaimType = "role",
                ClaimValue = "Admin_1"
            },
            new IdentityUserClaim<int>()
            {
                Id = 2,
                UserId = 1,
                ClaimType = "role",
                ClaimValue = "Guest_1"
            },
            new IdentityUserClaim<int>()
            {
                Id = 3,
                UserId = 1,
                ClaimType = "role",
                ClaimValue = "Maintainer_1"
            }
        };

        public List<IdentityUserClaim<int>> GetUserClaims()
        {
            return _userClaims;
        }
    }
}
