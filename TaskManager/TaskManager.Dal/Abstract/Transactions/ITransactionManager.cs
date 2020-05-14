using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Dal.Abstract.Transactions
{
    public interface ITransactionManager
    {
        /// <summary>
        /// Begins transaction and executes piece of code inside of it.
        /// Provides fine grained control on transaction commit/rollback.
        /// In case of not commiting would automatically rollback.
        /// </summary>
        Task<T> ExecuteInTransactionAsync<T>(Func<ITransaction, Task<T>> callback);

        /// <summary>
        /// Begins transaction and executes piece of code inside of it.
        /// Provides fine grained control on transaction commit/rollback.
        /// In case of not commiting would automatically rollback.
        /// </summary>
        Task ExecuteInTransactionAsync(Func<ITransaction, Task> callback);

        /// <summary>
        /// Begins transaction and executes piece of code inside of it.
        /// Transaction would be automatically committed upon successful completion of callback.
        /// In case of exception transaction would be rolled back!
        /// </summary>
        Task<T> ExecuteInImplicitTransactionAsync<T>(Func<Task<T>> callback);

        /// <summary>
        /// Begins transaction and executes piece of code inside of it.
        /// Transaction would be automatically committed upon successful completion of callback.
        /// In case of exception transaction would be rolled back!
        /// </summary>
        Task ExecuteInImplicitTransactionAsync(Func<Task> callback);
    }
}
