using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using RazorLight;

namespace TaskManager.Email.Template.Engine.Services
{
    public class RazorViewToStringRenderer : IRazorViewToStringRenderer
    {
        private readonly RazorLightEngine _engine;

        public RazorViewToStringRenderer()
        {
            _engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(GetPath())
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var found = _engine.Options.CachingProvider.RetrieveTemplate(viewName);
            if (found.Success)
            {
                return await _engine.RenderTemplateAsync(found.Template.TemplatePageFactory(), model);
            }
            return await _engine.CompileRenderAsync(viewName, model);
        }

        private string GetApplicationRoot()
        {
            var exePath =   Path.GetDirectoryName(System.Reflection
                .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher=new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        private string GetPath()
        {
            var typeFullName = (typeof(EmailEngineDependencyInstaller)).Namespace;
            string projectDirectory = Directory.GetParent(GetApplicationRoot()).FullName;
            return Path.Combine(projectDirectory, typeFullName);
        }
    }
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}