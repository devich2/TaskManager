using System.Threading.Tasks;
using TaskManager.Bll.Abstract.User;
using TaskManager.Models.ProjectMember;

namespace TaskManager.Bll.Impl.Services.User
{
    public class UserService: IUserService
    {
        public Task<UserInfoModel> GetUserInfo(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}