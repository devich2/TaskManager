using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Bll.Abstract.ProjectMember
{
    public interface IProjectMemberService
    {
        Task<string> GetUserProjectRole(int userId, int projectId);
        Task<bool> IsProjectMember(int projectId, int userId);
        Task<DataResult<List<ProjectMemberDisplayModel>>> GetProjectMembers(int projectId);
        Task<DataResult<ProjectMemberSelectionModel>> GetMembersList(int projectId);
        Task<DataResult<RoleChangeResponse>> ChangeRole(int currentUserId, ProjectMemberRolePatchModel model);
    }
}
