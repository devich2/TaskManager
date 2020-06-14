using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Common.Utils;
using TaskManager.Entities.Enum;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/{projectId}/")]
    [ApiController]
    public class ProjectMemberController : ControllerBase
    {
        private readonly IProjectMemberService _projectMemberService;

        public ProjectMemberController(IProjectMemberService projectMemberService)
        {
            _projectMemberService = projectMemberService;
        }
        [HttpGet]
        [Route("project_members")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<List<ProjectMemberDisplayModel>>> DisplayMembers(int projectId)
        {
            return await _projectMemberService.GetProjectMembers(projectId);
        }
        
        [HttpGet]
        [Route("members_list")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<ProjectMemberSelectionModel>> GetMembersList(int projectId)
        {
            return await _projectMemberService.GetMembersList(projectId);
        }
        
        [HttpPut]
        [Route("member/{userId}/")]      
        [HasPermission(PermissionType.RoleChange)]           
        public async Task<DataResult<RoleChangeResponse>> ChangeRole(int projectId, int userId, [FromBody] ProjectMemberRolePatchModel model)
        {
            model.ProjectId = projectId;
            model.UserId = userId;
            return await _projectMemberService.ChangeRole(User.GetUserId(), model);
        }
    }
}