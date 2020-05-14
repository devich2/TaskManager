using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Abstract.Converter;
using TaskManager.Bll.Impl.Services.Converter;
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

            //Other dependencies
            services.AddSingleton<IConverterService<int, ResponseMessageType>, HttpStatusConverterService>();
        }
    }
}
