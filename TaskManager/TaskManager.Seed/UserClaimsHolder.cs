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
                ClaimValue = "Maintainer_20"
            },
            new IdentityUserClaim<int>()
            {
                Id = 2,
                UserId = 2,
                ClaimType = "role",
                ClaimValue = "Owner_20"
            },
            new IdentityUserClaim<int>()
            {
                Id = 3,
                UserId = 3,
                ClaimType = "role",
                ClaimValue = "Developer_20"
            }
            
        };

        public List<IdentityUserClaim<int>> GetUserClaims()
        {
            return _userClaims;
        }
    }
}
