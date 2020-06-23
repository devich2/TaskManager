using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;

namespace TaskManager.Models.Unit
{
    public class UnitStatusPatchModel
    { 
        public Status Status { get; set; }
        public int UserId { get; set; }
        public int UnitId { get; set; }
    }
}
