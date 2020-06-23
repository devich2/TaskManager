using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;
using TaskManager.Models.ProjectMember;

namespace TaskManager.Dal.Abstract.IRepository.FiltersAndSorting
{
    public interface IOrderQueryFactory
    {
        Tuple<Expression<Func<Unit, object>>, SortingType> GetUnitOrderQuery(UnitType ut, KeyValuePair<string, SortingType> item);
        Tuple<Func<ProjectMemberDisplayModel, object>, SortingType> GetProjectMemberOrderQuery(KeyValuePair<string, SortingType> item);
    }
}
