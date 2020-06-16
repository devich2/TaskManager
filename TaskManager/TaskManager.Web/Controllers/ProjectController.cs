using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Common.Utils;
using TaskManager.Configuration;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.MileStone;
using TaskManager.Models.Pagination;
using TaskManager.Models.Project;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.TermInfo;
using TaskManager.Models.Unit;
using TaskManager.Models.User;
using TaskManager.Web.Infrastructure.Extension;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/project/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUnitEditService _unitEditService;
        private readonly IUnitSelectionService _unitSelectionService;
        private readonly PaginationConfiguration _paginationConfiguration;

        public ProjectController(IProjectService projectService, 
        IUnitEditService unitEditService,
        IUnitSelectionService unitSelectionService,
        IOptions<PaginationConfiguration> options)
        {
            _projectService = projectService;
            _unitEditService = unitEditService;
            _unitSelectionService = unitSelectionService;
            _paginationConfiguration = options.Value;
        }
        
        [HttpGet]
        [Route("{projectId}")]      
        [HasPermission(PermissionType.Read)]           
        public async Task<DataResult<UnitSelectionModel>> Get(int projectId)
        {
            return await _projectService.GetProjectDetails(projectId, User.GetUserId());
        }
        
        [HttpGet]
        [Route("my")]   
        [Authorize]
        public async Task<DataResult<GenericPaginatedModel<UnitSelectionModel>>> Get(string sortingQuery, int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            SelectionOptions so =
                new SelectionOptions()
                {
                    ExtendedType = UnitType.Project,
                    PagingOptions = new PagingOptions()
                    {
                        StartIndex = ((page ?? 1) - 1) * pageSize,
                        Count = pageSize
                    }
                };
            DataResult<SortingOptions> sortingOptionsDataResult =
                UnitOrderExtractor.ExtractSortingOptionsDataResult(sortingQuery);

            if (sortingOptionsDataResult.ResponseStatusType ==
                ResponseStatusType.Succeed &&
                sortingOptionsDataResult.Message ==
                ResponseMessageType.None)
            {
                so.SortingOptions = sortingOptionsDataResult.Data;
            }

            if (sortingOptionsDataResult.ResponseStatusType ==
                ResponseStatusType.Error)
            {
                return new DataResult<GenericPaginatedModel<UnitSelectionModel>>()
                {
                    Message = sortingOptionsDataResult.Message,
                    ResponseStatusType = sortingOptionsDataResult.ResponseStatusType,
                    MessageDetails = sortingOptionsDataResult.MessageDetails
                };
            }
            so.FilterOptions.Filters.Add(UnitFilterType.UserAccess, User.GetUserId());
            SelectionModel<UnitSelectionModel> selection = await _unitSelectionService.GetUnitPreview(User.GetUserId(), so);
         

            return new DataResult<GenericPaginatedModel<UnitSelectionModel>>()
            {
                ResponseStatusType = selection.Result.Any() ? ResponseStatusType.Succeed : ResponseStatusType.Warning,
                Data = new GenericPaginatedModel<UnitSelectionModel>()
                {
                    Models = selection.Result,
                    PaginationModel = new PaginationModel(selection.TotalCount, page ?? 1, pageSize)
                }
            };
        }
        
        [HttpPost]
        [Authorize]
        public async Task<DataResult<UnitAddResponse>> Post([FromBody] ProjectUnitCreateModel model)
        {
            int userId = User.GetUserId();
            UnitBlModel unitBlModel = new UnitBlModel()
            {
                UserId = userId,
                Name = model.Name,
                Description = model.Description,
                ExtendedType = UnitType.Project,
                TermInfo = new TermInfoCreateModel(){Status = Status.InProgress},
                Data = JObject.FromObject(new ProjectCreateOrUpdateModel(){ProjectManagerId = userId, Members = 1})
            };
            return await _unitEditService.ProcessUnitCreate(unitBlModel);
        }
    }
}