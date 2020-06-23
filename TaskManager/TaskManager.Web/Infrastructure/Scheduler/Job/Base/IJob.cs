using System.Threading.Tasks;
using Hangfire;

namespace TaskManager.Web.Infrastructure.Scheduler.Job.Base
{
    public interface IJob
    {
        Task Run(IJobCancellationToken jobCancellationToken);
    }
}