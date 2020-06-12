using System.Threading.Tasks;
using TaskManager.Models.MileStone;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Abstract.MileStone
{
    public interface IMileStoneService
    {
        System.Threading.Tasks.Task<MileStoneSelectionModel> GetPreviewModel(Entities.Tables.Unit unit);
        Task<DataResult<ChangeMileStoneResponse>> MoveToMileStone(TaskMileStonePatchModel model);
    }
}