using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Models.Role;

namespace TaskManager.Models.Project
{
    public class ProjectPreviewModel
    {
        public int Id { get; set; }
        public HashSet<RoleModel> Permissions { get; set; }
        public int Members { get; set; }
        public int TasksCount { get; set; }
    }
}
