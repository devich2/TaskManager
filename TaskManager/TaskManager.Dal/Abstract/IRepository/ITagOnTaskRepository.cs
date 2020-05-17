using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface ITagOnTaskRepository: IGenericKeyRepository<int, TagOnTask>
    {
        public Task AddToTags(int taskId, List<String> tags);
    }
}
