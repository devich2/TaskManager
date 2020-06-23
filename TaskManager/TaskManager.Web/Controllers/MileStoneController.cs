using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Entities.Enum;
using TaskManager.Models.MileStone;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/{projectId}/milestone/")]
    [ApiController]
    public class MileStoneController : ControllerBase
    {
        private readonly IMileStoneService _mileStoneService;

        public MileStoneController(IMileStoneService mileStoneService)
        {
            _mileStoneService = mileStoneService;
        }
        
        [HttpGet]
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<List<MileStoneBaseModel>>> GetMileStoneList(int projectId)
        {
            return await _mileStoneService.GetActiveListByProjectId(projectId);
        }
    }
}