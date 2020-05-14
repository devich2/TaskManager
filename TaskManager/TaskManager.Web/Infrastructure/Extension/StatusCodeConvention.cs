using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace TaskManager.Web.Infrastructure.Extension
{
    public class StatusCodeConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                controller.Filters.Add(new StatusCodesFilter());
            }
        }
    }
}
