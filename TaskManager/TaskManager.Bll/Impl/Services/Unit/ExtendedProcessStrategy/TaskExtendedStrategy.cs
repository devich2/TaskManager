using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Tables;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    public class TaskExtendedStrategy : BaseStrategy<int, Task, TaskCreateModel>, IUnitExtendedStrategy
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskExtendedStrategy(IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(unitOfWork.Tasks, mapper, jsonOptions)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async System.Threading.Tasks.Task CreateDependency(TaskCreateModel model, int unitId)
        {
            Task task = await _currentRepository.GetByUnitIdAsync(unitId);
            await _unitOfWork.TagOnTasks.AddToTags(task.Id, model.Tags);
            await _unitOfWork.SaveAsync();
        }
    }
}
