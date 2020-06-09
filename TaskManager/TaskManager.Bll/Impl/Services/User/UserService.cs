// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using AutoMapper;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using TaskManager.Bll.Abstract.User;
// using TaskManager.Dal.Abstract;
// using TaskManager.Entities.Tables;
// using TaskManager.Entities.Tables.Identity;
// using TaskManager.Models.Response;
// using TaskManager.Models.Role;
// using TaskManager.Models.User;
//
// namespace TaskManager.Bll.Impl.Services.User
// {
//     public class UserService: IUserService
//     {
//         private readonly IMapper _mapper;
//         private readonly UserManager<Entities.Tables.Identity.User> _userManager;
//         private readonly RoleManager<Role> _roleManager;
//         private readonly IUnitOfWork _unitOfWork;
//
//         public UserService(IMapper mapper,
//             UserManager<Entities.Tables.Identity.User> userManager,
//             RoleManager<Role> roleManager,
//             IUnitOfWork unitOfWork)
//         {
//             _mapper = mapper;
//             _userManager = userManager;
//             _roleManager = roleManager;
//             _unitOfWork = unitOfWork;
//         }
//
//         public async Task<DataResult<UserModel>> GetUser(int userId, int projectId)
//         {
//             Entities.Tables.Identity.User user =
//                 await _userManager.FindByIdAsync(userId.ToString());
//
//             UserModel model = _mapper.Map<UserModel>(user);
//
//             ProjectMember pr = await _unitOfWork.ProjectMembers
//                 .FirstOrDefaultAsync(x =>
//                     x.ProjectId == projectId && x.UserId == userId);
//
//             if (pr == null)
//             {
//                 return new DataResult<UserModel>()
//                 {
//                     ResponseStatusType = ResponseStatusType.Error,
//                     Message = ResponseMessageType.NotFound,
//                     MessageDetails = "Not found such a participant"
//                 };
//             }
//             List<Permission> permissions = await _unitOfWork.Permissions
//                 .GetByAsync(x => x.ProjectMemberId == pr.Id);
//
//             List<RoleModel> roles = new List<RoleModel>();
//
//             foreach (var pm in permissions)
//             {
//                 Role role = await _roleManager.Roles.FirstOrDefaultAsync();
//                 roles.Add(_mapper.Map<RoleModel>(role));
//             }
//
//             model.Roles = roles.ToHashSet();
//             return new DataResult<UserModel>()
//             {
//                 ResponseStatusType = ResponseStatusType.Succeed,
//                 Data = model
//             };
//         }
//     }
// }
