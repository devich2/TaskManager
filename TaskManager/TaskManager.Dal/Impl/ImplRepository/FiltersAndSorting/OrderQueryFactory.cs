using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.Dal.Abstract.IRepository.FiltersAndSorting;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;
using TaskManager.Models.ProjectMember;

namespace TaskManager.Dal.Impl.ImplRepository.FiltersAndSorting
{
    public class OrderQueryFactory: IOrderQueryFactory
    {
      
        public Tuple<Expression<Func<Unit, object>>, SortingType> GetUnitOrderQuery(UnitType ut, KeyValuePair<string, SortingType> item)
        {
            Tuple<Expression<Func<Unit, object>>, SortingType> result = null;
            switch (item.Key)
            {
                case "status":
                    result = new Tuple<Expression<Func<Unit, object>>, SortingType>
                        (p => p.TermInfo.Status, item.Value);
                    break;
                case "created":
                    result = new Tuple<Expression<Func<Unit, object>>, SortingType>
                        (p => p.TermInfo.StartTs, item.Value);
                    break;
                case "due":
                    result = new Tuple<Expression<Func<Unit, object>>, SortingType>
                        (p => p.TermInfo.DueTs, item.Value);
                    break;
            }

            return result;
        }
        
        public Tuple<Func<ProjectMemberDisplayModel, object>, SortingType> GetProjectMemberOrderQuery(KeyValuePair<string, SortingType> item)
        {
            Tuple<Func<ProjectMemberDisplayModel, object>, SortingType> result = null;
            switch (item.Key)
            {
                case "name":
                    result = new Tuple<Func<ProjectMemberDisplayModel, object>, SortingType>
                        (p => p.Name, item.Value);
                    break;
                case "access_level":
                    result = new Tuple<Func<ProjectMemberDisplayModel, object>, SortingType>
                        (p => p.Role.Rank, item.Value);
                    break;
                case "joined":
                    result = new Tuple<Func<ProjectMemberDisplayModel, object>, SortingType>
                        (p => p.Personal.GiveAccess, item.Value);
                    break;
                case "last_login":
                    result = new Tuple<Func<ProjectMemberDisplayModel, object>, SortingType>
                        (p => p.Personal.LastLoginDate, item.Value);
                    break;
            }

            return result;
        }
    }
}
