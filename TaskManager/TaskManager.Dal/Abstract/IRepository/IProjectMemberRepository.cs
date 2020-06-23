using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.User;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IProjectMemberRepository: IGenericKeyRepository<int, ProjectMember>
    {
        Task<List<ProjectMemberBaseModel>> GetMembersByProjectId(int projectId, string searchString);
        Task<List<User>> GetMembersListByProjectId(int? projectId, string searchString);
    }
}