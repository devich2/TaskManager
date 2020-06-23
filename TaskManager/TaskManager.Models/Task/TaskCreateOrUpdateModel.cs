using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using TaskManager.Entities.Tables;

namespace TaskManager.Models.Task
{
    public class TaskCreateOrUpdateModel
    {
        public int Id { get; set; }
        public int? AssignedId { get; set; }
        public int? MileStoneId { get; set; }
        public List<string> Tags { get; set; }
    }
}