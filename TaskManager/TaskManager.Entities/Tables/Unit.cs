using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables.Abstract;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class Unit: IUnitExtensionTable
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UnitType UnitType { get; set; }
        public TermInfo TermInfo { get; set; }
        public Guid Key { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public Task Task { get; set; }
        public Project Project { get; set; }
        public ICollection<RelationShip> SubUnits { get; set; }
    }
}
