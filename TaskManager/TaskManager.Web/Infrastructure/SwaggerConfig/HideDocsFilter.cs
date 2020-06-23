using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManager.Web.Infrastructure.SwaggerConfig
{
    public class HideDocsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                if(apiDescription.TryGetMethodInfo(out MethodInfo mf))
                {
                    if(mf.DeclaringType.GetCustomAttribute<ApiControllerAttribute>() == null)
                    {
                        var route = "/" + apiDescription.RelativePath;
                        swaggerDoc.Paths.Remove(route);
                    }
                }
            }
        }
    }
}