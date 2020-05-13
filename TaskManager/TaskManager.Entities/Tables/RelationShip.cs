using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TaskManager.Entities.Enum;

namespace TaskManager.Entities.Tables
{
    public class RelationShip
    {
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public int ParentUnitId { get; set; }
        public Unit ParentUnit { get; set; }
    }
}
