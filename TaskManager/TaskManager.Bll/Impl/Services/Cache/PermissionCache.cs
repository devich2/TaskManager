﻿using System;
using System.Collections.Generic;
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
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<PermissionType, List<string>> _permsToRoles;
        public PermissionCache(IServiceProvider sp)
        {
           
            using (var scope = sp.CreateScope())
            {
                _memoryCache = scope.ServiceProvider.GetService<IMemoryCache>();
                var permissionRepository = scope.ServiceProvider.GetService<IPermissionRepository>();
                _permsToRoles = permissionRepository.GetRolesGroupedByPermissions();
            }
        }
        public List<string> GetFromCache(PermissionType permissionType)
        {
            return _permsToRoles[permissionType];
        }
    }
}