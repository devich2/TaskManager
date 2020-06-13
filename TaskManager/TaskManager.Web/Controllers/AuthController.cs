using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.Auth;
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
        
        [Route("register")]
        [HttpPost]
        public async Task<Result> Login(RegisterModel model)
        {
            return await _authService.Register(model);
        }
    }
}