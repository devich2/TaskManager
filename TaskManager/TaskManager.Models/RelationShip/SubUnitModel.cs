using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Models.User;

namespace TaskManager.Models.RelationShip
{
    public class SubUnitModel
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public UnitType UnitType { get; set; }
        public UserModel Creator { get; set; }
        public DateTimeOffset CreatedTs { get; set; }
        public Status Status { get; set; }
    }
}
