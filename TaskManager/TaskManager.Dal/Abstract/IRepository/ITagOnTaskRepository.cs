using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface ITagOnTaskRepository: IGenericKeyRepository<int, TagOnTask>
    {
        public Task AddToTags(int taskId, List<String> tags);
        public Task<List<string>> GetTagsByTaskId(int taskId);
       public Task RemoveTags(int taskId, List<string> tags);
    }
}
