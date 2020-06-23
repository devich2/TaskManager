using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.Email;
using TaskManager.Email.Template.Engine.Views.Email;
using TaskManager.Email.Template.Engine.Views.Email.Test;

namespace TaskManager.Web.Controllers
{
    [Route("api/test/")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;

        public TestController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }
        [HttpPost]
        public async Task Post()
        {
            await _emailTemplateService.TemplateEmailMessage(new List<EmailTemplateModel>()
            {
                new EmailTemplateModel()
                {
                    Email = "olarevun23@gmail.com",
                    Model = new TestModel(){Name =  "dev"}
                }
            });
        }
    }
}