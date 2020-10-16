using Hasso.Cli.Split;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;
using System.CommandLine.DragonFruit;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    class App
    {

        private readonly IServiceProvider serviceProvider;
        private static readonly RootCommand rootCommand = new RootCommand();


        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSplitters()
                .AddFragmentWriter()
                .AddCommandHandlers();
        }

        internal void ConfigureCommands()
        {
            void AddCommand<THandler>(string name, string? description, params string[] aliases)
            {
                aliases = aliases ?? new string[0];

                var command = new Command(name, description);
                foreach (var alias in aliases)
                    command.AddAlias(alias);

                command.ConfigureFromMethod(
                    typeof(THandler).GetMethod("ExecuteAsync"),
                    serviceProvider.GetRequiredService<SplitCommandHandler>()
                );

                rootCommand.AddCommand(command);
            }

            AddCommand<SplitCommandHandler>("split",
                "splits monolithic yamls (scenes.yaml, scripts.yaml, ...) into many smaller ones",
                "fass!", "explode");
        }


        internal Task RunAsync(string[] args) => rootCommand.InvokeAsync(args);
    }
}
