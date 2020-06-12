using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Models.Response;
using TaskManager.Models.Result;

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
    }
}