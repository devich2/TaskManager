using System.Collections.Generic;
using TaskManager.Models.Email;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Abstract.Email
{
    public interface IEmailSendService
    {
        System.Threading.Tasks.Task<Result> SendEmailAsync(List<EmailMessage> messages);
    }
}