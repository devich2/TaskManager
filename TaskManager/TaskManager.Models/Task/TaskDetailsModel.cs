using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models.RelationShip;
using TaskManager.Models.TermInfo;
using TaskManager.Models.User;

namespace TaskManager.Models.Task
{
    public class TaskDetailsModel
    {
        public int Id { get; set; }
        public UserModel Assignee { get; set; }
        public List<string> Tags { get; set; }
        public List<SubUnitModel> Children { get; set; }
    }
}
