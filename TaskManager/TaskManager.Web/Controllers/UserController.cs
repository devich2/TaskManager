using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.User;
using TaskManager.Common.Utils;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Result;

namespace TaskManager.Web.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("@me")]
        public async Task<DataResult<UserInfoModel>> GetMe()
        {
            return await _userService.GetUserInfo(User.GetUserId());
        }
    }
}