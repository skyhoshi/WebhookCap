using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Catcher_WebAPI.Data.Diagnostics.Interceptors
{
    public class dbTransactionInterceptor : DbTransactionInterceptor
    {
        public override InterceptionResult<DbTransaction> TransactionStarting(DbConnection connection, TransactionStartingEventData eventData, InterceptionResult<DbTransaction> result)
        {
            return base.TransactionStarting(connection, eventData, result);
        }

        public override DbTransaction TransactionStarted(DbConnection connection, TransactionEndEventData eventData, DbTransaction result)
        {
            return base.TransactionStarted(connection, eventData, result);
        }

        public override async Task<InterceptionResult<DbTransaction>> TransactionStartingAsync(DbConnection connection, TransactionStartingEventData eventData, InterceptionResult<DbTransaction> result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.TransactionStartingAsync(connection, eventData, result, cancellationToken);
        }

        public override async Task<DbTransaction> TransactionStartedAsync(DbConnection connection, TransactionEndEventData eventData, DbTransaction result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.TransactionStartedAsync(connection, eventData, result, cancellationToken);
        }

        public override DbTransaction TransactionUsed(DbConnection connection, TransactionEventData eventData, DbTransaction result)
        {
            return base.TransactionUsed(connection, eventData, result);
        }

        public override async Task<DbTransaction> TransactionUsedAsync(DbConnection connection, TransactionEventData eventData, DbTransaction result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.TransactionUsedAsync(connection, eventData, result, cancellationToken);
        }

        public override InterceptionResult TransactionCommitting(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
        {
            return base.TransactionCommitting(transaction, eventData, result);
        }

        public override void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
        {
            base.TransactionCommitted(transaction, eventData);
        }

        public override async Task<InterceptionResult> TransactionCommittingAsync(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.TransactionCommittingAsync(transaction, eventData, result, cancellationToken);
        }

        public override async Task TransactionCommittedAsync(DbTransaction transaction, TransactionEndEventData eventData, CancellationToken cancellationToken = new CancellationToken())
        {
            await base.TransactionCommittedAsync(transaction, eventData, cancellationToken);
        }

        public override InterceptionResult TransactionRollingBack(DbTransaction transaction, TransactionEventData eventData,
            InterceptionResult result)
        {
            return base.TransactionRollingBack(transaction, eventData, result);
        }

        public override void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
        {
            base.TransactionRolledBack(transaction, eventData);
        }

        public override async Task<InterceptionResult> TransactionRollingBackAsync(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.TransactionRollingBackAsync(transaction, eventData, result, cancellationToken);
        }

        public override async Task TransactionRolledBackAsync(DbTransaction transaction, TransactionEndEventData eventData, CancellationToken cancellationToken = new CancellationToken())
        {
            await base.TransactionRolledBackAsync(transaction, eventData, cancellationToken);
        }

        public override void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
        {
            base.TransactionFailed(transaction, eventData);
        }

        public override async Task TransactionFailedAsync(DbTransaction transaction, TransactionErrorEventData eventData, CancellationToken cancellationToken = new CancellationToken())
        {
            await base.TransactionFailedAsync(transaction, eventData, cancellationToken);
        }
    }
}