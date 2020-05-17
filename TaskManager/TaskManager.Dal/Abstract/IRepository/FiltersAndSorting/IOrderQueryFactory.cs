using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;

namespace TaskManager.Dal.Abstract.IRepository.FiltersAndSorting
{
    public interface IOrderQueryFactory
    {
        Tuple<Expression<Func<Unit, object>>, SortingType> GetOrderQuery(UnitType ut, KeyValuePair<string, SortingType> item);
    }
}
