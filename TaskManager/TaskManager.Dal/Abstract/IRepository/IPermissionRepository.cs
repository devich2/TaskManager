using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IPermissionRepository: IGenericKeyRepository<int, Permission>
    {
    }
}
