using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;

namespace TaskManager.Models.TermInfo
{
    public class TermInfoCreateModel
    {
        public DateTimeOffset? DueTs { get; set; }
        public Status Status { get; set; }
    }
}
