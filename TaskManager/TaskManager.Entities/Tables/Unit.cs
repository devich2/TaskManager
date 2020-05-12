using TaskManager.Entities.Enum;

namespace TaskManager.Entities.Tables
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UnitType UnitType { get; set; }
        public TermInfo TermInfo { get; set; }
    }
}
