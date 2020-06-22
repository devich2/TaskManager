namespace TaskManager.Bll.Abstract.NewsLetter
{
    public interface INewsLetterService
    {
        System.Threading.Tasks.Task NotifyTaskExpiration();
    }
}