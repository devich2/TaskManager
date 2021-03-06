﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Abstract.IRepository.FiltersAndSorting;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.MileStone;
using TaskManager.Models.Pagination;
using TaskManager.Models.Project;
using TaskManager.Models.Result;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.Unit
{
    public class UnitSelectionService : IUnitSelectionService
    {
        private readonly IUnitExtendedStrategyFactory _unitExtendedStrategyFactory;
        private readonly IMapper _mapper;
        private readonly IProjectMemberService _projectMemberService;
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly IMileStoneService _mileStoneService;
        private readonly IFiltersQueryFactory _filtersQueryFactory;
        private readonly IOrderQueryFactory _orderQueryFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JsonSerializerSettings _serializerSettings;

        // ReSharper disable once UnusedParameter.Local
        public UnitSelectionService(IUnitExtendedStrategyFactory unitExtendedStrategyFactory,
            IMapper mapper,
            IProjectMemberService projectMemberService,
            ITaskService taskService,
            IProjectService projectService,
            IMileStoneService mileStoneService,
            IFiltersQueryFactory filtersQueryFactory,
            IOrderQueryFactory orderQueryFactory,
            IUnitOfWork unitOfWork,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _unitExtendedStrategyFactory = unitExtendedStrategyFactory;
            _mapper = mapper;
            _projectMemberService = projectMemberService;
            _taskService = taskService;
            _projectService = projectService;
            _mileStoneService = mileStoneService;
            _filtersQueryFactory = filtersQueryFactory;
            _orderQueryFactory = orderQueryFactory;
            _unitOfWork = unitOfWork;
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }

        public async Task<SelectionModel<UnitSelectionModel>> GetUnitPreview(int userId, SelectionOptions options)
        {
            IQueryable<int> compoundQueryableFilter = null;
            List<Tuple<Expression<Func<Entities.Tables.Unit, object>>, SortingType>> sortingExpression = null;

            if (options.FilterOptions != null &&
                options.FilterOptions.Filters.Any())
            {
                compoundQueryableFilter = options.FilterOptions.Filters.Select(filter =>
                        _filtersQueryFactory.GetUnitFilterQuery(options.ExtendedType, filter))
                    .Aggregate((prev, cur) => prev.Intersect(cur));
            }

            if (options.SortingOptions != null &&
                options.SortingOptions.Sortings.Any())
            {
                sortingExpression = options.SortingOptions.Sortings
                    .Select(sortItem => _orderQueryFactory
                        .GetUnitOrderQuery(options.ExtendedType, sortItem)).ToList();
            }

            var selectionResult = await _unitOfWork.Units
                .SelectByType(options.ExtendedType, options.PagingOptions, compoundQueryableFilter, sortingExpression);
            int count = await _unitOfWork.Units.SelectByTypeCount(options.ExtendedType, compoundQueryableFilter);

            List<UnitSelectionModel> result = new List<UnitSelectionModel>();
            foreach (var item in selectionResult)
            {
                UnitSelectionModel unitModel = _mapper.Map<UnitSelectionModel>(item);
                unitModel.Data = await GetRelatedPreviewData(item, userId);
                result.Add(unitModel);
            }

            return new SelectionModel<UnitSelectionModel>()
            {
                Result = result,
                TotalCount = count
            };
        }

        public async Task<DataResult<UnitSelectionModel>> GetUnitById(int unitId)
        {
            Entities.Tables.Unit unitEntity = await _unitOfWork.Units.GetByUnitIdAsync(unitId);
            if (unitEntity == null)
            {
                return new DataResult<UnitSelectionModel>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.NotFound
                };
            }

            IUnitExtendedStrategy strategy =
                _unitExtendedStrategyFactory.GetInstance(unitEntity.UnitType);

            DataResult<JObject> processResult =
                await strategy.ProcessExistingModel(unitEntity);

            if (processResult.ResponseStatusType != ResponseStatusType.Succeed)
            {
                return new DataResult<UnitSelectionModel>()
                {
                    ResponseStatusType = processResult.ResponseStatusType,
                    Message = processResult.Message,
                    MessageDetails = processResult.MessageDetails
                };
            }

            UnitSelectionModel model = _mapper.Map<UnitSelectionModel>(unitEntity);
            model.Data = processResult.Data;

            return new DataResult<UnitSelectionModel>()
            {
                Data = model,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }

        private async Task<JObject> GetRelatedPreviewData(Entities.Tables.Unit item, int userId)
        {
            JObject current = null;
            JsonSerializer jsonSerializer = JsonSerializer.Create(_serializerSettings);
            switch (item.UnitType)
            {
                case UnitType.Milestone:
                    MileStonePreviewModel model = await _mileStoneService.GetPreviewModel(item);
                    current = JObject.FromObject(model, jsonSerializer);
                    break;
                case UnitType.Task:
                    TaskPreviewModel taskModel = await _taskService.GetPreviewModel(item);
                    current = JObject.FromObject(taskModel, jsonSerializer);
                    break;
                case UnitType.Project:
                    ProjectPreviewModel projectModel = await _projectService.GetPreviewModel(item, userId);
                    current = JObject.FromObject(projectModel, jsonSerializer);
                    break;
            }

            return current;
        }
    }
}