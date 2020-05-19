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

        public async Task<Project> GetProjectByParentUnitId(int unitId, UnitType unitType)
        {
            IQueryable<Project> project;
            switch (unitType)
            {
                case UnitType.Milestone:
                    project = Context.Projects.Join(Context.RelationShips.Where(x => x.UnitId == unitId),
                        x => x.UnitId, y => y.ParentUnitId, (x, y) => x);
                    break;
                case UnitType.Project:
                    project = Context.Projects.Where(x => x.UnitId == unitId);
                    break;
                case UnitType.Task:
                    project = Context.Tasks.Include(x => x.Project)
                        .Where(x => x.UnitId == unitId).Select(x=>x.Project);
                    break;
                default:
                    IQueryable<int> id = Context.RelationShips
                        .Where(x => x.UnitId == unitId)
                        .Select(x => x.ParentUnitId);
                    project = Context.Tasks
                        .Include(x => x.Project)
                        .Where(x => x.UnitId == id.FirstOrDefault())
                        .Select(x => x.Project);
                    break;
            }

            return await project.FirstOrDefaultAsync();
        }

    }
}
