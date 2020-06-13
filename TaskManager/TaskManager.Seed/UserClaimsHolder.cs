using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TaskManager.Common.Utils;
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
                ClaimType = PermissionExtensions.PackedPermissionClaimType,
                ClaimValue = "Maintainer_20"
            },
            new IdentityUserClaim<int>()
            {
                Id = 2,
                UserId = 2,
                ClaimType = PermissionExtensions.PackedPermissionClaimType,
                ClaimValue = "Owner_20"
            },
            new IdentityUserClaim<int>()
            {
                Id = 3,
                UserId = 3,
                ClaimType = PermissionExtensions.PackedPermissionClaimType,
                ClaimValue = "Developer_20"
            }
            
        };

        public List<IdentityUserClaim<int>> GetUserClaims()
        {
            return _userClaims;
        }
    }
}
