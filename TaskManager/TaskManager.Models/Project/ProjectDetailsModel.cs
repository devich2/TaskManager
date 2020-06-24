using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Models.Task;

namespace TaskManager.Models.Project
{
    public class ProjectDetailsModel: ProjectPreviewModel
    {
        public List<PermissionType> Permissions {get; set;}
        public int MileStoneCount {get; set;}
        public Dictionary<Status, int> TaskStatusList { get; set; }
    }
}
