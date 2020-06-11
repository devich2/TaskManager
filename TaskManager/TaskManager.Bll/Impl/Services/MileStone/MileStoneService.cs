using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Enum;
using TaskManager.Models.MileStone;
using TaskManager.Models.Response;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services.MileStone
{
    public class MileStoneService: IMileStoneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MileStoneService(IUnitOfWork unitOfWork,
                            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MileStoneSelectionModel> GetPreviewModel(int unitId)
        {
            List<int> unitIds = (await _unitOfWork.MileStones.GetTasksByMileStoneId(unitId)).Select(x=>x.UnitId).ToList();
            int countDone = await _unitOfWork.TermInfos.GetByStatusCount(unitIds, Status.Closed);
            int total = unitIds.Count;
            return new MileStoneSelectionModel()
            {
                ClosedTasksCount = countDone,
                Total = total,
                CompletedPercentage = (decimal)countDone/total
            };
        }
        public async Task<DataResult<ChangeMileStoneResponse>> MoveToMileStone(int taskId, int? milestoneId)
        {
            DataResult<ChangeMileStoneResponse> dataResult = new DataResult<ChangeMileStoneResponse>();
            Entities.Tables.Task task = await _unitOfWork.Tasks.FirstOrDefaultAsync(x=>x.Id == taskId);
            if(task == null)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.InvalidId;
                dataResult.MessageDetails = $"Task id-{taskId} not found";
            }
            else
            {
                task.MileStoneId = milestoneId;
                await _unitOfWork.Tasks.UpdateAsync(task);
                await _unitOfWork.SaveAsync();
                dataResult.Data = new ChangeMileStoneResponse()
                {
                    MileStoneId = milestoneId
                };
            }
            return dataResult;
        }
    }
}