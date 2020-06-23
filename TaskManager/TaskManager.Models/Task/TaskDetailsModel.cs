using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models.TermInfo;
using TaskManager.Models.Unit;
using TaskManager.Models.User;

namespace TaskManager.Models.Task
{
    public class TaskDetailsModel: TaskPreviewModel
    {
        public List<SubUnitModel> SubUnits  { get; set; }
    }
}
