// using System;
// using System.Collections.Generic;
// using System.Text;
// using AutoMapper;
// using Newtonsoft.Json.Linq;
// using TaskManager.Bll.Abstract.Task;
// using TaskManager.Bll.Abstract.User;
// using TaskManager.Bll.Impl.Services.Unit;
// using TaskManager.Dal.Abstract;
// using TaskManager.Models.RelationShip;
// using TaskManager.Models.Response;
// using TaskManager.Models.Task;
// using TaskManager.Models.Unit;
// using TaskManager.Models.User;
//
// namespace TaskManager.Bll.Impl.Services.Task
// {
//     public class TaskService: ITaskService
//     {
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly IUserService _userService;
//
//         public TaskService(IMapper mapper, 
//             IUnitOfWork unitOfWork,
//             IUserService userService)
//         {
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//             _userService = userService;
//         }
//
//         public async System.Threading.Tasks.Task<DataResult<UnitSelectionModel>> GetTaskDetails(int taskId)
//         {
//             Entities.Tables.Task entityTask = await _unitOfWork.Tasks.GetTaskExpanded(taskId);
//             if (entityTask == null)
//             {
//                 return new DataResult<UnitSelectionModel>()
//                 {
//                     Message = ResponseMessageType.InvalidId,
//                     ResponseStatusType = ResponseStatusType.Error,
//                     MessageDetails = "Task not found"
//                 };
//             }
//             UnitSelectionModel model = _mapper.Map<UnitSelectionModel>(entityTask.Unit);
//             model.Creator = _mapper.Map<UserModel>(
//                 (await _userService.GetUser(entityTask.Unit.CreatorId, entityTask.ProjectId)).Data);
//
//             List<Entities.Tables.Unit> subUnits = 
//                 await _unitOfWork.RelationShips.GetSubUnitsByParentId(entityTask.UnitId);
//             UserModel assignee = entityTask.AssignedId == null
//                 ? null
//                 : (await _userService.GetUser(entityTask.AssignedId.Value, entityTask.ProjectId)).Data;
//
//             List<SubUnitModel> subModels = new List<SubUnitModel>();
//             foreach (var unit in subUnits)
//             {
//                 SubUnitModel unitModel = _mapper.Map<SubUnitModel>(unit);
//                 unitModel.Creator = (await _userService.GetUser(unit.CreatorId, entityTask.ProjectId)).Data;
//                 subModels.Add(unitModel);
//             }
//             List<string> tags = await _unitOfWork.TagOnTasks.GetTagsByTaskId(taskId);
//             TaskDetailsModel taskModel = new TaskDetailsModel()
//             {
//                 Id = taskId,
//                 Assignee = assignee,
//                 Tags = tags,
//                 Children = subModels
//             };
//             model.Data = JObject.FromObject(taskModel);
//             return new DataResult<UnitSelectionModel>()
//             {
//                 Data = model,
//                 ResponseStatusType = ResponseStatusType.Succeed
//             };
//         }
//     }
// }
