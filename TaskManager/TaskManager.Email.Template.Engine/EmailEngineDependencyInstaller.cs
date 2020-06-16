using Microsoft.Extensions.DependencyInjection;
using TaskManager.Email.Template.Engine.Services;

namespace TaskManager.Email.Template.Engine
{
    public static class EmailEngineDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        }
    }
}