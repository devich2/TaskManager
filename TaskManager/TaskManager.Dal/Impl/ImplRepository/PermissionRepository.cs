using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class PermissionRepository: GenericKeyRepository<int, Permission>, IPermissionRepository
    {
        public PermissionRepository(TaskManagerDbContext context) : base(context)
        {
        }
    }
}