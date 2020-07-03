using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.User;
using TaskManager.Models.ProjectMember;

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
        public async Task<UserInfoModel> GetMe()
        {
            return null;
        }
    }
}