using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using TaskManager.Bll.Abstract.Email;
using TaskManager.Web.Infrastructure.Scheduler.Job.Base;

namespace TaskManager.Web.Infrastructure.Scheduler.Job.Email
{
    public class NotificationJob: INotificationJob
    {
        private readonly ILogger<NotificationJob> _logger;
        private readonly IEmailNotificationService _emailNotificationService;

        public NotificationJob(ILogger<NotificationJob> logger,
            IEmailNotificationService emailNotificationService)
        {
            _logger = logger;
            _emailNotificationService = emailNotificationService;
        }
        
        public async Task Run(IJobCancellationToken jobCancellationToken)
        {
            jobCancellationToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Notification job starts....");
            await _emailNotificationService.NotifyTaskExpiration();
            _logger.LogInformation("Notification job completed....");
        }
    }
    public interface INotificationJob: IJob
    {
    }
}