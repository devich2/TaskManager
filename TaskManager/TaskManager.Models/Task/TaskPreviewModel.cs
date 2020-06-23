using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models.User;

namespace TaskManager.Models.Task
{
    public class TaskPreviewModel
    {
        public int Id { get; set; }
        public UserBaseModel Assignee {get; set;}
        public int? MileStoneId {get; set;}
        public string MileStone {get; set;}
        public List<string> Tags { get; set; }
        public int SubTasksCompleted { get; set; }
        public int SubTasksTotal { get; set; }
        public int Comments { get; set; }
    }
}
