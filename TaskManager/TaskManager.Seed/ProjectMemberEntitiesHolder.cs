using System;
using System.Collections.Generic;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Seed
{
    public class ProjectMemberEntitiesHolder
    {
        private readonly List<ProjectMember> _projectMembers = new List<ProjectMember>
        {
            new ProjectMember()
            {
                UserId = 1,
                ProjectId = 20,
                GivenAccess = DateTimeOffset.Now
            },
            new ProjectMember()
            {
                UserId = 2,
                ProjectId = 20,
                GivenAccess = DateTimeOffset.Now
            },
            new ProjectMember()
            {
                UserId = 3,
                ProjectId = 20,
                GivenAccess = DateTimeOffset.Now
            }
        };

        public List<ProjectMember> GetProjectMembers()
        {
            return _projectMembers;
        }
    }
}