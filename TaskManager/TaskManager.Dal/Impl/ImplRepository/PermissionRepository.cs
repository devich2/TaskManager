﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Resolvers;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class PermissionRepository : GenericKeyRepository<int, Permission>, IPermissionRepository
    {
        public PermissionRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public Dictionary<PermissionType, List<string>> GetRolesGroupedByPermissions()
        {
            return Context.Permissions.Include(x => x.Role)
                .AsEnumerable()
                .GroupBy(x => x.PermissionType)
                .ToDictionary(x => x.Key,
                    x => x.Select(z => z.Role.Name).ToList());
        }
    }
}