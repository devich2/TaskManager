using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.User;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class ProjectMemberRepository : GenericKeyRepository<int, ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<List<ProjectMemberBaseModel>> GetMembersByProjectId(int projectId)
        {
            var list = await (from p in Context.ProjectMembers
                    join u in Context.Users on p.UserId equals u.Id
                    join c in Context.UserClaims
                        on u.Id equals c.UserId
                    where p.ProjectId == projectId && c.ClaimType == PermissionExtensions.PackedPermissionClaimType
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

        public async Task<List<User>> GetMembersListByProjectId(int projectId)
        {
            return await (from pm in Context.ProjectMembers
                join u in Context.Users on pm.UserId equals u.Id
                where pm.ProjectId == projectId
                select u).ToListAsync();
        }
    }
}