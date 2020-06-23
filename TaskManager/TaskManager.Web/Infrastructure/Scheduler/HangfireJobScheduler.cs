using System;
using Hangfire;
using TaskManager.Web.Infrastructure.Scheduler.Job.Email;

namespace TaskManager.Web.Infrastructure.Scheduler
{
    public static class HangfireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {
            RecurringJob.RemoveIfExists(nameof(NotificationJob));
            RecurringJob.AddOrUpdate<NotificationJob>(x => x.Run(JobCancellationToken.Null), Cron.Daily, TimeZoneInfo.Local);
        }
    }
}