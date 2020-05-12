using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManager.Web.Infrastructure
{
    public class ClaimsAccessAttribute : IAuthorizationFilter
    {
        public string Issuer { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
