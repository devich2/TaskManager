using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface ITaskRepository: IUnitFkRepository<Task>
    {
        public Task<List<Task>> GetTasksByProjectId(int projectId);
        public Task<int> GetTaskCountByProjectId(int projectId);
    }
}
