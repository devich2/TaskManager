using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository.FiltersAndSorting;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;

namespace TaskManager.Dal.Impl.ImplRepository.FiltersAndSorting
{
    public class FilterQueryFactory: IFiltersQueryFactory
    {
        private readonly TaskManagerDbContext _dbContext;

        public FilterQueryFactory(TaskManagerDbContext context)
        {
            _dbContext = context;
        }
        public IQueryable<int> GetFilterQuery(UnitType ut, KeyValuePair<UnitFilterType, dynamic> item)
        {
            IQueryable<int> result = null;
            switch (item.Key)
            {
                case UnitFilterType.Status: 
                    Status st = (Status)item.Value;
                    result = _dbContext.TermInfos.Where(x=>x.Status == st)
                        .Select(p => p.UnitId);
                    break;
                case UnitFilterType.Assignee:
                    int assigneeId = (int)item.Value;
                    result = _dbContext.Tasks
                        .Where(p => p.AssignedId == assigneeId)
                        .Select(p => p.UnitId);
                    break;
                case UnitFilterType.MileStone:
                    int mileStoneId = (int)item.Value;
                    result = _dbContext.RelationShips.Where(x=>x.ParentUnitId == mileStoneId)
                        .Select(p => p.UnitId);
                    break;
                case UnitFilterType.Author:
                    int creatorId = (int)item.Value;
                    result = _dbContext.Units
                        .Where(p => p.UnitType == ut && p.CreatorId == creatorId)
                        .Select(p => p.UnitId);
                    break;
                case UnitFilterType.Label:
                    string tag = (string)item.Value;
                    result = _dbContext.Tasks
                        .Join(_dbContext.TagOnTasks, 
                            x => x.Id, y => y.TaskId, (x, y) => new {x.UnitId, y.TagId})
                        .Join(_dbContext.Tags.Where(x=>x.TextValue == tag),
                            x => x.TagId, y => y.Id, (x, y) => x.UnitId);
                    break;
                case UnitFilterType.Project:
                    int projectId = (int) item.Value;
                    result = _dbContext.Tasks
                        .Where(x => x.ProjectId == projectId)
                        .Select(x => x.UnitId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }
    }
}
