using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Common.Utils;
using TaskManager.Entities.Enum;
using TaskManager.Models.MileStone;
using TaskManager.Models.Project;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.TermInfo;
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
        private readonly IUnitEditService _unitEditService;


        public ProjectController(IProjectService projectService, 
        IUnitEditService unitEditService)
        {
            _projectService = projectService;
            _unitEditService = unitEditService;
        }
        
        [HttpGet]
        [Route("{projectId}")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<UnitSelectionModel>> Get(int projectId)
        {
            return await _projectService.GetProjectDetails(projectId, User.GetUserId());
        }
        
        [HttpPost]
        [Authorize]
        public async Task<DataResult<UnitAddResponse>> Post([FromBody] ProjectUnitCreateModel model)
        {
            int userId = User.GetUserId();
            UnitBlModel unitBlModel = new UnitBlModel()
            {
                UserId = userId,
                Name = model.Name,
                Description = model.Description,
                ExtendedType = UnitType.Project,
                TermInfo = new TermInfoCreateModel(){Status = Status.InProgress},
                Data = JObject.FromObject(new ProjectCreateOrUpdateModel(){ProjectManagerId = userId, Members = 1})
            };
            return await _unitEditService.ProcessUnitCreate(unitBlModel);
        }
    }
}