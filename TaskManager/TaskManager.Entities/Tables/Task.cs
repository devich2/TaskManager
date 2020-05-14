using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using TaskManager.Entities.Tables.Abstract;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class Task: IUnitExtensionTable
    {
       public int Id { get; set; }
       public int ProjectId { get; set; }
       public Project Project { get; set; }
       public int? AssignedId { get; set; }
       [AllowNull]
       public User Assigned { get; set; }
       public int UnitId { get; set; }
       public Unit Unit { get; set; }
       public ICollection<TagOnTask> TagOnTasks { get; set; }
    }
}
