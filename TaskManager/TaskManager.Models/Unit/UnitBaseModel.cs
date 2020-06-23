using Newtonsoft.Json.Linq;
using TaskManager.Entities.Enum;

namespace TaskManager.Models.Unit
{
    public class UnitBaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public JObject Data { get; set; }
        public UnitType ExtendedType { get; set; }
    }
}