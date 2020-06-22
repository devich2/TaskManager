using System;
using System.Collections.Generic;

namespace TaskManager.Models.Task
{
    public class TaskExpirationModel
    {
        public int AssigneeId { get; set; }
        public string Assignee { get; set; }
        public string Email { get; set; }
        public List<TaskExpirationItemModel> ProjectTasks { get; set; }
    }

    public class TaskExpirationItemModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }

    public class TaskModel
    {
        public string Creator { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTimeOffset DueTs { get; set; }
    }
}