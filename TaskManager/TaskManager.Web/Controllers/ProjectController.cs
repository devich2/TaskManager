using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Common.Utils;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;

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
        [Route("{unitId}")]                 
        public async Task<DataResult<UnitSelectionModel>> Get(int unitId)
        {
            return await _projectService.GetProjectDetails(unitId, 1);
        }
    }
}