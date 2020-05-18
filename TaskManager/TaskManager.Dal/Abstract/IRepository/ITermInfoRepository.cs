using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface ITermInfoRepository: IUnitFkRepository<TermInfo>
    {
        Task<int> GetPostDueCount(List<int> unitIds);
        Task<int> GetByStatusCount(List<int> unitIds, Status status);
    }
}
