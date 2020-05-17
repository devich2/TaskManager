using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using Task = System.Threading.Tasks.Task;
using TaskAlias = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class PermissionRepository: GenericKeyRepository<int, Permission>, IPermissionRepository
    {
        public PermissionRepository(TaskManagerDbContext context) : base(context)
        {
        }
    }
}
