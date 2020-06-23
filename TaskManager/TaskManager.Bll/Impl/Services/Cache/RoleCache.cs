using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Bll.Abstract.Cache;
using TaskManager.Dal;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Bll.Impl.Services.Cache
{
    public class RoleCache: IRoleCache
    {
        private readonly Dictionary<string, decimal> _roleRanks;
        public RoleCache(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<TaskManagerDbContext>();
                _roleRanks = dbContext.Roles.ToDictionary(x=>x.Name, x=>x.Rank);
            }
        }
        
        public decimal GetRankByRoleName(string name)
        {
            return _roleRanks.TryGetValue(name, out decimal rank) ? rank : default;
        }
    }
}