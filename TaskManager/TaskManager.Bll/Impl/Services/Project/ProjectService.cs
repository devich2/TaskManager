using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        private readonly JsonSerializerSettings _serializerSettings;

        public ProjectService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IProjectMemberService projectMemberService,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _projectMemberService = projectMemberService;
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }

        public async Task<DataResult<UnitSelectionModel>> GetProjectDetails(int unitId,
            int userId)
        {
            Entities.Tables.Unit unit =
                await _unitOfWork.Units.SelectExpandedByUnitIdAndType(UnitType.Project, unitId);

            if (unit == null)
            {
                return new DataResult<UnitSelectionModel>()
                {
                    Message = ResponseMessageType.NotFound,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "Project not found"
                };
            }

            UnitSelectionModel model = _mapper.Map<UnitSelectionModel>(unit);

            ProjectPreviewModel previewModel = await GetPreviewModel(unit, userId);
            ProjectDetailsModel projectModel = _mapper.Map<ProjectDetailsModel>(previewModel);

            projectModel.TaskStatusList = unit.Children
                .GroupBy(x => x.TermInfo.Status, x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            projectModel.MileStoneCount = unit.Children.Count(x => x.UnitType == UnitType.Milestone);

            model.Data = JObject.FromObject(projectModel, JsonSerializer.Create(_serializerSettings));
            return new DataResult<UnitSelectionModel>()
            {
                Data = model,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }

        public async Task<ProjectPreviewModel> GetPreviewModel(Entities.Tables.Unit unit, int userId)
        {
            string role = await _projectMemberService.GetUserProjectRole(userId, unit.UnitId);
            int tasksCount = unit.Children.Count(x => x.UnitType == UnitType.Task);

            return new ProjectPreviewModel()
            {
                Role = role,
                TasksCount = tasksCount,
                Members = unit.Project.Members
            };
        }
    }
}