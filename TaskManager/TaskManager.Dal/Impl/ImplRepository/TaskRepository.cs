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
    public class TaskRepository: UnitFkRepository<TaskAlias>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<List<TaskAlias>> GetTasksByProjectId(int projectId)
        {
            return await Context.Tasks
                .Where(x => x.ProjectId == projectId)
                .Include(x=>x.Unit.TermInfo)
                .OrderBy(x=>x.Unit.TermInfo.StartTs)
                .ToListAsync();
        }

        public async Task<int> GetTaskSubUnitsCountByType(UnitType unitType, int taskId)
        {
            return await Context.Tasks.Where(x => x.Id == taskId)
                .Join(Context.RelationShips
                        .Include(x => x.Unit)
                        .Where(x => x.Unit.UnitType == unitType),
                    x => x.UnitId, y => y.ParentUnitId, (x, y) => y).CountAsync();
        }

        public async Task<TaskAlias> GetTaskExpanded(int taskId)
        {
            return await Context.Tasks.Include(x => x.Unit).ThenInclude(x=>x.TermInfo)
                .FirstOrDefaultAsync(x => x.Id == taskId);
        }
    }
}
