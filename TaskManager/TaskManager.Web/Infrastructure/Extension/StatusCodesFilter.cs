using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Abstract.Converter;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Response;

namespace TaskManager.Web.Infrastructure.Extension
{
    public class StatusCodesFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var objectResult = context.Result as ObjectResult;
            Result result = objectResult?.Value as Result;

            if (result == null)
            {
                return;
            }

            var statusConverter = context.HttpContext.RequestServices
                .GetRequiredService<IConverterService<int, ResponseMessageType>>();

            var statusCode = statusConverter.Convert(result.Message);

            context.HttpContext.Response.StatusCode = statusCode;
        }
    }
}
