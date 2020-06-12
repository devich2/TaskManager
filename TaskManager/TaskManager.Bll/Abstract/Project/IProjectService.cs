using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models;
using TaskManager.Models.Project;
using TaskManager.Models.Result;
using TaskManager.Models.Task;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Project
{
    public interface IProjectService
    {
        System.Threading.Tasks.Task<DataResult<UnitSelectionModel>> GetProjectDetails(int unitId, int userId);
        System.Threading.Tasks.Task<ProjectPreviewModel> GetPreviewModel(Entities.Tables.Unit unit, int userId);
    }
}
