﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid Key { get; set; }
        public ICollection<RelationShip> SubUnits { get; set; }
    }
}
