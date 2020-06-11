using TaskManager.Models.Result;

namespace TaskManager.Bll.Abstract.Tag
{
    public interface ITagService
    {
        System.Threading.Tasks.Task<Result> AddTag(int taskId, int tagId);
        System.Threading.Tasks.Task<Result> RemoveTag(int taskId, int tagId);
    }
}