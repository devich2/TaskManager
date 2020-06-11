using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskAlias = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class TaskRepository : UnitFkRepository<TaskAlias>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public override async Task<TaskAlias> GetByUnitIdAsync(int id)
        {
            var query = Context.Set<TaskAlias>().AsNoTracking().Where(x => x.UnitId == id)
                .Include(x => x.Unit).ThenInclude(x => x.Children).ThenInclude(x => x.TermInfo).FirstAsync();
            return await query;
        }
    }
}