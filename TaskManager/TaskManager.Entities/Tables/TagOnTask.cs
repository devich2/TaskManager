using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities.Tables
{
    public class TagOnTask
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
