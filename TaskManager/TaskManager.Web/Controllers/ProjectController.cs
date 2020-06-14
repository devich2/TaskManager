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
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;
using TaskManager.Models.User;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/project/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
       

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
           
        }
        
        [HttpGet]
        [Route("{projectId}")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<UnitSelectionModel>> Get(int projectId)
        {
            return await _projectService.GetProjectDetails(projectId, User.GetUserId());
        }
    }
}