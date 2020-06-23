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
    public class TaskExtendedStrategy : BaseStrategy<Entities.Tables.Task, TaskCreateOrUpdateModel>, IUnitExtendedStrategy
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskExtendedStrategy(IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(unitOfWork.Tasks, mapper, jsonOptions)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async System.Threading.Tasks.Task CreateAsync(Entities.Tables.Task entity, TaskCreateOrUpdateModel model)
        {
            await base.CreateAsync(entity, model);
            await _unitOfWork.TagOnTasks.AddToTags(entity.Id, model.Tags);
            await _unitOfWork.SaveAsync();
        }
    }
}
