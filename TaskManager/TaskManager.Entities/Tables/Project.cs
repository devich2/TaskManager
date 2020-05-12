using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class Project
    {
        public int Id { get; set; }
        public int ProjectManagerId { get; set; }
        public User ProjectManager { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public int Members { get; set; }
        public string ProjectIdentifier { get; set; }
    }
}
