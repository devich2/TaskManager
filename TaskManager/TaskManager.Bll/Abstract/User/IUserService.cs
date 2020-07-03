using TaskManager.Models.ProjectMember;

namespace TaskManager.Bll.Abstract.User
{
    public interface IUserService
    {
        System.Threading.Tasks.Task<UserInfoModel> GetUserInfo(int userId);
    }
}