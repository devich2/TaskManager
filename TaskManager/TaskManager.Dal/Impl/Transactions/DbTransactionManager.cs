using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract.Transactions;

namespace TaskManager.Dal.Impl.Transactions
{
    public class DbTransactionManager: ITransactionManager
    {
        private readonly TaskManagerDbContext _context;
        private readonly ILogger<DbTransactionManager> _logger;

        public DbTransactionManager(TaskManagerDbContext context, ILogger<DbTransactionManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<T> ExecuteInTransactionAsync<T>(Func<ITransaction, Task<T>> callback)
        {
            return _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
            {
                await using (IDbContextTransaction transaction =
                    await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var txWrapper = new DbTransactionWrapper(transaction);
                        T res = await callback(txWrapper);

                        if (!txWrapper.Finished)
                        {
                            _logger.LogError(
                                "Explicit transaction was neither closed nor rolled back, action: {0}",
                                callback
                            );
                        }

                        return res;
                    }
                    catch (Exception e)
                    {
                        _logger.LogW(e, "Uncaught exception in transaction block");
                        throw;
                    }
                }
            });
        }

        public Task ExecuteInTransactionAsync(Func<ITransaction, Task> callback)
        {
            return _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
            {
                await using IDbContextTransaction transaction =
                    await _context.Database.BeginTransactionAsync();
                try
                {
                    var txWrapper = new DbTransactionWrapper(transaction);
                    await callback(txWrapper);
                    if (!txWrapper.Finished)
                    {
                        _logger.LogError(
                            "Explicit transaction was neither closed nor rolled back, action: {0}",
                            callback
                        );
                    }
                }
                catch (Exception e)
                {
                    _logger.LogW(e, "Uncaught exception in transaction block");
                    throw;
                }
            });
        }

        public Task<T> ExecuteInImplicitTransactionAsync<T>(Func<Task<T>> callback)
        {
            return ExecuteInTransactionAsync(async tx =>
            {
                try
                {
                    _logger.LogTrace("Beginning transactional execution of {0}", callback);
                    T val = await callback();

                    _logger.LogTrace("Commiting transaction for {0}", callback);
                    await tx.CommitAsync();
                    return val;
                }
                catch (Exception e)
                {
                    // We would print it twice, as we have catch in ExecuteInTransactionAsync method,
                    // but I guess it's fine, more warning in console, whatever!
                    _logger.LogW(e, "Rolling back transaction due to exception.");
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }

        public Task ExecuteInImplicitTransactionAsync(Func<Task> callback)
        {
            return ExecuteInTransactionAsync(async tx =>
            {
                try
                {
                    _logger.LogTrace("Beginning transactional execution of {0}", callback);
                    await callback();

                    _logger.LogTrace("Commiting transaction for {0}", callback);
                    await tx.CommitAsync();
                }
                catch (Exception e)
                {
                    // We would print it twice, as we have catch in ExecuteInTransactionAsync method,
                    // but I guess it's fine, more warning in console, whatever!
                    _logger.LogW(e, "Rolling back transaction due to exception");
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }
    }
}
