using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.Auth;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Auth;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Web.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Route("login")]
        [HttpPost]
        public async Task<DataResult<UserBaseModel>> Login(LoginModel model)
        {
            return await _authService.Login(model);
        }
        [Route("sign_out")]
        [HttpPost]
        public async Task SignOut()
        {
            await _authService.SignOut();
        }
        [Route("register")]
        [HttpPost]
        public async Task<Result> Login(RegisterModel model)
        {
            return await _authService.Register(model);
        }
    }
}