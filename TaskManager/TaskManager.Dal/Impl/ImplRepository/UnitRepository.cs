using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class UnitRepository : UnitFkRepository<Unit>, IUnitRepository
    {
        public UnitRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<List<Unit>> SelectByType(UnitType type,
            PagingOptions po,
            IQueryable<int> unitFilterQuery,
            List<Tuple<Expression<Func<Unit, object>>, SortingType>> orderByQuery)
        {
            IQueryable<Unit> entityQuery = Context.Units
                .Where(unit => unit.UnitType == type);

            if (unitFilterQuery != null)
            {
                entityQuery = entityQuery
                    .Where(q => unitFilterQuery.Contains(q.UnitId));
            }

            entityQuery = ExpandQuery(type, entityQuery);

            if (orderByQuery != null &&
                orderByQuery.Any())
            {
                foreach (var (item1, item2) in orderByQuery)
                {
                    switch (item2)
                    {
                        case SortingType.Asc:
                            entityQuery = entityQuery.OrderBy(item1);
                            break;
                        case SortingType.Desc:
                            entityQuery = entityQuery.OrderByDescending(item1);
                            break;
                    }
                }
            }

            if (po != null)
            {
                entityQuery = entityQuery.Skip(po.StartIndex).Take(po.Count);
            }

            return await entityQuery.ToListAsync();
        }

        public async Task<Unit> SelectExpandedByUnitIdAndType(UnitType type, int unitId)
        {
            IQueryable<Unit> entityQuery = Context.Units
                .Where(unit => unit.UnitType == type && unit.UnitId == unitId);
            return await ExpandQuery(type, entityQuery).FirstOrDefaultAsync();
        }
        
        private IQueryable<Unit> ExpandQuery(UnitType unitType, IQueryable<Unit> entityQuery)
        {
            entityQuery = entityQuery.Include(x => x.Creator)
                .Include(x => x.TermInfo);
            switch (unitType)
            {
                case UnitType.Milestone:
                    entityQuery = entityQuery.Include(x => x.MileStone)
                        .ThenInclude(x => x.Tasks);
                    break;
                case UnitType.Project:
                    entityQuery = entityQuery.Include(x => x.Project)
                        .Include(x => x.Children)
                        .ThenInclude(x => x.TermInfo);
                    break;
                case UnitType.Task:
                    entityQuery = entityQuery.Include(x => x.Task.Assigned)
                        .Include(x => x.Children).ThenInclude(x => x.TermInfo);
                    break;
            }
            return entityQuery;
        }

        public async Task<int> SelectByTypeCount(UnitType type, IQueryable<int> unitFilterQuery)
        {
            IQueryable<Unit> entityQuery = Context.Units.Where(x => x.UnitType == type);

            if (unitFilterQuery != null)
            {
                entityQuery = entityQuery
                    .Where(x => unitFilterQuery.Contains(x.UnitId));
            }

            return await entityQuery.CountAsync();
        }

        public override async Task<Unit> GetByUnitIdAsync(int id)
        {
            var query = Context.Set<Unit>().Where(x => x.UnitId == id)
                .Include(x => x.TermInfo).FirstOrDefaultAsync();
            return await query;
        }

        public override async Task<List<Unit>> GetByAsync(Expression<Func<Unit, bool>> predicate)
        {
            return await Context.Units.Include(x => x.TermInfo).Where(predicate).ToListAsync();
        }

        public async Task<Dictionary<Status, List<Unit>>> GetUnitStatusListByTypeAndParent(UnitType unitType,
            int? unitParentId)
        {
            IQueryable<Unit> entityQuery = Context.Units.Include(x => x.TermInfo).Where(x => x.UnitType == unitType);
            if (unitParentId.HasValue)
            {
                entityQuery = entityQuery.Where(x => x.UnitParentId == unitParentId.Value);
            }

            return await entityQuery
                .GroupBy(x => x.TermInfo.Status)
                .Select(x => new {x.Key, L = x.ToList()})
                .ToDictionaryAsync(x => x.Key, x => x.L);
        }

        public async Task<Dictionary<Status, int>> GetUnitStatusCountByTypeAndParent(UnitType unitType,
            int? unitParentId)
        {
            IQueryable<Unit> entityQuery = Context.Units.Include(x => x.TermInfo).Where(x => x.UnitType == unitType);
            if (unitParentId.HasValue)
            {
                entityQuery = entityQuery.Where(x => x.UnitParentId == unitParentId.Value);
            }

            return await entityQuery
                .GroupBy(x => x.TermInfo.Status)
                .Select(x => new {x.Key, Count = x.Count()})
                .ToDictionaryAsync(x => x.Key, x => x.Count);
        }
    }
}