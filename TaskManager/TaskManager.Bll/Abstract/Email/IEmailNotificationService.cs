namespace TaskManager.Bll.Abstract.Email
{
    public interface IEmailNotificationService
    {
        System.Threading.Tasks.Task NotifyTaskExpiration();
        System.Threading.Tasks.Task SendInvitation(int projectId, int userId);
    }
}