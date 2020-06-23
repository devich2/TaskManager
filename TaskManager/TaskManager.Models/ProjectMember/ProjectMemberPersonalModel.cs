using System;

namespace TaskManager.Models.ProjectMember
{
    public class ProjectMemberPersonalModel
    {
        public DateTimeOffset LastLoginDate {get; set;}
        public DateTimeOffset GiveAccess {get;set;}
    }
}