using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Common.Utils;
using TaskManager.Entities.Enum;
using TaskManager.Models.MileStone;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/project/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMileStoneService _mileStoneService;
        private readonly IProjectMemberService _projectMemberService;

        public ProjectController(IProjectService projectService, 
            IMileStoneService mileStoneService, 
            IProjectMemberService projectMemberService)
        {
            _projectService = projectService;
            _mileStoneService = mileStoneService;
            _projectMemberService = projectMemberService;
        }
        
        [HttpGet]
        [Route("{projectId}")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<UnitSelectionModel>> Get(int projectId)
        {
            return await _projectService.GetProjectDetails(projectId, User.GetUserId());
        }
        
        [HttpGet]
        [Route("{projectId}/milestone")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<List<MileStoneBaseModel>>> GetMileStones(int projectId)
        {
            return await _mileStoneService.GetActiveListByProjectId(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/members")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<List<MileStoneBaseModel>>> GetMembers(int projectId)
        {
            return await _mileStoneService.GetActiveListByProjectId(projectId);
        }
    }
}