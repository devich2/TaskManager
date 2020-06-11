using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class ProjectRepository : UnitFkRepository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskManagerDbContext context) : base(context)
        {
        }
        public override async Task<Project> GetByUnitIdAsync(int id)
        {
            var query = Context.Set<Project>().AsNoTracking().Where(x => x.UnitId == id)
                .Include(x => x.Unit).ThenInclude(x=>x.TermInfo).FirstAsync();
            return await query;
        }
    }
}
