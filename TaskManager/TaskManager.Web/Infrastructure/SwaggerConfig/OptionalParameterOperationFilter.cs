using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManager.Web.Infrastructure.SwaggerConfig
{
    public class OptionalParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            var parameters = context.ApiDescription.ParameterDescriptions;
            foreach (var pm in parameters)
            {
                var info = pm.ParameterInfo();
                if (info == null)
                    continue;
                if (!(info.HasDefaultValue || info.IsOptional))
                {
                    var param = operation.Parameters.FirstOrDefault(x => x.Name.ToLowerInvariant() == pm.Name.ToLowerInvariant());
                    if (param == null)
                        continue;
                    param.Required = pm.ModelMetadata.IsRequired;
                }

            }
        }
    }
}