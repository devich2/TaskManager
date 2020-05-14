using System.Collections.Generic;

namespace TaskManager.Entities.Tables
{
    public class Tag
    {
        public int Id { get; set; }
        public string TextValue { get; set; }
        public ICollection<TagOnTask> TagOnTasks { get; set; }
    }
}
