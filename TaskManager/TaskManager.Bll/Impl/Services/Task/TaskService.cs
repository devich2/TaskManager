using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Bll.Impl.Services.Unit;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectMemberService _projectMemberService;
        private readonly JsonSerializerSettings _serializerSettings;

        public TaskService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IProjectMemberService projectMemberService,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _projectMemberService = projectMemberService;
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }

        public async System.Threading.Tasks.Task<DataResult<UnitSelectionModel>> GetTaskDetails(int unitId)
        {
            Entities.Tables.Unit unit =
                await _unitOfWork.Units.SelectExpandedByUnitIdAndType(UnitType.Task, unitId);

            if (unit == null)
            {
                return new DataResult<UnitSelectionModel>()
                {
                    Message = ResponseMessageType.NotFound,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "Task not found"
                };
            }

            UnitSelectionModel model = _mapper.Map<UnitSelectionModel>(unit);

            TaskPreviewModel previewModel = await GetPreviewModel(unit);
            TaskDetailsModel taskModel = _mapper.Map<TaskDetailsModel>(previewModel);

            List<Entities.Tables.Unit> subUnits =
                await _unitOfWork.Units.GetByAsync(x => x.UnitParentId == unitId);

            List<SubUnitModel> subModels = new List<SubUnitModel>();

            foreach (var item in subUnits)
            {
                SubUnitModel subUnit = _mapper.Map<SubUnitModel>(item);

                if (item.UnitType == UnitType.Comment)
                    if (unit.UnitParentId != null)
                        subUnit.Creator.Role =
                            await _projectMemberService.GetUserProjectRole(item.CreatorId, unit.UnitParentId.Value);

                subModels.Add(subUnit);
            }

            taskModel.SubUnits = subModels;
            model.Data = JObject.FromObject(taskModel, JsonSerializer.Create(_serializerSettings));

            return new DataResult<UnitSelectionModel>()
            {
                Data = model,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }

        public async System.Threading.Tasks.Task<TaskPreviewModel> GetPreviewModel(Entities.Tables.Unit unit)
        {
            var children = unit.Children ?? new List<Entities.Tables.Unit>();
            TaskPreviewModel taskModel = new TaskPreviewModel
            {
                Id = unit.Task.Id,
                MileStoneId = unit.Task.MileStoneId,
                SubTasksTotal = children.Count(x => x.UnitType == UnitType.SubTask),
                SubTasksCompleted = children.Count(x => x.TermInfo.Status == Status.Closed),
                Comments = children.Count(x => x.UnitType == UnitType.Comment),
            };

            if (unit.Task.AssignedId.HasValue)
            {
                taskModel.Assignee = _mapper.Map<UserBaseModel>(unit.Task.Assigned);
            }

            int? mileId = unit.Task.MileStoneId;
            if (mileId.HasValue)
                taskModel.MileStone =
                    (await _unitOfWork.MileStones.GetByIdAsync(mileId.Value)).Unit.Name;

            taskModel.Tags = await _unitOfWork.TagOnTasks.GetTagsByTaskId(unit.Task.Id);
            return taskModel;
        }

        public async System.Threading.Tasks.Task<DataResult<ChangeAssigneeResponse>> ChangeAssignee(
            TaskAssigneePatchModel patchModel)
        {
            DataResult<ChangeAssigneeResponse> dataResult = new DataResult<ChangeAssigneeResponse>();
            Entities.Tables.Task task = await _unitOfWork.Tasks.GetByIdAsync(patchModel.TaskId);
            if (task == null)
            {
                dataResult.Message = ResponseMessageType.InvalidId;
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.MessageDetails = $"Task id-{patchModel.TaskId} not found";
            }
            else if (patchModel.AssigneeId != null &&
                     (task.Unit.UnitParentId != null &&
                      !await _projectMemberService.IsProjectMember(task.Unit.UnitParentId.Value,
                          patchModel.AssigneeId.Value)))
            {
                dataResult.Message = ResponseMessageType.UnitAccessDenied;
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.MessageDetails =
                    $"User id-{patchModel.AssigneeId} has no access to project id-{task.Unit.UnitParentId}";
            }
            else
            {
                task.AssignedId = patchModel.AssigneeId;
                await _unitOfWork.Tasks.UpdateAsync(task);
                
                TermInfo termInfo = await _unitOfWork.TermInfos.GetByUnitIdAsync(task.UnitId);
                termInfo.Status = Status.InProgress;
                await _unitOfWork.TermInfos.UpdateAsync(termInfo);
                await _unitOfWork.SaveAsync();
                
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Message = ResponseMessageType.None;
                dataResult.Data = new ChangeAssigneeResponse()
                {
                    AssigneeId = task.AssignedId
                };
            }

            return dataResult;
        }
    }
}