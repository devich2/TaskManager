// using System;
// using System.Collections.Generic;
// using System.Text;
// using AutoMapper;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
// using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
// using TaskManager.Dal.Abstract;
// using TaskManager.Dal.Abstract.IRepository;
// using TaskManager.Entities.Tables;
// using TaskManager.Entities.Tables.Identity;
// using TaskManager.Models.Project;
// using TaskManager.Models.Task;
// using Task = System.Threading.Tasks.Task;
//
// namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
// {
//     public class ProjectExtendedStrategy : BaseStrategy<int, Project, ProjectCreateModel>, IUnitExtendedStrategy
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly RoleManager<Role> _roleManager;
//
//         public ProjectExtendedStrategy(IUnitOfWork unitOfWork, 
//             RoleManager<Role> roleManager,
//             IMapper mapper,
//             IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(unitOfWork.Projects, mapper, jsonOptions)
//         {
//             _unitOfWork = unitOfWork;
//             _roleManager = roleManager;
//         }
//
//         protected override async Task CreateDependency(ProjectCreateModel model, int unitId)
//         {
//             Project project = await _unitOfWork.Projects.GetByUnitIdAsync(unitId);
//             Role role = await _roleManager.FindByNameAsync("Owner");
//             var projectMember = 
//                 await _unitOfWork.ProjectMembers.AddAsync(new ProjectMember()
//             {
//                 UserId = model.ProjectManagerId,
//                 ProjectId = project.Id
//             });
//             await _unitOfWork.Permissions.AddAsync(new Permission()
//             {
//                 ProjectMemberId = projectMember.Id,
//                 RoleId = role.Id
//             });
//             await _unitOfWork.SaveAsync();
//         }
//     }
// }
