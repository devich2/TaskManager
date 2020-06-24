﻿using System;
using System.Collections.Generic;
 using System.Linq;
 using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Abstract.Cache;
using TaskManager.Bll.Abstract.Permission;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Enum;

namespace TaskManager.Bll.Impl.Services.Cache
{
    public class PermissionCache: IPermissionCache
    {
        private readonly Dictionary<string, List<PermissionType>> _roleToPerms;
        public PermissionCache(IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var permissionRepository = scope.ServiceProvider.GetService<IPermissionRepository>();
                _roleToPerms = permissionRepository.GetPermissionsGroupedByRole();
            }
        }
        public List<PermissionType> GetFromCache(string role)
        {
            return _roleToPerms.TryGetValue(role, out List<PermissionType> permissions) ? permissions : default;
        }
    }
}