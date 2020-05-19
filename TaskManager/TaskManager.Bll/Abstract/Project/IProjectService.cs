using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models;
using TaskManager.Models.Project;

namespace TaskManager.Bll.Abstract.Project
{
    public interface IProjectService
    {
        System.Threading.Tasks.Task<ProjectDetailsModel> GetProjectDetails(SelectionOptions options);
    }
}
