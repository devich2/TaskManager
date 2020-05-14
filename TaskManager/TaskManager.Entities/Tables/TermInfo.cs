using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;

namespace TaskManager.Entities.Tables
{
    public class TermInfo
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public DateTimeOffset StartTs { get; set; }
        public DateTimeOffset? DueTs { get; set; }
        public Status Status { get; set; }
    }
}
