using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class UnitRepository: GenericKeyRepository<int, Unit>, IUnitRepository
    {
        public UnitRepository(TaskManagerDbContext context) : base(context)
        {
        }
    }
}
