using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Tables;
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

        public async Task<List<Task>> GetTasksByMileStoneId(int unitId)
        {
            return await Context.Tasks.Join(Context.MileStones.Where(x => x.UnitId == unitId), 
            x => x.MileStoneId, y => y.Id,
                (x, y) => x).ToListAsync();
        }
    }
}