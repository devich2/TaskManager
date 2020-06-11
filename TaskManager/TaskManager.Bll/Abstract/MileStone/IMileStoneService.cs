using System.Threading.Tasks;
using TaskManager.Models.MileStone;
using TaskManager.Models.Response;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Abstract.MileStone
{
    public interface IMileStoneService
    {
        System.Threading.Tasks.Task<MileStoneSelectionModel> GetPreviewModel(int unitId);
        Task<DataResult<ChangeMileStoneResponse>> MoveToMileStone(int taskId, int? milestoneId);
    }
}