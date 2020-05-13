using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            //
        }
    }
}
