using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Bll.Abstract.User;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Response;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.User
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper,
            UserManager<Entities.Tables.Identity.User> userManager,
            RoleManager<Role> roleManager,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResult<UserModel>> GetUser(int userId, int projectId)
        {
            Entities.Tables.Identity.User user =
                await _userManager.FindByIdAsync(userId.ToString());
            
            if (user == null)
            {
                return new DataResult<UserModel>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.NotFound,
                    MessageDetails = $"user with id-{userId} is missing..."
                };
            }
            UserModel model = _mapper.Map<UserModel>(user);
            
            var claims = await _userManager.GetClaimsAsync(user);
            string[] userRoles = (from c in claims
                                    let arr = c.Value.Split("_", StringSplitOptions.RemoveEmptyEntries)
                                    where int.Parse(arr[1]) == projectId
                                    select arr[0]).ToArray();
                
            List<Role> rawRoles = _roleManager.Roles.Where(x=>userRoles.Contains(x.Name)).ToList();
            model.Role = rawRoles.OrderByDescending(i => i.Rank).FirstOrDefault()?.Name;
            return new DataResult<UserModel>()
            {
                ResponseStatusType = ResponseStatusType.Succeed,
                Data = model
            };
        }
    }
}
