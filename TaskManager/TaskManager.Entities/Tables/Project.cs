using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Entities.Tables.Abstract;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class Project: IUnitExtensionTable
    {
        public int ProjectManagerId { get; set; }
        
        public User ProjectManager { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public int Members { get; set; }
    }
}
