using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Dal.Abstract.IRepository.Base
{
    public interface IGenericKeyRepository<in TKey, TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> GetCountAsync();
    }
}
