using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models.MileStone;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class MileStoneRepository : UnitFkRepository<MileStone>, IMileStoneRepository
    {
        public MileStoneRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public override async Task<MileStone> GetByIdAsync(int id)
        {
            return await Context.MileStones.Include(x => x.Unit).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MileStoneBaseModel>> GetActiveMilestonesByProjectId(int projectId)
        {
            var query = from m in Context.MileStones
                join u in Context.Units on m.UnitId equals u.UnitId
                join t in Context.TermInfos on m.UnitId equals t.UnitId
                where (t.Status == Status.Closed || t.Status == Status.Open) &&
                      u.UnitParentId == projectId
                select new MileStoneBaseModel()
                {
                    MileStoneId = m.Id,
                    MileStoneName = u.Name
                };
            return await query.ToListAsync();
        }

        public async Task<List<Task>> GetTasksByMileStoneId(int unitId)
        {
            return await Context.Tasks.Join(Context.MileStones.Where(x => x.UnitId == unitId),
                x => x.MileStoneId, y => y.Id,
                (x, y) => x).ToListAsync();
        }
    }
}