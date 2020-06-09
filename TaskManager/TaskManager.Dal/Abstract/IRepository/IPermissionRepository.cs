using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IPermissionRepository: IGenericKeyRepository<int, IdentityUserClaim<User>>
    {
    }
}
