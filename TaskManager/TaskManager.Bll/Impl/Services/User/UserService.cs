using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManager.Bll.Abstract.User;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<Entities.Tables.Identity.User> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<DataResult<UserInfoModel>> GetUserInfo(int userId)
        {
            Entities.Tables.Identity.User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new DataResult<UserInfoModel>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.NotFound
                };
            }
            var mapped = _mapper.Map<UserInfoModel>(user);
            return new DataResult<UserInfoModel>()
            {
                ResponseStatusType = ResponseStatusType.Succeed,
                Data = mapped
            };
        }
    }
}