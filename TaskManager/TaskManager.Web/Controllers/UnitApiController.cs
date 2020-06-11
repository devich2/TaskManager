using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Common.Utils;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;
using TaskManager.Web.Infrastructure.Extension;

namespace TaskManager.Web.Controllers
{
    [Route("api/Unit")]
    [ApiController]
    public class UnitApiController : ControllerBase
    {
        private readonly IUnitEditService _editService;
        private readonly IUnitSelectionService _unitSelectionService;
        private readonly IMapper _mapper;

        public UnitApiController(IUnitEditService editService,
            IUnitSelectionService unitSelectionService,
            IMapper mapper)
        {
            _editService = editService;
            _unitSelectionService = unitSelectionService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("{projectId}")]
        public async Task<DataResult<List<UnitSelectionModel>>> Get
            (int projectId, UnitType unitType, int? startIndex, int? count, string sortingQuery, string filterQuery)
        {
            if (unitType != UnitType.Task && unitType != UnitType.Milestone)
            {
                return new DataResult<List<UnitSelectionModel>>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.OperationNotAllowedForUnitType
                };
            }
            SelectionOptions so = 
                new SelectionOptions()
                {
                    ExtendedType = unitType
                };
            if (count.HasValue && startIndex.HasValue)
            {
                so.PagingOptions = new PagingOptions()
                {
                    Count = count.Value,
                    StartIndex = startIndex.Value,
                };
            }

            DataResult<FilterOptions> filterOptionsDataResult =
                UnitFilterExtractor.ExtractFilterOptionsDataResult(filterQuery);

            if (filterOptionsDataResult.ResponseStatusType ==
                ResponseStatusType.Succeed &&
                filterOptionsDataResult.Message ==
                ResponseMessageType.None)
            {
                so.FilterOptions = filterOptionsDataResult.Data;
            }

            if (filterOptionsDataResult.ResponseStatusType ==
                ResponseStatusType.Error)
            {
                return new DataResult<List<UnitSelectionModel>>()
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
                return new DataResult<List<UnitSelectionModel>>()
                {
                    Message = sortingOptionsDataResult.Message,
                    ResponseStatusType = sortingOptionsDataResult.ResponseStatusType,
                    MessageDetails = sortingOptionsDataResult.MessageDetails
                };
            }
            return await _unitSelectionService.GetUnitPreview(so);
        }
        
        [HttpPost]
        public async Task<DataResult<UnitAddResponse>> Post([FromBody] UnitModel model)
        {
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
        public async Task<DataResult<UnitUpdateResponse>> Put([FromRoute] int id,
            [FromBody] UnitModel model)
        {
            int userId = 1;
            UnitBlModel blModel = _mapper.Map<UnitBlModel>(model);
            blModel.UnitId = id;
            blModel.UserId = userId;

            return await _editService.ProcessUnitUpdate(blModel);
        }
        
        [HttpPut]
        [Route("{id}/state/{newStatus}")]
        public async Task<DataResult<UnitUpdateResponse>> Put([FromRoute] int id, [FromRoute] Status newStatus)
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
        public async Task<Result> Delete([FromRoute] int id)
        {
            return await _editService.ProcessUnitDelete(id);
        }
    }
}