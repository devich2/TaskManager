using System.Collections.Generic;
using TaskManager.Entities.Enum;

namespace TaskManager.Bll.Abstract.Cache
{
    public interface IPermissionCache
    {
        List<PermissionType> GetFromCache(string role);
    }
}