using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TaskManager.Models.User;

namespace TaskManager.Models.ProjectMember
{
    public class ProjectMemberBaseModel: UserBaseModel
    {
        public ProjectMemberPersonalModel Personal {get;set;}
        public List<IdentityUserClaim<int>> RoleClaims {get;set;}
    }
}