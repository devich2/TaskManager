﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class UnitRepository: UnitFkRepository<Unit>, IUnitRepository
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
                .Where(content => content.UnitType == type);

            if (unitFilterQuery != null)
            {
                entityQuery = entityQuery
                    .Where(q => unitFilterQuery.Contains(q.UnitId))
                    .Include(x=>x.TermInfo);
            }

            switch (type)
            {
                case UnitType.Comment:
                    break;
                case UnitType.Milestone:
                    entityQuery = entityQuery.Include(x => x.SubUnits);
                    break;
                case UnitType.Project:
                    entityQuery = entityQuery.Include(x => x.Project);
                    break;
                case UnitType.Task:
                    entityQuery = entityQuery.Include(x => x.Task)
                        .Include(x=>x.Creator);
                    break;
                case UnitType.SubTask:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            if (entityQuery == null)
                return null;

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
                .Include(x => x.TermInfo).FirstAsync();
            return await query;
        }
    }
}
