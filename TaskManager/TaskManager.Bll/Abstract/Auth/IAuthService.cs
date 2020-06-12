using TaskManager.Models.Auth;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Bll.Abstract.Auth
{
    public interface IAuthService
    {
        System.Threading.Tasks.Task<DataResult<UserBaseModel>> Login(LoginModel model);
    }
}