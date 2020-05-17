using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;
using Task = System.Threading.Tasks.Task;
using TaskAlias = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class TagOnTaskRepository: GenericKeyRepository<int, TagOnTask>, ITagOnTaskRepository
    {
        public TagOnTaskRepository(TaskManagerDbContext context) : base(context)
        {
        }
        public async Task AddToTags(int taskId, List<string> tags)
        {
            foreach (var tag in tags)
            {
                Tag entity = await Context.Tags.FirstOrDefaultAsync(x => x.TextValue == tag);
                if (entity != null)
                {
                    await Context.TagOnTasks.AddAsync(new TagOnTask()
                    {
                        TaskId = taskId,
                        TagId = entity.Id
                    });
                }
            }
        }
    }
}
