using TaskManager.Entities.Enum;

namespace TaskManager.Models.Unit
{
    public interface IUnitAuth
    {
        public int? ParentId {get;set;}
        public UnitType ExtendedType {get;set;}

    }
}