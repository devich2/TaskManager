using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.TermInfo;
using TaskManager.Models.User;

namespace TaskManager.Models.Unit
{
    public class UnitSelectionModel: UnitBaseModel
    {
        public int UnitId {get;set;}
        public TermInfoSelectionModel TermInfo { get; set; }
        public UserBaseModel Creator { get; set; }
    }
}
