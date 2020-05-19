using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IProjectMemberRepository: IGenericKeyRepository<int, ProjectMember>
    {
        Task<List<Project>> GetProjectsForUser(int userId);
        Task<List<ProjectMember>> GetMembersById(int projectId);
    }
}
