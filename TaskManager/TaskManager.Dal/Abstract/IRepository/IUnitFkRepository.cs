using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository.Base;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface IUnitFkRepository<TEntity>: IGenericKeyRepository<int, TEntity>
    {
        Task<TEntity> GetByUnitIdAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
