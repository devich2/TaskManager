using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
