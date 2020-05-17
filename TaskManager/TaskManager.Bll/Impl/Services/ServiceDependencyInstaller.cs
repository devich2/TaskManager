using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Abstract.Converter;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Impl.Services.Converter;
using TaskManager.Bll.Impl.Services.Unit;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy;
using TaskManager.Models.Response;

namespace TaskManager.Bll.Impl.Services
{
    public class ServiceDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            //Permission

            //ProjectMember

            //Tag

            //Unit
            services.AddTransient<IUnitEditService, UnitEditService>();
            services.AddTransient<IUnitExtendedStrategyFactory, UnitExtendedStrategyFactory>();

            //Other dependencies
            services.AddSingleton<IConverterService<int, ResponseMessageType>, HttpStatusConverterService>();
        }
    }
}
