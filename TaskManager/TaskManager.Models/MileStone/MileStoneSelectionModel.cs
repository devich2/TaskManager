using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.MileStone
{
    public class MileStoneSelectionModel
    {
        public decimal CompletedPercentage { get; set; }
        public bool Expired { get; set; }
        public int ClosedTasksCount { get; set; }
    }
}
