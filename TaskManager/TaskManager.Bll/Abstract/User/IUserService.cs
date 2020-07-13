using System.Threading.Tasks;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Abstract.User
{
    public interface IUserService
    {
        Task<DataResult<UserInfoModel>> GetUserInfo(int userId);
    }
}