using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models.MileStone;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Abstract.MileStone
{
    public interface IMileStoneService
    {
        Task<MileStonePreviewModel> GetPreviewModel(Entities.Tables.Unit unit);
        Task<DataResult<List<MileStoneBaseModel>>> GetActiveListByProjectId(int projectId);
        Task<DataResult<ChangeMileStoneResponse>> MoveToMileStone(TaskMileStonePatchModel model);
    }
}