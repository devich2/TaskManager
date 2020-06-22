using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Common.Utils;
using TaskManager.Configuration;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.Pagination;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;
using TaskManager.Web.Infrastructure.Extension;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/{projectId}/")]
    [ApiController]
    public class ProjectMemberController : ControllerBase
    {
        private readonly IProjectMemberService _projectMemberService;
        private readonly PaginationConfiguration _paginationConfiguration;

        public ProjectMemberController(IProjectMemberService projectMemberService, 
            IOptions<PaginationConfiguration> options)
        {
            _projectMemberService = projectMemberService;
            _paginationConfiguration = options.Value;
        }

        [HttpGet]
        [Route("project_members")]
        [HasPermission(PermissionType.Read)]
        public async Task<DataResult<GenericPaginatedModel<ProjectMemberDisplayModel>>> DisplayMembers(int projectId,
            string sortingQuery, string searchString, int? page)
        {
            DataResult<SortingOptions> sortingOptionsDataResult =
                UnitOrderExtractor.ExtractSortingOptionsDataResult(sortingQuery);
            int pageSize = _paginationConfiguration.PageSize;
            if (sortingOptionsDataResult.ResponseStatusType ==
                ResponseStatusType.Succeed)
            {
                List<ProjectMemberDisplayModel> displayModels = await _projectMemberService.GetProjectMembers(projectId, sortingOptionsDataResult.Data, searchString);
                return new DataResult<GenericPaginatedModel<ProjectMemberDisplayModel>>()
                {
                    ResponseStatusType = displayModels.Any() ? ResponseStatusType.Succeed : ResponseStatusType.Warning,
                    Data = new GenericPaginatedModel<ProjectMemberDisplayModel>()
                    {
                        Models = displayModels.Skip(((page ?? 1)-1)*pageSize).Take(pageSize),
                        PaginationModel = new PaginationModel(displayModels.Count, page ?? 1, pageSize)
                    }
                };
                
            }
            
            return new DataResult<GenericPaginatedModel<ProjectMemberDisplayModel>>()
            {
                Message = sortingOptionsDataResult.Message,
                ResponseStatusType = sortingOptionsDataResult.ResponseStatusType,
                MessageDetails = sortingOptionsDataResult.MessageDetails
            };
        }

        [HttpGet]
        [Route("members_list")]
        [HasPermission(PermissionType.Read)]
        public async Task<DataResult<ProjectMemberSelectionModel>> GetMembersList(int projectId)
        {
            return await _projectMemberService.GetMembersList(projectId, null);
        }

        [HttpPost]
        [Route("leave")]
        [Authorize]
        public async Task<Result> LeaveProject(int projectId)
        {
            return await _projectMemberService.RemoveFromProject(projectId, User.GetUserId());
        }


        [HttpPost]
        [Route("member_add/{userId}")]
        [HasPermission(PermissionType.UserInvite)]
        public async Task<DataResult<ProjectMemberResponse>> AddProjectMember(int projectId, int userId,
            [FromBody] ProjectMemberRoleModel model)
        {
            if (await _projectMemberService.IsProjectMember(projectId, userId))
            {
                return new DataResult<ProjectMemberResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.AlreadyInProject,
                    MessageDetails = $"User id-{userId} already has access to project id-{projectId}"
                };
            }

            model.ProjectId = projectId;
            model.UserId = userId;
            return await _projectMemberService.AddToProject(model);
        }


        [HttpPut]
        [Route("member_role/{userId}")]
        [HasPermission(PermissionType.RoleChange)]
        public async Task<DataResult<RoleChangeResponse>> ChangeRole(int projectId, int userId,
            [FromBody] ProjectMemberRoleModel model)
        {
            model.ProjectId = projectId;
            model.UserId = userId;
            return await _projectMemberService.ChangeRole(User.GetUserId(), model);
        }

        [HttpDelete]
        [Route("member_kick/{userId}")]
        [HasPermission(PermissionType.UserKick)]
        public async Task<Result> LeaveProject(int projectId, int userId)
        {
            if (User.GetUserId() == userId)
            {
                return new Result()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.SelfEditForbidden,
                };
            }

            return await _projectMemberService.RemoveFromProject(projectId, userId);
        }
    }
}