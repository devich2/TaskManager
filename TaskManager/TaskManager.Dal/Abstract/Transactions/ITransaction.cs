using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Dal.Abstract.Transactions
{
    /// <summary>
    /// You should not dispose this as it would be automatically dispose upon scope completion in
    /// <see cref="ITransactionManager"/> 
    /// </summary>

    public interface ITransaction
    {
        /// <summary>
        ///     Commits all changes made to the database in the current transaction.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Discards all changes made to the database in the current transaction.
        /// </summary>
        void Rollback();

        /// <summary>
        ///     Commits all changes made to the database in the current transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns> A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation. </returns>
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Discards all changes made to the database in the current transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns> A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation. </returns>
        Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
