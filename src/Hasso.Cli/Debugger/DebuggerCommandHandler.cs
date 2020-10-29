using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hasso.Cli.Debugger
{
    internal class DebuggerCommandHandler
    {
        private readonly ILogger logger;
        private readonly IAppHost app;

        public DebuggerCommandHandler(ILogger logger, IAppHost app)
        {
            this.logger = logger;
            this.app = app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">defaults to "http://localhost:8234"</param>
        /// <param name="launchBrowser"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(Uri? uri = null, bool launchBrowser = true)
        {
            var hostUri = uri ?? new Uri("http://localhost:8234");

            var builder = Hasso.Debugger.App.Program.CreateHostBuilder(new string[0], webHost => webHost.UseUrls(hostUri.ToString()), logger);

            logger.Information("starting debugger web-app");

            using var host = builder.Build();

            await host.StartAsync();


            if (launchBrowser)
            {
                var startupUri = new Uri(hostUri, "/app");
                OpenBrowser(startupUri);
            }

            await app.WaitForShutdownAsync();
        }

        /// <summary>
        /// kudos Brock! https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
        /// </summary>
        /// <param name="uri"></param>
        public static void OpenBrowser(Uri uri)
        {
            var url = uri.ToString();
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
