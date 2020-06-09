using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Abstract.User;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository.FiltersAndSorting;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.MileStone;
using TaskManager.Models.Project;
using TaskManager.Models.Response;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.Unit
{
    public class UnitSelectionService : IUnitSelectionService
    {
        private readonly IUnitExtendedStrategyFactory _unitExtendedStrategyFactory;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFiltersQueryFactory _filtersQueryFactory;
        private readonly IOrderQueryFactory _orderQueryFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JsonSerializerSettings _serializerSettings;

        // ReSharper disable once UnusedParameter.Local
        public UnitSelectionService(IUnitExtendedStrategyFactory unitExtendedStrategyFactory,
            IMapper mapper,
            IUserService userService,
            ITaskService taskService,
            IFiltersQueryFactory filtersQueryFactory,
            IOrderQueryFactory orderQueryFactory,
            IUnitOfWork unitOfWork,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _unitExtendedStrategyFactory = unitExtendedStrategyFactory;
            _mapper = mapper;
            _userService = userService;
            _filtersQueryFactory = filtersQueryFactory;
            _orderQueryFactory = orderQueryFactory;
            _unitOfWork = unitOfWork;
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }

        public async Task<List<Entities.Tables.Unit>> GetFilteredUnits(SelectionOptions options)
        {
            IQueryable<int> compoundQueryableFilter = null;
            List<Tuple<Expression<Func<Entities.Tables.Unit, object>>, SortingType>> sortingExpression = null;

            if (options.FilterOptions != null &&
                options.FilterOptions.Filters.Any())
            {
                compoundQueryableFilter = options.FilterOptions.Filters.Select(filter =>
                        _filtersQueryFactory.GetFilterQuery(options.ExtendedType, filter))
                    .Aggregate((prev, cur) => prev.Intersect(cur));
            }

            if (options.SortingOptions != null &&
                options.SortingOptions.Sortings.Any())
            {
                sortingExpression = options.SortingOptions.Sortings
                    .Select(sortItem => _orderQueryFactory
                        .GetOrderQuery(options.ExtendedType, sortItem)).ToList();
            }
    
            return await _unitOfWork.Units
                .SelectByType(options.ExtendedType, options.PagingOptions, compoundQueryableFilter, sortingExpression);
        }

        public async Task<DataResult<List<UnitSelectionModel>>> GetUnitPreview(SelectionOptions options)
        {
            var selectionResult = await GetFilteredUnits(options);
            List<UnitSelectionModel> result = new List<UnitSelectionModel>();
            foreach (var item in selectionResult)
            {
                UnitSelectionModel model = _mapper.Map<UnitSelectionModel>(item);
                model.Creator = (await _userService.GetUser(item.CreatorId, options.ProjectId)).Data;
                model.Data = await GetRelatedPreviewData(item, options.UserId);
                result.Add(model);
            }

            DataResult<List<UnitSelectionModel>> methodResult =
                new DataResult<List<UnitSelectionModel>>();

            if (result.Any())
            {
                methodResult.ResponseStatusType = ResponseStatusType.Succeed;
                methodResult.Data = result;
            }
            else
            {
                methodResult.ResponseStatusType = ResponseStatusType.Warning;
                methodResult.Data = result;
                methodResult.Message = ResponseMessageType.EmptyResult;
            }

            return methodResult;
        }

        public async Task<DataResult<int>> GetFilteredCountByType(UnitType type, FilterOptions filterOptions)
        {
            IQueryable<int> filters = null;
            if (filterOptions != null && filterOptions.Filters.Any())
            {
                filters = filterOptions.Filters
                    .Select(x => _filtersQueryFactory.GetFilterQuery(type, x))
                    .Aggregate((prev, cur) => prev.Intersect(cur));
            }

            int count = await _unitOfWork.Units.SelectByTypeCount(type, filters);

            DataResult<int> methodResult = new DataResult<int>()
            {
                Data = count,
                ResponseStatusType = (count == 0)
                    ? ResponseStatusType.Warning
                    : ResponseStatusType.Succeed,
                Message = (count == 0)
                    ? ResponseMessageType.EmptyResult
                    : ResponseMessageType.None
            };

            return methodResult;
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
                case UnitType.Milestone: ;
                    int countDone = item.Children.Count(x => x.TermInfo.Status == Status.Closed);
                    int total = item.Children.Count();
                    MileStoneSelectionModel model = new MileStoneSelectionModel()
                    {
                        ClosedTasksCount = countDone,
                        Total = total,
                        CompletedPercentage = (decimal)countDone/total
                    };
                    current = JObject.FromObject(model, jsonSerializer);
                    break;
                case UnitType.Project:
                    int tasksCount = item.Children.Count(x => x.UnitType == UnitType.Task);
                    UserModel userModel = (await _userService.GetUser(userId, item.Project.UnitId)).Data;
                    ProjectPreviewModel prModel = new ProjectPreviewModel()
                    {
                        Role = userModel.Role,
                        TasksCount = tasksCount,
                        Members = item.Project.Members
                    };
                    current = JObject.FromObject(prModel, jsonSerializer);
                    break;
                case UnitType.Task:
                    TaskPreviewModel taskModel = _mapper.Map<TaskPreviewModel>(item.Task);
                    taskModel.Tags = await _unitOfWork.TagOnTasks.GetTagsByTaskId(item.Task.Id);
                    taskModel.SubTasksTotal = item.Children.Count(x=>x.UnitType == UnitType.SubTask);
                    taskModel.SubTasksCompleted = item.Children.Count(x => x.TermInfo.Status == Status.Closed);
                    taskModel.Comments = item.Children.Count(x => x.UnitType == UnitType.Comment);
                    current = JObject.FromObject(taskModel, jsonSerializer);
                    break;
                case UnitType.SubTask:
                    break;
                case UnitType.Comment:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return current;
        }
    }
}