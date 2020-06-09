using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json.Linq;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;

namespace TaskManager.Models.Unit
{
    public class UnitStateModel
    {
        public UnitModel UnitModel { get; set; }
        public JObject Data { get; set; }
        public UnitType ExtendedType { get; set; }
    }
}
