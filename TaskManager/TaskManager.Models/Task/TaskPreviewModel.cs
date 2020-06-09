using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.Task
{
    public class TaskPreviewModel
    {
        public int Id { get; set; }
        public int? AssignedId { get; set; }
        public string AssignedName { get; set; }
        public List<string> Tags { get; set; }
        public int SubTasksCompleted { get; set; }
        public int SubTasksTotal { get; set; }
        public int Comments { get; set; }
    }
}
