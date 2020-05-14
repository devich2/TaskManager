using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IPermissionRepository: IGenericKeyRepository<int, Permission>
    {
    }
}
