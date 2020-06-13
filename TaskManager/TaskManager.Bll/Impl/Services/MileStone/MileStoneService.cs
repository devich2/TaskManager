using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Enum;
using TaskManager.Models.MileStone;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Impl.Services.MileStone
{
    public class MileStoneService: IMileStoneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private JsonSerializerSettings _serializerSettings;

        public MileStoneService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }

        public async Task<MileStonePreviewModel> GetPreviewModel(Entities.Tables.Unit unit)
        {
            List<int> unitIds = unit.MileStone.Tasks.Select(x=>x.UnitId).ToList();
            int countDone = await _unitOfWork.TermInfos.GetByStatusCount(unitIds, Status.Closed);
            int total = unitIds.Count;
            return new MileStonePreviewModel()
            {
                ClosedTasksCount = countDone,
                Total = total,
                CompletedPercentage = (decimal)countDone/total
            };
        }

        public async Task<DataResult<List<MileStoneBaseModel>>> GetActiveListByProjectId(int projectId)
        {
             DataResult<List<MileStoneBaseModel>> dataResult = new DataResult<List<MileStoneBaseModel>>();
             List<MileStoneBaseModel> mileStones = await _unitOfWork.MileStones.GetActiveMilestonesByProjectId(projectId);
             if(mileStones.Any())
             {
                 dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                 dataResult.Data = mileStones;
             }
             else
             {
                 dataResult.ResponseStatusType = ResponseStatusType.Warning;
                 dataResult.Message = ResponseMessageType.EmptyResult;   
             }
             return dataResult;
        }

        public async Task<DataResult<ChangeMileStoneResponse>> MoveToMileStone(TaskMileStonePatchModel patchModel)
        {
            DataResult<ChangeMileStoneResponse> dataResult = new DataResult<ChangeMileStoneResponse>();
            Entities.Tables.Task task = await _unitOfWork.Tasks.FirstOrDefaultAsync(x=>x.Id == patchModel.TaskId);
            if(task == null)
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.InvalidId;
                dataResult.MessageDetails = $"Task id-{patchModel.TaskId} not found";
                return dataResult;
            }
            if(patchModel.MileStoneId.HasValue && ! await _unitOfWork.MileStones.IsExisting(patchModel.MileStoneId.Value))
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.InvalidId;
                dataResult.MessageDetails = $"MileStone id-{patchModel.MileStoneId} not found";
                return dataResult;
            }

            task.MileStoneId = patchModel.MileStoneId;
            await _unitOfWork.Tasks.UpdateAsync(task);
            await _unitOfWork.SaveAsync();
            dataResult.ResponseStatusType = ResponseStatusType.Succeed;
            dataResult.Data = new ChangeMileStoneResponse()
            {
                MileStoneId = task.MileStoneId
            };
            return dataResult;
        }
    }
}