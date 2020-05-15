using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.Unit
{
    public class UnitCreateOrUpdateModel
    {
        public UnitStateModel UnitStateModel { get; set; }

        public int UserId { get; set; }

        public int UnitId { get; set; }
    }
}
