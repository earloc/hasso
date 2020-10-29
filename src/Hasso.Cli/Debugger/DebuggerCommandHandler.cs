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


        public async Task ExecuteAsync(Uri? url = null, bool launchBrowser = true)
        {
            var hostUrl = url ?? new Uri(Strings.DefaultUrl);

            var builder = Hasso.Debugger.App.Program.CreateHostBuilder(new string[0], webHost => webHost.UseUrls(hostUrl.ToString()), logger);

            logger.Information("starting debugger web-app");

            using var host = builder.Build();

            await host.StartAsync();


            if (launchBrowser)
            {
                var startupUri = new Uri(hostUrl, "/app");
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
