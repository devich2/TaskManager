using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class RelationShipRepository: GenericKeyRepository<int, RelationShip>, IRelationShipRepository
    {
        public RelationShipRepository(TaskManagerDbContext context) : base(context)
        {
        }
    }
}
