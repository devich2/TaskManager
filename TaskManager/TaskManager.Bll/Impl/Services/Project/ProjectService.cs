using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.Project;
using TaskManager.Models.Result;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectMemberService _projectMemberService;

        public ProjectService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IProjectMemberService projectMemberService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _projectMemberService = projectMemberService;
        }

        public async Task<DataResult<UnitSelectionModel>> GetProjectDetails(int unitId,
            int userId)
        {
            Entities.Tables.Project entityProject = await _unitOfWork.Projects.GetByUnitIdAsync(unitId);
            if (entityProject == null)
            {
                return new DataResult<UnitSelectionModel>()
                {
                    Message = ResponseMessageType.NotFound,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "Project not found"
                };
            }

            UnitSelectionModel model = _mapper.Map<UnitSelectionModel>(entityProject.Unit);
            model.Creator = _mapper.Map<UserModel>(
                (await _projectMemberService.GetUserInfo(entityProject.Unit.CreatorId, unitId)).Data);

            ProjectPreviewModel previewModel = await GetPreviewModel(unitId, userId);
            ProjectDetailsModel projectModel = _mapper.Map<ProjectDetailsModel>(previewModel);
            projectModel.TaskStatusList =
                await _unitOfWork.Units.GetUnitStatusCountByTypeAndParent(UnitType.Task, unitId);
            projectModel.MileStoneCount =
                await _unitOfWork.Units.GetCountAsync(x =>
                    x.UnitType == UnitType.Milestone && x.UnitParentId == unitId);

            model.Data = JObject.FromObject(projectModel);
            return new DataResult<UnitSelectionModel>()
            {
                Data = model,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }
        
        public async Task<ProjectPreviewModel> GetPreviewModel(int unitId, int userId)
        {
            int tasksCount =
                await _unitOfWork.Units.GetCountAsync(x => x.UnitParentId == unitId && x.UnitType == UnitType.Task);

            UserModel userModel = (await _projectMemberService.GetUserInfo(userId, unitId)).Data;

            Entities.Tables.Project project = await _unitOfWork.Projects.GetByUnitIdAsync(unitId);

            return new ProjectPreviewModel()
            {
                Role = userModel.Role,
                TasksCount = tasksCount,
                Members = project.Members
            };
        }
    }
}