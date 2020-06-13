using System;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class ProjectMember
    {
        public int ProjectId {get;set;}
        public Project Project {get;set;}
        public int UserId {get; set;}
        public User User {get;set;}
        
        public DateTimeOffset GivenAccess {get;set;}
    }
}