using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Impl.Services;

namespace TaskManager.Bll
{
    public class BllDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            //ServiceDependencyInstaller.Install(services);

            services.AddAutoMapper(typeof(BllDependencyInstaller));
            //MapperDependencyInstaller.Install(services);
        }
    }
}
