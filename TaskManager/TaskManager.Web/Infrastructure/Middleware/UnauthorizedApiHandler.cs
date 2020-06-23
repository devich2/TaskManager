using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TaskManager.Models.Result;

namespace TaskManager.Web.Infrastructure.Middleware
{
    public class UnauthorizedApiHandler
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public UnauthorizedApiHandler(IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }

        public Task Handle(RedirectContext<CookieAuthenticationOptions> ctx)
        {
            var result = JsonConvert.SerializeObject(
                new Result
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.UserNotAuthorized,
                },
                _serializerSettings
            );
            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            ctx.Response.ContentType = "application/json; charset=utf-8";
            return ctx.Response.WriteAsync(result);
        }
    }
}
