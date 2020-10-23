using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Catcher_WebAPI.Data.CanvasLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Catcher_WebAPI.Middleware
{
    public partial class RequestResponseLogger
    {

        private readonly RequestDelegate _next;

        public RequestResponseLogger(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext, GoCanvasLoggingDbContext dbContext)
        {
            if (httpContext.Request.Path.HasValue)
            {
                if (httpContext.Request.Path.Value.Contains("api/submission/new"))
                {
                    RequestToLog rtl = new RequestToLog(httpContext);
                    rtl.Save(dbContext);
                    MemoryStream ms = (MemoryStream)httpContext.Request.Body;
                    ms.Seek(0, SeekOrigin.Begin);
                    httpContext.Request.Body = ms;
                }
            }

            await _next(httpContext);
        }
    }
}
