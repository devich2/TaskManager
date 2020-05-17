using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IUnitRepository: IUnitFkRepository<Unit>
    {
        public Task<List<Unit>> SelectByType(UnitType type,
            PagingOptions po,
            IQueryable<int> unitFilterQuery,
            List<Tuple<Expression<Func<Unit, object>>, SortingType>> orderByQuery);
        public Task<int> SelectByTypeCount(UnitType type, IQueryable<int> unitFilterQuery);
    }
}
