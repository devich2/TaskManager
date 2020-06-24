using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using TaskManager.Bll.Abstract.Cache;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Common.Security;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository.FiltersAndSorting;
using TaskManager.Dal.Abstract.Transactions;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Role;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.ProjectMember
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleCache _roleCache;
        private readonly IPermissionCache _permissionCache;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITransactionManager _transactionManager;
        private readonly IOrderQueryFactory _orderQueryFactory;
        private readonly ILogger<ProjectMemberService> _logger;

        public ProjectMemberService(IMapper mapper,
            UserManager<Entities.Tables.Identity.User> userManager,
            IUnitOfWork unitOfWork,
            IRoleCache roleCache,
            IPermissionCache permissionCache,
            RoleManager<Role> roleManager,
            ITransactionManager transactionManager,
            IOrderQueryFactory orderQueryFactory,
            ILogger<ProjectMemberService> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleCache = roleCache;
            _permissionCache = permissionCache;
            _roleManager = roleManager;
            _transactionManager = transactionManager;
            _orderQueryFactory = orderQueryFactory;
            _logger = logger;
        }

        public async Task<string> GetUserProjectRole(int userId, int projectId)
        {
            Entities.Tables.Identity.User user = new Entities.Tables.Identity.User {Id = userId};
            return RetrieveRoleFromClaims(await _userManager.GetClaimsAsync(user), projectId);
        }
        
        public async Task<bool> IsProjectMember(int projectId, int userId)
        {
            var entity = await _unitOfWork.ProjectMembers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);
            return entity != null;
        }
        
        public async Task<DataResult<List<PermissionType>>> GetUserPermissions(int userId, int projectId)
        {
            string role = await GetUserProjectRole(userId, projectId);
            if(role == null)
            {
                return new DataResult<List<PermissionType>>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.UnitAccessDenied,
                    MessageDetails = $"User id-{userId} has no access to project id-{projectId}"
                };
            }
            List<PermissionType> permissions = _permissionCache.GetFromCache(role);
            if(permissions != null)
            {
                return new DataResult<List<PermissionType>>()
                {
                    ResponseStatusType = ResponseStatusType.Succeed,
                    Data = permissions
                };
            }
            return new DataResult<List<PermissionType>>()
            {
                ResponseStatusType = ResponseStatusType.Error,
                Message = ResponseMessageType.NotFound,
                MessageDetails = $"Role {role} not found"
            };

        }
        public async Task<DataResult<ProjectMemberResponse>> AddToProject(ProjectMemberRoleModel model)
        {
            Role role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            if (role == null)
            {
                return new DataResult<ProjectMemberResponse>()
                {
                    Message = ResponseMessageType.InvalidId,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = $"Role id-{model.RoleId} not found"
                };
            }

            return await _transactionManager.ExecuteInTransactionAsync(async transaction =>
            {
                try
                {
                    await _unitOfWork.ProjectMembers.AddAsync(new Entities.Tables.ProjectMember()
                    {
                        ProjectId = model.ProjectId,
                        UserId = model.UserId,
                        GivenAccess = DateTimeOffset.Now
                    });
                    await _unitOfWork.SaveAsync();

                    DataResult<UserResponse> result = await AddUserRole(model.ProjectId, model.UserId, role.Name);
                    if (result.ResponseStatusType == ResponseStatusType.Succeed)
                    {
                        await transaction.CommitAsync();
                        return new DataResult<ProjectMemberResponse>()
                        {
                            ResponseStatusType = ResponseStatusType.Succeed,
                            Data = new ProjectMemberResponse()
                            {
                                Id = model.UserId,
                                RoleId = role.Id,
                                RoleName = role.Name,
                                ProjectId = model.ProjectId
                            }
                        };
                    }

                    await transaction.RollbackAsync();
                    return new DataResult<ProjectMemberResponse>()
                    {
                        Message = result.Message,
                        ResponseStatusType = result.ResponseStatusType,
                        MessageDetails = result.MessageDetails
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogW(e, "ProjectMemberAddModel: {P}", args:
                        new object[]
                        {
                            AsJsonFormatter.Create(model)
                        });

                    return new DataResult<ProjectMemberResponse>()
                    {
                        Message = ResponseMessageType.InvalidModel,
                        ResponseStatusType = ResponseStatusType.Error
                    };
                }
            });
        }

        public async Task<Result> RemoveFromProject(int projectId, int userId)
        {
            var projectMember = await _unitOfWork.ProjectMembers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);
            if (projectMember == null)
            {
                return new Result()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.ProjectMemberNotExisting,
                    MessageDetails = $"User id-{userId} has no access to project id{projectId}"
                };
            }

            return await _transactionManager.ExecuteInImplicitTransactionAsync(async () =>
            {
                await _unitOfWork.ProjectMembers.DeleteAsync(projectMember);
                await _unitOfWork.SaveAsync();

                Entities.Tables.Identity.User user = await _userManager.FindByIdAsync(userId.ToString());
                IEnumerable<Claim> userClaims =
                    (await _userManager.GetClaimsAsync(user)).Where(x => x.UnpackRole().Item1 == projectId);

                await _userManager.RemoveClaimsAsync(user, userClaims);
                return new Result()
                {
                    ResponseStatusType = ResponseStatusType.Succeed,
                    MessageDetails = $"User id-{userId} left project id-{projectId}"
                };
            });
        }

        public async Task<DataResult<UserResponse>> AddUserRole(int projectId, int userId, string roleName)
        {
            DataResult<UserResponse> dataResult = new DataResult<UserResponse>();
            Entities.Tables.Identity.User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Message = ResponseMessageType.NotFound;
                dataResult.MessageDetails = $"User id-{userId} not found ";
                return dataResult;
            }

            IdentityResult result = await _userManager.AddClaimAsync(user, GenerateRoleClaim(roleName, projectId));
            if (!result.Succeeded)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.InvalidModel;
                dataResult.MessageDetails = string.Join(", ", result.Errors.Select(x => x.Description));
            }
            else
            {
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Data = new UserResponse()
                {
                    Id = userId
                };
            }

            return dataResult;
        }

        public async Task<DataResult<RoleChangeResponse>> ChangeRole(int currentUserId, ProjectMemberRoleModel model)
        {
            DataResult<RoleChangeResponse> dataResult = new DataResult<RoleChangeResponse>();
            Role role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            if (role == null)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.NotFound;
                dataResult.MessageDetails = $"Role id-{model.RoleId} not found";
                return dataResult;
            }

            if (currentUserId == model.UserId)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.SelfEditForbidden;
                dataResult.MessageDetails = $"Attempt to self-edit to role-{role.Name}";
                return dataResult;
            }

            Entities.Tables.Identity.User user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.NotFound;
                dataResult.MessageDetails = $"Candidate for changing role id-{model.UserId} not found";
                return dataResult;
            }

            if (!await IsProjectMember(model.ProjectId, model.UserId))
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.ProjectMemberNotExisting;
                dataResult.MessageDetails = $"User id-{model.UserId} has no access to project id-{model.ProjectId}";
                return dataResult;
            }

            return await _transactionManager.ExecuteInTransactionAsync(async transaction =>
            {
                try
                {
                    IEnumerable<Claim> userClaims =
                        (await _userManager.GetClaimsAsync(user)).Where(x => x.UnpackRole().Item1 == model.ProjectId);

                    if ((await _userManager.RemoveClaimsAsync(user, userClaims)).Succeeded &&
                        (await _userManager.AddClaimAsync(user, GenerateRoleClaim(role.Name, model.ProjectId)))
                        .Succeeded)
                    {
                        await transaction.CommitAsync();
                        return new DataResult<RoleChangeResponse>()
                        {
                            Data = new RoleChangeResponse()
                            {
                                RoleId = role.Id,
                                UserId = model.UserId,
                                RoleName = role.Name
                            },
                            ResponseStatusType = ResponseStatusType.Succeed
                        };
                    }

                    await transaction.RollbackAsync();
                    dataResult.ResponseStatusType = ResponseStatusType.Error;
                    dataResult.Message = ResponseMessageType.InvalidModel;
                    return dataResult;
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogW(e, "ProjectMemberRoleModel: {P}", args:
                        new object[]
                        {
                            AsJsonFormatter.Create(model)
                        });

                    return new DataResult<RoleChangeResponse>()
                    {
                        Message = ResponseMessageType.InvalidModel,
                        ResponseStatusType = ResponseStatusType.Error
                    };
                }
            });
        }

        private Claim GenerateRoleClaim(string roleName, int projectId)
        {
            return new Claim(PermissionExtensions.PackedPermissionClaimType, $"{roleName}_{projectId}");
        }

        private string RetrieveRoleFromClaims(IEnumerable<Claim> roleClaims,
            int projectId)
        {
            if (roleClaims == null || !roleClaims.Any())
                return null;

            var userRoles = (from c in roleClaims
                let pr = c.UnpackRole()
                where pr.Item1 == projectId
                select pr.Item2).ToArray();

            return userRoles.OrderByDescending(r => _roleCache.GetRankByRoleName(r)).FirstOrDefault();
        }

        private Tuple<string, decimal> RetrieveRoleAndRankFromClaims(IEnumerable<Claim> roleClaims, int projectId)
        {
            if (roleClaims == null || !roleClaims.Any())
                return null;

            var userRoles = from c in roleClaims
                let pr = c.UnpackRole()
                where pr.Item1 == projectId
                select new Tuple<string, decimal>(pr.Item2, _roleCache.GetRankByRoleName(pr.Item2));

            return userRoles.OrderByDescending(r => r.Item2).FirstOrDefault();
        }

        public async Task<DataResult<ProjectMemberSelectionModel>> GetMembersList(int projectId, string searchString)
        {
            DataResult<ProjectMemberSelectionModel> dataResult = new DataResult<ProjectMemberSelectionModel>();

            var memberList = await _unitOfWork.ProjectMembers.GetMembersListByProjectId(projectId, searchString);
            if (memberList.Any())
            {
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Data = new ProjectMemberSelectionModel()
                {
                    MemberList = memberList.Select(_mapper.Map<UserInfoModel>).ToList()
                };
            }
            else
            {
                dataResult.ResponseStatusType = ResponseStatusType.Warning;
                dataResult.Message = ResponseMessageType.EmptyResult;
            }

            return dataResult;
        }

        public async Task<List<ProjectMemberDisplayModel>> GetProjectMembers(int projectId,
            SortingOptions sortingOptions, string searchString)
        {
            List<Tuple<Func<ProjectMemberDisplayModel, object>, SortingType>> sortingExpression = null;
            DataResult<List<ProjectMemberDisplayModel>> dataResult = new DataResult<List<ProjectMemberDisplayModel>>();

            var baseModels = await _unitOfWork.ProjectMembers.GetMembersByProjectId(projectId, searchString);
            List<ProjectMemberDisplayModel> displayModels = new List<ProjectMemberDisplayModel>();
            if (baseModels.Any())
            {
                foreach (var pm in baseModels)
                {
                    ProjectMemberDisplayModel memberDisplayModel = _mapper.Map<ProjectMemberDisplayModel>(pm);

                    Tuple<string, decimal> roleRank =
                        RetrieveRoleAndRankFromClaims(pm.RoleClaims.Select(x => x.ToClaim()), projectId);

                    memberDisplayModel.Role = new RoleRankModel()
                    {
                        Rank = roleRank.Item2,
                        Name = roleRank.Item1
                    };
                    displayModels.Add(memberDisplayModel);
                }

                if (sortingOptions != null &&
                    sortingOptions.Sortings.Any())
                {
                    sortingExpression = sortingOptions.Sortings
                        .Select(sortItem => 
                            _orderQueryFactory.GetProjectMemberOrderQuery(sortItem)).ToList();
                }
                if (sortingExpression != null &&
                    sortingExpression.Any())
                {
                    foreach (var (item1, item2) in sortingExpression)
                    {
                        switch (item2)
                        {
                            case SortingType.Asc:
                                displayModels = displayModels.OrderBy(item1).ToList();
                                break;
                            case SortingType.Desc:
                                displayModels = displayModels.OrderByDescending(item1).ToList();
                                break;
                        }
                    }
                }

                dataResult.Data = displayModels;
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
            }
            return displayModels;
        }
    }
}