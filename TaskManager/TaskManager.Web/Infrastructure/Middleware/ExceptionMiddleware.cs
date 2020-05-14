using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TaskManager.Common.Utils;
using TaskManager.Models.Response;

namespace TaskManager.Web.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions,
            IWebHostEnvironment env
        )
        {
            _next = next;
            _logger = logger;
            _serializerSettings = jsonOptions.Value.SerializerSettings;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogW(e, "Unhandled exception in api controller");
                if (context.Response.HasStarted)
                {
                    _logger.LogW(e,
                        "The response has already started, the Exception Middleware will not be executed");
                    throw;
                }

                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var expResult = new ExceptionResult
            {
                ResponseStatusType = ResponseStatusType.Error,
                Message = ResponseMessageType.InternalError
            };

            if (!_env.IsProduction())
            {
                expResult.StackTrace = e.ToString();
            }
            var result = JsonConvert.SerializeObject(expResult, _serializerSettings);
            context.Response.ContentType = "application/json; charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }
}
