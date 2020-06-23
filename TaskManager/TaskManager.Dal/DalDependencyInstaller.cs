using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Abstract.IRepository.FiltersAndSorting;
using TaskManager.Dal.Abstract.Transactions;
using TaskManager.Dal.Impl;
using TaskManager.Dal.Impl.ImplRepository;
using TaskManager.Dal.Impl.ImplRepository.FiltersAndSorting;
using TaskManager.Dal.Impl.Transactions;

namespace TaskManager.Dal
{
    public static class DalDependencyInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings")
                                   ?? configuration.GetConnectionString("DefaultConnection");
            //Configure database context
            services.AddDbContext<TaskManagerDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString,
                    p =>
                    {
                        p.EnableRetryOnFailure();
                        p.MigrationsAssembly("TaskManager.Dal");
                    });
            });
            
            
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ITagOnTaskRepository, TagOnTaskRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITermInfoRepository, TermInfoRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IMileStoneRepository, MileStoneRepository>();
            services.AddTransient<IProjectMemberRepository, ProjectMemberRepository>();
            
            //Other dependencies
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionManager, DbTransactionManager>();
            services.AddTransient<IFiltersQueryFactory, FilterQueryFactory>();
            services.AddTransient<IOrderQueryFactory, OrderQueryFactory>();
        }
    }
}
