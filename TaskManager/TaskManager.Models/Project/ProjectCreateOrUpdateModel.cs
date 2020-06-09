using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.Project
{
    public class ProjectCreateOrUpdateModel
    {
        public int Id { get; set; }
        public int ProjectManagerId { get; set; }
        public int Members { get; set; }
    }
}
