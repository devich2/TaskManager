using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;

namespace TaskManager.Models.TermInfo
{
    public class TermInfoSelectionModel
    {
        public DateTimeOffset CreatedTs { get; set; }
        public DateTimeOffset? DueTs { get; set; }
        public bool Expired { get; set; }
        public Status Status { get; set; }
    }
}
