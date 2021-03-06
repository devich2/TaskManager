﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Security;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.User;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class ProjectMemberRepository : GenericKeyRepository<int, ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(TaskManagerDbContext context) : base(context)
        {
        }
        
        public async Task<List<ProjectMemberBaseModel>> GetMembersByProjectId(int projectId, string searchString)
        {
            var list = await (from p in Context.ProjectMembers
                    join u in Context.Users on p.UserId equals u.Id
                    join c in Context.UserClaims
                        on u.Id equals c.UserId
                    where p.ProjectId == projectId &&
                          c.ClaimType == PermissionExtensions.PackedPermissionClaimType &&
                          (searchString == null || u.Name.Contains(searchString))
                    select new {p.UserId, p.GivenAccess, u.Name, u.Email, u.UserName, u.LastLoginDate, RoleClaim = c})
                .ToListAsync();

            return list.GroupBy(x => x.UserId)
                .Select(x =>
                {
                    var first = x.First();
                    return new ProjectMemberBaseModel()
                    {
                        Id = first.UserId,
                        Name = first.Name,
                        Email = first.Email,
                        UserName = first.UserName,
                        Personal = new ProjectMemberPersonalModel()
                            {GiveAccess = first.GivenAccess, LastLoginDate = first.LastLoginDate},
                        RoleClaims = x.Select(r => r.RoleClaim).ToList()
                    };
                }).ToList();
        }

        public async Task<List<User>> GetMembersListByProjectId(int? projectId, string searchString)
        {
            return await (from pm in Context.ProjectMembers
                join u in Context.Users on pm.UserId equals u.Id
                where (projectId == null || pm.ProjectId == projectId) &&
                    (searchString == null || u.UserName.Contains(searchString) || u.Name.Contains(searchString))
                select u).ToListAsync();
        }
    }
}