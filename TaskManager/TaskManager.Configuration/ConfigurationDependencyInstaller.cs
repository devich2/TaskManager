using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TaskManager.Configuration
{
    public static class ConfigurationDependencyInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration configuration)
        {
           // services.ConfigureOptions<IOptions<Result>>()
        }
    }
}
