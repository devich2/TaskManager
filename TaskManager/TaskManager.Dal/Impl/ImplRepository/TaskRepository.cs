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
using TaskManager.Models.Task;
using TaskAlias = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class TaskRepository : UnitFkRepository<TaskAlias>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<List<TaskExpirationModel>> GetActiveTasksInDuePeriod(DateTimeOffset toD)
        {
            var list = await (from t in Context.Tasks
                join u in Context.Units on t.UnitId equals u.UnitId
                join tm in Context.TermInfos on t.UnitId equals tm.UnitId
                join a in Context.Users on t.AssignedId equals a.Id
                join c in Context.Users on u.CreatorId equals c.Id
                join p in Context.Units on u.UnitParentId equals p.UnitId
                where t.AssignedId != null && tm.DueTs != null && tm.Status != Status.Closed && tm.DueTs < toD
                select new {Task = t, Unit = u, TermInfo = tm, Assigned = a, CreatedBy = c, Project = p}).ToListAsync();
            
            return list.GroupBy(x => x.Assigned.Id).Select(x=>
            {
                var first = x.First();
                return new TaskExpirationModel()
                {
                    AssigneeId = first.Assigned.Id,
                    Assignee = first.Assigned.Name,
                    Email = first.Assigned.Email,
                    ProjectTasks = x.GroupBy(p=>p.Project.UnitId).Select(r=>
                    {
                        var proj = r.First();
                        return new TaskExpirationItemModel()
                        {
                            ProjectId = r.Key,
                            ProjectName = proj.Project.Name,
                            Tasks = r.Select(z=>new TaskModel()
                            {
                                Creator = z.CreatedBy.Name,
                                TaskId = z.Task.UnitId,
                                TaskName = z.Unit.Name,
                                DueTs = z.TermInfo.DueTs.Value
                            }).ToList()
                            
                        };
                    }).ToList()
                };
            }).ToList();
        }

        public override async Task<TaskAlias> GetByUnitIdAsync(int id)
        {
            var query = Context.Set<TaskAlias>().AsNoTracking().Where(x => x.UnitId == id)
                .Include(x => x.Unit.TermInfo)
                .Include(x => x.Unit.Children)
                .ThenInclude(x => x.TermInfo);
            return await query.FirstOrDefaultAsync();
        }

        public override async Task<TaskAlias> GetByIdAsync(int id)
        {
            var query = Context.Tasks.Include(x => x.Unit).Where(x => x.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}