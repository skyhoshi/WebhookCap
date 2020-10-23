using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Catcher_WebAPI.Data.Diagnostics.Interceptors
{
    public class dbCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
        {
            return base.CommandCreating(eventData, result);
        }

        public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        {
            return base.CommandCreated(eventData, result);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            return base.ReaderExecuting(command, eventData, result);
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            return base.ScalarExecuting(command, eventData, result);
        }

        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            return base.NonQueryExecuting(command, eventData, result);
        }

        public override async Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override async Task<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override async Task<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            return base.ReaderExecuted(command, eventData, result);
        }

        public override object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
        {
            return base.ScalarExecuted(command, eventData, result);
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            return base.NonQueryExecuted(command, eventData, result);
        }

        public override async Task<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override async Task<object> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override async Task<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            base.CommandFailed(command, eventData);
        }

        public override async Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = new CancellationToken())
        {
            await base.CommandFailedAsync(command, eventData, cancellationToken);
        }

        public override InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
        {
            return base.DataReaderDisposing(command, eventData, result);
        }
    }
}
