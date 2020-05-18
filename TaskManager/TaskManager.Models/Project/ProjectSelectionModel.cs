using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models.Role;

namespace TaskManager.Models.Project
{
    public class ProjectSelectionModel
    {
        public int Id { get; set; }
        public HashSet<RoleModel> Permissions { get; set; }
        public int Members { get; set; }
        public int TasksCount { get; set; }
    }
}
