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
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
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
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;

        public ProjectExtendedStrategy(IUnitOfWork unitOfWork, 
            UserManager<Entities.Tables.Identity.User> userManager,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(unitOfWork.Projects, mapper, jsonOptions)
        {
            _userManager = userManager;
        }

        protected override async System.Threading.Tasks.Task CreateAsync(Entities.Tables.Project entity, ProjectCreateOrUpdateModel model)
        {
            await base.CreateAsync(entity, model);
            var user = await _userManager.FindByIdAsync(model.ProjectManagerId.ToString());
            await _userManager.AddClaimAsync(user, new Claim("role", $"Owner_{entity.UnitId}"));
        }
    }
}
