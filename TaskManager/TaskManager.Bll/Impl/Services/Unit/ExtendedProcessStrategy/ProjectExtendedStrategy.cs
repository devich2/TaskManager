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
using TaskManager.Models.Project;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    public class ProjectExtendedStrategy : BaseStrategy<int, Project, ProjectModel>, IUnitExtendedStrategy
    {
        public ProjectExtendedStrategy(
            IProjectRepository projectRepository,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(projectRepository, mapper, jsonOptions)
        {
        }
    }
}
