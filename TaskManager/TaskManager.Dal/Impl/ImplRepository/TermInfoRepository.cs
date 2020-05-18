using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class TermInfoRepository: UnitFkRepository<TermInfo>, ITermInfoRepository
    {
        public TermInfoRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<int> GetPostDueCount(List<int> unitIds)
        {
            var termInfos = 
                Context.TermInfos
                    .Where(x => unitIds.ToArray().Contains(x.UnitId) && 
                                x.DueTs < DateTimeOffset.Now);
            return await termInfos.CountAsync();
        }

        public async Task<int> GetByStatusCount(List<int> unitIds, Status status)
        {
            var termInfos =
                Context.TermInfos
                    .Where(x => unitIds.ToArray().Contains(x.UnitId) &&
                                x.Status == status);
            return await termInfos.CountAsync();
        }
    }
}
