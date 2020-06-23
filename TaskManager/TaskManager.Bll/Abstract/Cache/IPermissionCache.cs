using System.Collections.Generic;
using TaskManager.Entities.Enum;

namespace TaskManager.Bll.Abstract.Cache
{
    public interface IPermissionCache
    {
        public List<string> GetFromCache(PermissionType permissionType);
    }
}