using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Common.Security;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Project;
using TaskManager.Models.Task;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    public class ProjectExtendedStrategy : BaseStrategy<Entities.Tables.Project, ProjectCreateOrUpdateModel>, IUnitExtendedStrategy
    {
        private readonly IProjectMemberService _projectMemberService;

        public ProjectExtendedStrategy(IUnitOfWork unitOfWork, 
            IProjectMemberService projectMemberService,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(unitOfWork.Projects, mapper, jsonOptions)
        {
            _projectMemberService = projectMemberService;
        }

        protected override async System.Threading.Tasks.Task CreateAsync(Entities.Tables.Project entity, ProjectCreateOrUpdateModel model)
        {
            await base.CreateAsync(entity, model);
            await _projectMemberService.AddUserRole(entity.UnitId, model.ProjectManagerId, RoleNames.Owner);
        }
    }
}
