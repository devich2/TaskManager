using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskManager.Bll.Abstract.Email;
using TaskManager.Common.Utils;
using TaskManager.Models.Email;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services.Email
{
     public class EmailSendService : IEmailSendService
    {
        private readonly ILogger<EmailSendService> _logger;

        public EmailSendService(
            ILogger<EmailSendService> logger)
        {
            _logger = logger;
        }

        public async Task<Result> SendEmailAsync(EmailMessage message)
        {
            MailMessage mailObj =
                BuildMessage(message);

            return await this.SendMessageAsync(mailObj);
        }

        public async  Task<Result>SendEmailAsync(EmailStringAttachment message)
        {
            MailMessage mailObj = (MailMessage)
                BuildMessage(message);


            if (message.AttachmentsFilePath != null &&
                message.AttachmentsFilePath.Count > 0)
            {
                foreach (string pathToFile in message.AttachmentsFilePath)
                {
                    mailObj.Attachments.Add(new Attachment(pathToFile, MediaTypeNames.Application.Octet));
                }
            }

            return await this.SendMessageAsync(mailObj);
        }

        private async Task<Result> SendMessageAsync(MailMessage message)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient
                {
                    Host = (string) _settingCacheService.GetParsedObject("smtp.host"),
                    Port = (int) _settingCacheService.GetParsedObject("port.smtp"),
                    EnableSsl = (bool) _settingCacheService.GetParsedObject("ssl.enable"),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential
                    ((string) _settingCacheService.GetParsedObject("email.default"),
                        (string) _settingCacheService.GetParsedObject("email.password"))
                })
                {
                    await smtpClient.SendMailAsync(message);

                    return new Result()
                    {
                        ResponseStatusType = ResponseStatusType.Succeed
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogW(ex, "SendEmail Failed  {JO}", args:
                    new object[]
                    {
                        new AsJsonFormatter(message)
                    });

                return new Result()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.InternalError
                };
            }

            ;
        }


        private MailMessage BuildMessage(EmailMessage message)
        {
            string email = (string) _settingCacheService.GetParsedObject("email.default");
            MailAddress from = new MailAddress(email,
                (string) _settingCacheService.GetParsedObject("email.name"));
            MailMessage mailObj =
                new MailMessage(email,
                    message.EmailTo,
                    message.Subject,
                    message.Message);

            mailObj.Sender = from;
            mailObj.IsBodyHtml = true;
            mailObj.BodyEncoding = Encoding.UTF8;
            mailObj.SubjectEncoding = Encoding.UTF8;
            mailObj.From = from;

            return mailObj;
        }
    }
}