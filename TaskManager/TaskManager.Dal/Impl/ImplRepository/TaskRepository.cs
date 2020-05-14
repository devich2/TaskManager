using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class TaskRepository: GenericKeyRepository<int, Task>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<List<Task>> GetTasksByProjectId(int projectId)
        {
            return await Context.Tasks
                .Where(x => x.ProjectId == projectId)
                .Include(x=>x.Unit.TermInfo)
                .OrderBy(x=>x.Unit.TermInfo.StartTs)
                .ToListAsync();
        }
    }
}
