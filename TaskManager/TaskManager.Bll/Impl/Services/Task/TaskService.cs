using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Bll.Impl.Services.Unit;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Enum;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.Task
{
    public class TaskService: ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectMemberService _projectMemberService;

        public TaskService(IMapper mapper, 
            IUnitOfWork unitOfWork,
            IProjectMemberService projectMemberService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _projectMemberService = projectMemberService;
        }

        public async System.Threading.Tasks.Task<DataResult<UnitSelectionModel>> GetTaskDetails(int unitId)
        {
            Entities.Tables.Task entityTask = await _unitOfWork.Tasks.GetByUnitIdAsync(unitId);
            if (entityTask == null)
            {
                return new DataResult<UnitSelectionModel>()
                {
                    Message = ResponseMessageType.NotFound,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "Task not found"
                };
            }
            UnitSelectionModel model = _mapper.Map<UnitSelectionModel>(entityTask.Unit);
            int projectId = entityTask.Unit.UnitParentId.Value;
            model.Creator = _mapper.Map<UserModel>(
                (await _projectMemberService.GetUserInfo(entityTask.Unit.CreatorId, projectId)).Data);
            
            List<Entities.Tables.Unit> subUnits = 
                await _unitOfWork.Units.GetByAsync(x=>x.UnitParentId == entityTask.UnitId);
            
            List<SubUnitModel> subModels = new List<SubUnitModel>();
            foreach (var unit in subUnits)
            {
                SubUnitModel unitModel = _mapper.Map<SubUnitModel>(unit);
                unitModel.Creator = (await _projectMemberService.GetUserInfo(unit.CreatorId, projectId)).Data;
                subModels.Add(unitModel);
            }
            TaskPreviewModel previewModel = await GetPreviewModel(unitId);
            TaskDetailsModel taskModel = _mapper.Map<TaskDetailsModel>(previewModel);
            taskModel.SubUnits = subModels;
            model.Data = JObject.FromObject(taskModel);
            return new DataResult<UnitSelectionModel>()
            {
                Data = model,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }
        
        public async System.Threading.Tasks.Task<TaskPreviewModel> GetPreviewModel(int unitId)
        {
            Entities.Tables.Task entityTask = await _unitOfWork.Tasks.GetByUnitIdAsync(unitId);
            var children = entityTask.Unit.Children;
            TaskPreviewModel taskModel = new TaskPreviewModel
            {
                Id = entityTask.Id,
                MileStoneId = entityTask.MileStoneId,
                SubTasksTotal = children.Count(x => x.UnitType == UnitType.SubTask),
                SubTasksCompleted = children.Count(x => x.TermInfo.Status == Status.Closed),
                Comments = children.Count(x => x.UnitType == UnitType.Comment),
            };

            int projectId = entityTask.Unit.UnitParentId.Value;
            UserModel assignee = entityTask.AssignedId == null
                ? null
                : (await _projectMemberService.GetUserInfo(entityTask.AssignedId.Value, projectId)).Data;
            
            taskModel.Assignee = assignee;
            if (entityTask.MileStoneId != null)
                taskModel.MileStone =
                    (await _unitOfWork.MileStones.GetByIdAsync(entityTask.MileStoneId.Value)).Unit.Name;
            
            taskModel.Tags = await _unitOfWork.TagOnTasks.GetTagsByTaskId(entityTask.Id);
            return taskModel;
        }
        
        public async System.Threading.Tasks.Task<DataResult<ChangeAssigneeResponse>> ChangeAssignee(TaskAssigneePatchModel patchModel)
        {
            DataResult<ChangeAssigneeResponse> dataResult = new DataResult<ChangeAssigneeResponse>();
            Entities.Tables.Task task = await _unitOfWork.Tasks.GetByIdAsync(patchModel.TaskId);
            if(task == null)
            {
                dataResult.Message = ResponseMessageType.InvalidId;
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.MessageDetails = $"Task id-{patchModel.TaskId} not found";
            }
            else
            {
                task.AssignedId = patchModel.AssigneeId;
                await _unitOfWork.Tasks.UpdateAsync(task);
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
