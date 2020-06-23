using System;
using System.Collections.Generic;

namespace TaskManager.Email.Template.Engine.Views.Email.TaskExpiration
{
    public class TaskExpirationEmailModel: ParentViewModel
    {
        public string Assignee { get; set; }
        public List<TaskExpirationEmailItemModel> ProjectTasks { get; set; }
    }
    public class TaskExpirationEmailItemModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<TaskExpirationSelectionModel> Tasks {get; set;}
       
    }
    public class TaskExpirationSelectionModel
    {
        public string Creator { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public bool Expired {get; set;}
        public DateTimeOffset DueTs { get; set; }
    }
}