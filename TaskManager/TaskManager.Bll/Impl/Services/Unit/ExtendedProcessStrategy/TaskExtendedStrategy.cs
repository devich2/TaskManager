using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Tables;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    class TaskExtendedStrategy : BaseStrategy<int, Task, TaskModel>, IUnitExtendedStrategy
    {
        public TaskExtendedStrategy(
            ITaskRepository taskRepository,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(taskRepository, mapper, jsonOptions)
        {
        }
    }
}
