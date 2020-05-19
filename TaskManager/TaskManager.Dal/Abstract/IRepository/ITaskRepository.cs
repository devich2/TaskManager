using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Enum;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface ITaskRepository: IUnitFkRepository<Task>
    {
        Task<List<Task>> GetTasksByProjectId(int projectId);
        Task<int> GetTaskSubUnitsCountByType(UnitType unitType, int taskId);
        Task<Task> GetTaskExpanded(int taskId);
    }
}
