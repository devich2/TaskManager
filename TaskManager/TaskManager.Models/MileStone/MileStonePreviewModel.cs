using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.MileStone
{
    public class MileStonePreviewModel
    {
        public int Total { get; set; }
        public decimal CompletedPercentage { get; set; }
        public int ClosedTasksCount { get; set; }
    }
}
