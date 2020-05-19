using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IRelationShipRepository: IUnitFkRepository<RelationShip>
    {
        Task<List<Unit>> GetSubUnitsByParentId(int unitId);
    }
}
