using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models.TermInfo;

namespace TaskManager.Models.Unit
{
    public class UnitCreateOrUpdateModel
    {
        public TermInfoCreateModel TermInfo { get; set; }
        public UnitStateModel UnitStateModel { get; set; }
        public int UserId { get; set; }
        public int UnitId { get; set; }
    }
}
