using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Email;
using TaskManager.Common.Utils;
using TaskManager.Configuration;
using TaskManager.Models.Email;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services.Email
{
    public class EmailSendService : IEmailSendService
    {
        private readonly ILogger<EmailSendService> _logger;
        private readonly SmtpSendConfiguration _smtpSendConfiguration;

        public EmailSendService(IOptions<SmtpSendConfiguration> options,
            ILogger<EmailSendService> logger)
        {
            _logger = logger;
            _smtpSendConfiguration = options.Value;
        }

        public async Task<Result> SendEmailAsync(List<EmailMessage> messages)
        {
            List<MailMessage> mailObjs = messages.Select(BuildMessage).ToList();
            return await SendEmailAsync(mailObjs);
        }

        private async Task<Result> SendEmailAsync(List<MailMessage> messages)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient
                {
                    Host = _smtpSendConfiguration.Host,
                    Port = _smtpSendConfiguration.Port,
                    EnableSsl = _smtpSendConfiguration.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential
                    (_smtpSendConfiguration.DefaultEmailFrom,
                        _smtpSendConfiguration.Password)
                })
                {
                    foreach (var ms in messages)
                    {
                        await smtpClient.SendMailAsync(ms);
                    }

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
                        new AsJsonFormatter(messages)
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
            string email = _smtpSendConfiguration.DefaultEmailFrom;
            MailAddress from = new MailAddress(email,
                _smtpSendConfiguration.DefaultNameFrom);
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