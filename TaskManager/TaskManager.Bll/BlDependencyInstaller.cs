using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Impl.Services;

namespace TaskManager.Bll
{
    public class BllDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            ServiceDependencyInstaller.Install(services);
            //MapperDependencyInstaller.Install(services);
        }
    }
}
