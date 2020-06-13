using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Bll.Abstract.ProjectMember
{
    public interface IProjectMemberService
    {
        Task<string> GetRole(int userId, int projectId);
        Task<bool> IsProjectMember(int projectId, int userId);
    }
}
