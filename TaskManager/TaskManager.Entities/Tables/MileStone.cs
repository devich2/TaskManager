using System.Collections.Generic;
using TaskManager.Entities.Tables.Abstract;

namespace TaskManager.Entities.Tables
{
    public class MileStone : IUnitExtensionTable
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        
        public Unit Unit { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}