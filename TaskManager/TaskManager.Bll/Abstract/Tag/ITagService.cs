using System.Collections.Generic;
using TaskManager.Models.Result;
using TaskManager.Models.Tag;

namespace TaskManager.Bll.Abstract.Tag
{
    public interface ITagService
    {
        System.Threading.Tasks.Task<Result> UpdateTags(TagUpdateModel model);
        System.Threading.Tasks.Task<DataResult<List<TagModel>>> GetTags();
        System.Threading.Tasks.Task<DataResult<List<string>>> GetTagsByTaskId(int taskId);
    }
}