using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Models.Task;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface ITaskRepository: IUnitFkRepository<Task>
    {
        Task<List<TaskExpirationModel>> GetActiveTasksInDuePeriod(DateTimeOffset toD);
    }
}
