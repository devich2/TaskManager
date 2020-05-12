using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class ProjectMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
