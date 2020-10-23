using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Catcher_WebAPI.Data.Diagnostics.Interceptors
{
    public class dbConnectionInterceptor : DbConnectionInterceptor
    {
        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            return base.ConnectionOpening(connection, eventData, result);
        }

        public override async Task<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
        }

        public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        {
            base.ConnectionOpened(connection, eventData);
        }

        public override async Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData, CancellationToken cancellationToken = new CancellationToken())
        {
            await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
        }

        public override InterceptionResult ConnectionClosing(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            return base.ConnectionClosing(connection, eventData, result);
        }

        public override async Task<InterceptionResult> ConnectionClosingAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            return await base.ConnectionClosingAsync(connection, eventData, result);
        }

        public override void ConnectionClosed(DbConnection connection, ConnectionEndEventData eventData)
        {
            base.ConnectionClosed(connection, eventData);
        }

        public override async Task ConnectionClosedAsync(DbConnection connection, ConnectionEndEventData eventData)
        {
            await base.ConnectionClosedAsync(connection, eventData);
        }

        public override void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
        {
            base.ConnectionFailed(connection, eventData);
        }

        public override async Task ConnectionFailedAsync(DbConnection connection, ConnectionErrorEventData eventData, CancellationToken cancellationToken = new CancellationToken())
        {
            await base.ConnectionFailedAsync(connection, eventData, cancellationToken);
        }
    }
}