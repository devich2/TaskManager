using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Abstract.Transactions;
using TaskManager.Dal.Impl;
using TaskManager.Dal.Impl.ImplRepository;
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionManager, DbTransactionManager>();

            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IProjectMemberRepository, ProjectMemberRepository>();
            services.AddTransient<IRelationShipRepository, RelationShipRepository>();
            services.AddTransient<ITagOnTaskRepository, TagOnTaskRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITermInfoRepository, TermInfoRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
        }
    }
}
