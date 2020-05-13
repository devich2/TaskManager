using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Entities.Tables
{
    public class Task
    {
       public int Id { get; set; }
       public int ProjectId { get; set; }
       public Project Project { get; set; }
       public int? AssignedId { get; set; }
       [AllowNull]
       public ProjectMember Assigned { get; set; }
       public int UnitId { get; set; }
       public Unit Unit { get; set; }
       public ICollection<TagOnTask> TagOnTasks { get; set; }
    }
}
