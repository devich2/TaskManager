using System.Collections.Generic;
using TaskManager.Bll.Abstract.Email;
using TaskManager.Email.Template.Engine.Services;
using TaskManager.Email.Template.Engine.Views.Email;
using TaskManager.Email.Template.Engine.Views.Email.ProjectInvitation;
using TaskManager.Email.Template.Engine.Views.Email.TaskExpiration;
using TaskManager.Email.Template.Engine.Views.Email.Test;
using TaskManager.Models.Email;

namespace TaskManager.Bll.Impl.Services.Email
{
    public class EmailTemplateService: IEmailTemplateService
    {
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        private readonly IEmailSendService _emailSendService;
        
        private const string TestPath = "/Views/Email/Test/Test";
        private const string DigestPath = "/Views/Email/TaskExpiration/TaskExpiration";
        private const string InvitationPath = "/Views/Email/ProjectInvitation/Invitation";
        public EmailTemplateService(IRazorViewToStringRenderer razorViewToStringRenderer, IEmailSendService emailSendService)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
            _emailSendService = emailSendService;
        }
        
        public async System.Threading.Tasks.Task TemplateEmailMessage(List<EmailTemplateModel> templates)
        {
            List<EmailMessage> messages = new List<EmailMessage>();
            foreach (var item in templates)
            {
                var emailMessage = new EmailMessage();
                string path = string.Empty;
                switch (item.Model)
                {
                    case TestModel _:
                        path = TestPath;
                        emailMessage.Subject = "Hello";
                        break;
                    case TaskExpirationEmailModel _:
                        path = DigestPath;
                        emailMessage.Subject = "Daily Digest";
                        break;
                    case ProjectInvitationModel _:
                        path = InvitationPath;
                        emailMessage.Subject = "Project Invitation";
                        break;
                }
                string body = await _razorViewToStringRenderer
                    .RenderViewToStringAsync(path, item.Model);

                emailMessage.EmailTo = item.Email;
                emailMessage.Message = body;
                messages.Add(emailMessage);
            }
            await _emailSendService.SendEmailAsync(messages);
        }
    }
}