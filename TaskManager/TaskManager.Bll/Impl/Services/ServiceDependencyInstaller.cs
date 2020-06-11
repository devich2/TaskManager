using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Abstract.Converter;
using TaskManager.Bll.Abstract.MileStone;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Bll.Abstract.ProjectMember;
using TaskManager.Bll.Abstract.Tag;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Abstract.User;
using TaskManager.Bll.Impl.Services.Converter;
using TaskManager.Bll.Impl.Services.MileStone;
using TaskManager.Bll.Impl.Services.Project;
using TaskManager.Bll.Impl.Services.ProjectMember;
using TaskManager.Bll.Impl.Services.Tag;
using TaskManager.Bll.Impl.Services.Task;
using TaskManager.Bll.Impl.Services.Unit;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy;
using TaskManager.Bll.Impl.Services.User;
using TaskManager.Models.Response;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services
{
    public class ServiceDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            //MileStone
            services.AddTransient<IMileStoneService, MileStoneService>();
            //Project
            services.AddTransient<IProjectService, ProjectService>();
            //ProjectMember
            services.AddTransient<IProjectMemberService, ProjectMemberService>();
            //Tag
            services.AddTransient<ITagService, TagService>();
            //Task
            services.AddTransient<ITaskService, TaskService>();
            //User
            services.AddTransient<IUserService, UserService>();
            
            //Unit
            services.AddTransient<IUnitEditService, UnitEditService>();
            services.AddTransient<IUnitSelectionService, UnitSelectionService>();
            services.AddTransient<IUnitExtendedStrategyFactory, UnitExtendedStrategyFactory>();

            //Other dependencies
            services.AddSingleton<IConverterService<int, ResponseMessageType>, HttpStatusConverterService>();
        }
    }
}
