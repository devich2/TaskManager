using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Permission;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Common.Utils;
using TaskManager.Configuration;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.Pagination;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;
using TaskManager.Web.Infrastructure.Extension;
using TaskManager.Web.Infrastructure.Handler;

namespace TaskManager.Web.Controllers
{
    [Route("api/{projectId}/unit/")]
    [ApiController]
    public class UnitApiController : ControllerBase
    {
        private readonly IUnitEditService _editService;
        private readonly IUnitSelectionService _unitSelectionService;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly PaginationConfiguration _paginationConfiguration;

        public UnitApiController(IUnitEditService editService,
            IUnitSelectionService unitSelectionService,
            IMapper mapper,
            IPermissionService permissionService,
            IOptions<PaginationConfiguration> options)
        {
            _editService = editService;
            _unitSelectionService = unitSelectionService;
            _mapper = mapper;
            _permissionService = permissionService;
            _paginationConfiguration = options.Value;
        }

        [HttpGet]
        [HasPermission(PermissionType.Read)]
        public async Task<DataResult<GenericPaginatedModel<UnitSelectionModel>>> Get
            (int projectId, UnitType unitType, string sortingQuery, string filterQuery, int? page)
        {
            if (unitType != UnitType.Task && unitType != UnitType.Milestone)
            {
                return new DataResult<GenericPaginatedModel<UnitSelectionModel>>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.OperationNotAllowedForUnitType
                };
            }

            int pageSize = _paginationConfiguration.PageSize;
            SelectionOptions so =
                new SelectionOptions()
                {
                    ExtendedType = unitType,
                    PagingOptions = new PagingOptions()
                    {
                        StartIndex = ((page ?? 1) - 1) * pageSize,
                        Count = pageSize
                    }
                };

            DataResult<FilterOptions> filterOptionsDataResult =
                UnitFilterExtractor.ExtractFilterOptionsDataResult(filterQuery);

            if (filterOptionsDataResult.ResponseStatusType ==
                ResponseStatusType.Succeed &&
                filterOptionsDataResult.Message ==
                ResponseMessageType.None)
            {
                so.FilterOptions = filterOptionsDataResult.Data;
                so.FilterOptions.Filters.Add(UnitFilterType.Project, projectId);
            }

            if (filterOptionsDataResult.ResponseStatusType ==
                ResponseStatusType.Error)
            {
                return new DataResult<GenericPaginatedModel<UnitSelectionModel>>()
                {
                    Message = filterOptionsDataResult.Message,
                    ResponseStatusType = filterOptionsDataResult.ResponseStatusType,
                    MessageDetails = filterOptionsDataResult.MessageDetails
                };
            }

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

            List<UnitSelectionModel> unitModels = await _unitSelectionService.GetUnitPreview(User.GetUserId(), so);
            int count = await _unitSelectionService.SelectByTypeCount(
                unitType, unitModels.Select(x => x.UnitId));

            return new DataResult<GenericPaginatedModel<UnitSelectionModel>>()
            {
                ResponseStatusType = unitModels.Any() ? ResponseStatusType.Succeed : ResponseStatusType.Warning,
                Data = new GenericPaginatedModel<UnitSelectionModel>()
                {
                    Models = unitModels,
                    PaginationModel = new PaginationModel(count, page ?? 1, pageSize)
                }
            };
        }


        [HttpPost]
        public async Task<DataResult<UnitAddResponse>> Post(int projectId, [FromBody] UnitModel model)
        {
            if (User == null ||
                !_permissionService.HasAccessByTypeAndProcessToState(
                    User.Claims, projectId, model.ExtendedType, Models.Unit.ModelState.Add))
                return new DataResult<UnitAddResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.UserNotAuthorized,
                    MessageDetails = $"No access to create {model.ExtendedType}"
                };

            int userId = User.GetUserId();
            if (model.ExtendedType == UnitType.Project)
            {
                return new DataResult<UnitAddResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.OperationNotAllowedForUnitType
                };
            }

            UnitBlModel blModel = _mapper.Map<UnitBlModel>(model);
            blModel.UserId = userId;
            return await _editService.ProcessUnitCreate(blModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<DataResult<UnitUpdateResponse>> Put(int projectId, int id,
            [FromBody] UnitModel model)
        {
            if (User == null ||
                !_permissionService.HasAccessByTypeAndProcessToState(
                    User.Claims, projectId, model.ExtendedType, Models.Unit.ModelState.Modify))
                return new DataResult<UnitUpdateResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.UserNotAuthorized,
                    MessageDetails = $"No access to update {model.ExtendedType}"
                };
            int userId = User.GetUserId();
            UnitBlModel blModel = _mapper.Map<UnitBlModel>(model);
            blModel.UnitId = id;
            blModel.UserId = userId;
            return await _editService.ProcessUnitUpdate(blModel);
        }

        [HttpPut]
        [Route("{id}/state/{newStatus}")]
        [HasPermission(PermissionType.StatusChange)]
        public async Task<DataResult<UnitUpdateResponse>> Put(int id, [FromRoute] Status newStatus)
        {
            int userId = User.GetUserId();

            UnitStatusPatchModel um = new UnitStatusPatchModel()
            {
                Status = newStatus,
                UserId = userId,
                UnitId = id
            };
            return await _editService.ProcessContentChangeStatus(um);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Result> Delete(int projectId, int id, [FromBody] UnitDeleteModel model)
        {
            if (User == null ||
                !_permissionService.HasAccessByTypeAndProcessToState(
                    User.Claims, projectId, model.UnitType, Models.Unit.ModelState.Delete))
                return new DataResult<UnitUpdateResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.UserNotAuthorized,
                    MessageDetails = $"No access to delete {model.UnitType}"
                };
            model.UnitId = id;
            return await _editService.ProcessUnitDelete(model);
        }
    }
}