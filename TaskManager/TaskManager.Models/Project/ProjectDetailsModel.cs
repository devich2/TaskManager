using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Models.Task;

namespace TaskManager.Models.Project
{
    public class ProjectDetailsModel: ProjectPreviewModel
    {
        public Dictionary<Status, int> TaskStatusList { get; set; }
        public List<TaskDetailsModel> Tasks { get; set; }
    }
}
