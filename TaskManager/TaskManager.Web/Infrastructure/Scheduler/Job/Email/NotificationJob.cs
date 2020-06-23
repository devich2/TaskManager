using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using TaskManager.Bll.Abstract.NewsLetter;
using TaskManager.Web.Infrastructure.Scheduler.Job.Base;

namespace TaskManager.Web.Infrastructure.Scheduler.Job.Email
{
    public class NotificationJob: INotificationJob
    {
        private readonly ILogger<NotificationJob> _logger;
        private readonly INewsLetterService _newsLetterService;

        public NotificationJob(ILogger<NotificationJob> logger,
            INewsLetterService newsLetterService)
        {
            _logger = logger;
            _newsLetterService = newsLetterService;
        }
        
        public async Task Run(IJobCancellationToken jobCancellationToken)
        {
            jobCancellationToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Notification job starts....");
            await _newsLetterService.NotifyTaskExpiration();
            _logger.LogInformation("Notification job completed....");
        }
    }
    public interface INotificationJob: IJob
    {
    }
}