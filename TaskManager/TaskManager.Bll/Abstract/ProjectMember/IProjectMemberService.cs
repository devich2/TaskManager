using System.Threading.Tasks;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Bll.Abstract.ProjectMember
{
    public interface IProjectMemberService
    {
        Task<DataResult<UserModel>> GetUserInfo(int userId, int projectId);
    }
}
