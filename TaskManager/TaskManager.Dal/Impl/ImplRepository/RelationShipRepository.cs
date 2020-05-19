using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class RelationShipRepository: UnitFkRepository<RelationShip>, IRelationShipRepository
    {
        public RelationShipRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<List<Unit>> GetSubUnitsByParentId(int unitId)
        {
            return await Context.RelationShips
                .Include(x => x.Unit)
                 .ThenInclude(x=>x.TermInfo)
                    .Where(x => x.ParentUnitId == unitId)
                .Select(x => x.Unit).ToListAsync();
        }
    }
}
