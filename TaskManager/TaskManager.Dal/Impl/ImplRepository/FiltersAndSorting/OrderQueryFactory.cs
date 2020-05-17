using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.Dal.Abstract.IRepository.FiltersAndSorting;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;

namespace TaskManager.Dal.Impl.ImplRepository.FiltersAndSorting
{
    public class OrderQueryFactory: IOrderQueryFactory
    {
      
        public Tuple<Expression<Func<Unit, object>>, SortingType> GetOrderQuery(UnitType ut, KeyValuePair<string, SortingType> item)
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
    }
}
