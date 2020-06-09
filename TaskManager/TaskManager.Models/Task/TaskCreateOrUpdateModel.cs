using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using TaskManager.Entities.Tables;

namespace TaskManager.Models.Task
{
    public class TaskCreateOrUpdateModel
    {
        public int? AssignedId { get; set; }
        public List<string> Tags { get; set; }
    }
}
