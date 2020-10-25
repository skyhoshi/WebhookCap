using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Catcher_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //This fixes an issue with IIS that if the log folder doesn't exist at runtime then the event viewer will show an error every so often that could be interpreted as a application failure.
            if (!System.IO.Directory.Exists("./log/"))
            {
                System.IO.Directory.CreateDirectory("./log/");
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIIS();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.CaptureStartupErrors(true);
                    webBuilder.ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Information));
                });
    }
}
