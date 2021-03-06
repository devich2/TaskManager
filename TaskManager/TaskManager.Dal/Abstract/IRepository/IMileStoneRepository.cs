﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Entities.Tables;
using TaskManager.Models.MileStone;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IMileStoneRepository: IUnitFkRepository<MileStone>
    {
        Task<List<Task>> GetTasksByMileStoneId(int unitId);
        Task<List<MileStoneBaseModel>> GetActiveMilestonesByProjectId(int projectId);
    }
}