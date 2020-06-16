using System.Collections.Generic;

namespace TaskManager.Bll.Abstract.Email
{
    public interface IEmailTemplateService
    {
        System.Threading.Tasks.Task TemplateEmailMessage(string email, ParentViewModel model);
        System.Threading.Tasks.Task TemplateEmailStringAttachment(string email, ParentViewModel model,List<string> files);
    }
}