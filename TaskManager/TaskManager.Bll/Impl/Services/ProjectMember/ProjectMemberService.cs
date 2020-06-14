using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using TaskManager.Bll.Abstract.Cache;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.ProjectMember
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleCache _roleCache;
        private readonly RoleManager<Role> _roleManager;

        public ProjectMemberService(IMapper mapper,
            UserManager<Entities.Tables.Identity.User> userManager,
            IUnitOfWork unitOfWork,
            IRoleCache roleCache,
            RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleCache = roleCache;
            _roleManager = roleManager;
        }

        public async Task<string> GetUserProjectRole(int userId, int projectId)
        {
            Entities.Tables.Identity.User user = new Entities.Tables.Identity.User {Id = userId};
            return RetrieveRoleFromClaims(await _userManager.GetClaimsAsync(user));
        }

        public async Task<bool> IsProjectMember(int projectId, int userId)
        {
            var entity = await _unitOfWork.ProjectMembers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);
            return entity != null;
        }
        public async Task<DataResult<RoleChangeResponse>> ChangeRole(int currentUserId, ProjectMemberRolePatchModel model)
        {    
            DataResult<RoleChangeResponse> dataResult = new DataResult<RoleChangeResponse>();
            Role role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            if(role == null)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.NotFound;
                dataResult.MessageDetails = $"Role id-{model.RoleId} not found";
                return dataResult;
            }
            if(currentUserId == model.UserId)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.SelfEditForbidden;
                dataResult.MessageDetails = $"Attempt to self-edit to role-{role.Name}";
                return dataResult;
            }
            Entities.Tables.Identity.User user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if(user == null)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.NotFound;
                dataResult.MessageDetails = $"Candidate for changing role id-{model.UserId} not found";
                return dataResult;
            }
            if(!await IsProjectMember(model.ProjectId, model.UserId))
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.ProjectMemberNotExisting;
                dataResult.MessageDetails = $"User id-{model.UserId} has no access to project id-{model.ProjectId}";
                return dataResult;
            }
            IEnumerable<Claim> userClaims = (await _userManager.GetClaimsAsync(user)).Where(x=>x.UnpackRole().Item1 == model.ProjectId);
            
            await _userManager.RemoveClaimsAsync(user, userClaims);
            await _userManager.AddClaimAsync(user, GenerateRoleClaim(role.Name, model.ProjectId));
            
            dataResult.ResponseStatusType = ResponseStatusType.Succeed;
            dataResult.Data = new RoleChangeResponse()
            {
                RoleId = role.Id,
                UserId = model.UserId,
                RoleName = role.Name
            };
            return dataResult;
        }
        private Claim GenerateRoleClaim(string roleName, int projectId)
        {
            return new Claim(PermissionExtensions.PackedPermissionClaimType, $"{roleName}_{projectId}");
        }
        private string RetrieveRoleFromClaims(IEnumerable<Claim> roleClaims,
            int? projectId = null)
        {
            if (roleClaims == null || !roleClaims.Any())
                return null;

            string[] userRoles = (from c in roleClaims
                let pr = c.UnpackRole()
                where projectId == null || pr.Item1 == projectId
                select pr.Item2).ToArray();

            return userRoles.OrderByDescending(r => _roleCache.GetRankByRoleName(r)).FirstOrDefault();
        }
        public async Task<DataResult<ProjectMemberSelectionModel>> GetMembersList(int projectId)
        {
            DataResult<ProjectMemberSelectionModel> dataResult = new DataResult<ProjectMemberSelectionModel>();

            var memberList = await _unitOfWork.ProjectMembers.GetMembersListByProjectId(projectId);
            if (memberList.Any())
            {
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Data = new ProjectMemberSelectionModel()
                {
                    MemberList = memberList.Select(_mapper.Map<ProjectMemberSelectionItemModel>).ToList()
                };
            }
            else
            {
                dataResult.ResponseStatusType = ResponseStatusType.Warning;
                dataResult.Message = ResponseMessageType.EmptyResult;
            }

            return dataResult;
        }
        public async Task<DataResult<List<ProjectMemberDisplayModel>>> GetProjectMembers(int projectId)
        {
            DataResult<List<ProjectMemberDisplayModel>> dataResult = new DataResult<List<ProjectMemberDisplayModel>>();

            var baseModels = await _unitOfWork.ProjectMembers.GetMembersByProjectId(projectId);
            if (baseModels.Any())
            {
                List<ProjectMemberDisplayModel> displayModels = new List<ProjectMemberDisplayModel>();
                foreach (var pm in baseModels)
                {
                    ProjectMemberDisplayModel memberDisplayModel = _mapper.Map<ProjectMemberDisplayModel>(pm);
                    memberDisplayModel.Role = RetrieveRoleFromClaims(pm.RoleClaims.Select(x => x.ToClaim()));
                    displayModels.Add(memberDisplayModel);
                }

                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Data = displayModels;
            }
            else
            {
                dataResult.ResponseStatusType = ResponseStatusType.Warning;
                dataResult.Message = ResponseMessageType.EmptyResult;
            }

            return dataResult;
        }
    }
}