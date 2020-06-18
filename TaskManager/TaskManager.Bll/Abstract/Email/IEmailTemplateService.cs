using System.Collections.Generic;
using TaskManager.Email.Template.Engine.Views.Email;

namespace TaskManager.Bll.Abstract.Email
{
    public interface IEmailTemplateService
    {
        System.Threading.Tasks.Task TemplateEmailMessage(List<EmailTemplateModel> models);
    }
}