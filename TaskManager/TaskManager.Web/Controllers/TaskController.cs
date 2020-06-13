using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Enum;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/{projectId}/task/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMileStoneService _mileStoneService;

        public TaskController(ITaskService taskService,
            IMileStoneService mileStoneService)
        {
            _taskService = taskService;
            _mileStoneService = mileStoneService;
        }

        [HttpGet]
        [Route("{unitId}")]
        [HasPermission(PermissionType.Read)]
        public async Task<DataResult<UnitSelectionModel>> Get(int unitId)
        {
            return await _taskService.GetTaskDetails(unitId);
        }

        [HttpPut]
        [Route("{taskId}/assignee")]
        public async Task<DataResult<ChangeAssigneeResponse>> Put(int taskId,
            [FromBody] TaskAssigneePatchModel model)
        {
            model.TaskId = taskId;
            return await _taskService.ChangeAssignee(model);
        }

        [HttpPut]
        [Route("{taskId}/milestone")]
        public async Task<DataResult<ChangeMileStoneResponse>> Put(int taskId, 
            [FromBody] TaskMileStonePatchModel model)
        {
            model.TaskId = taskId;
            return await _mileStoneService.MoveToMileStone(model);
        }
    }
}