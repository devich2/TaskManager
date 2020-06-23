using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Models;

namespace TaskManager.Dal.Abstract.IRepository.FiltersAndSorting
{
    public interface IFiltersQueryFactory
    {
        IQueryable<int> GetUnitFilterQuery(UnitType ut, KeyValuePair<UnitFilterType, dynamic> item);
    }
}
