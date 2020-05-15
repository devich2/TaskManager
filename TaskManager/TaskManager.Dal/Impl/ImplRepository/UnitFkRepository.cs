using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables.Abstract;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class UnitFkRepository<TEntity>: GenericKeyRepository<int, TEntity>,
        IUnitFkRepository<TEntity> where TEntity : class, IUnitExtensionTable
    {
        public UnitFkRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
        public async Task<TEntity> GetByUnitIdAsync(int id)
        {
            return await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.UnitId == id);
        }
    }
}
