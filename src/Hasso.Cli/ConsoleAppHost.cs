using System;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    class ConsoleAppHost : IAppHost
    {
        private readonly TaskCompletionSource<bool> cancellationSource = new TaskCompletionSource<bool>();

        public ConsoleAppHost()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                cancellationSource.SetResult(true);
            };
        }

        public Task WaitForShutdownAsync() => cancellationSource.Task;
    }
}
