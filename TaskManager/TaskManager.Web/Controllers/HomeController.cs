using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Web.Infrastructure.Extension;

namespace TaskManager.Web.Controllers
{
    
    [Route("api/home")]
    [MinimumAgeHandler]
    [ApiController]
    public class HomeController : ControllerBase
    {
    }
}