using Newtonsoft.Json.Linq;
using TaskManager.Entities.Enum;
using TaskManager.Models.TermInfo;

namespace TaskManager.Models.Unit
{
    public class UnitModel: UnitBaseModel, IUnitAuth
    {
        public TermInfoCreateModel TermInfo { get; set; }
        public int? ParentId { get; set; }
    }
}