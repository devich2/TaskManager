﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.ProjectMember;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class ProjectMemberRepository : GenericKeyRepository<int, ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(TaskManagerDbContext context) : base(context)
        {
        }
        public async Task<List<ProjectMember>> GetMembersById(int projectId)
        {
            return await Context.ProjectMembers
                .Where(x => x.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<Project>> GetProjectsForUser(int userId)
        {
            return await Context.ProjectMembers
                .Include(x => x.Project)
                    .Where(x => x.UserId == userId)
                        .Select(x=>x.Project)
                .Distinct().ToListAsync();
        }
    }
}
