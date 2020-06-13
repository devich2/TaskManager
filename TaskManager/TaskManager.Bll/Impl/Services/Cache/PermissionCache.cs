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
      
        public PermissionCache(IServiceProvider sp, 
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            using (var scope = sp.CreateScope())
            {
                var permissionRepository = scope.ServiceProvider.GetService<IPermissionRepository>();
                Initialize(permissionRepository.GetRolesGroupedByPermissions());
            }
        }
        public List<string> GetFromCache(PermissionType permissionType)
        {
            string cacheKey = GetCacheKey(permissionType);
            _memoryCache.TryGetValue(cacheKey, out List<string> roles);
            return roles;
        }
        private void Initialize(Dictionary<PermissionType, List<string>> permissionDict)
        {
            foreach(KeyValuePair<PermissionType, List<string>> entry in permissionDict)
            {
                _memoryCache.Set(GetCacheKey(entry.Key), entry.Value);
            }
        }
        private string GetCacheKey(PermissionType permissionType)
        {
            return $"{CacheKeys.Permission}{permissionType}";
        }
    }
}