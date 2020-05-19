using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models.Response;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Task
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task<DataResult<UnitSelectionModel>> GetTaskDetails(int taskId);
    }
}
