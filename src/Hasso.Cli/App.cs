﻿using Hasso.Cli.Compose;
using Hasso.Cli.Debugger;
using Hasso.Cli.Split;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.CommandLine;
using System.CommandLine.DragonFruit;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    class App
    {

        private readonly IServiceProvider serviceProvider;
        private readonly RootCommand rootCommand = new RootCommand();
        private readonly ILogger logger;

        public App(ILogger logger)
        {
            this.logger = logger;

            var services = new ServiceCollection();
            services.AddSingleton(logger);
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSplitters()
                .AddComposer()
                .AddFragmentWriter()
                .AddCommandHandlers()
                .AddAppHost();
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
                    serviceProvider.GetRequiredService<THandler>()
                );

                rootCommand.AddCommand(command);
            }

            AddCommand<SplitCommandHandler>("split",
                "splits monolithic yamls (scenes.yaml, scripts.yaml, ...) into many smaller ones",
                "fass!", "explode", "-s");

            AddCommand<ComposeCommandHandler>("compose",
                "composes multiple partial-yamls into monolithic ones, ready to deploy",
                "aus!", "implode", "-c");

            AddCommand<DebuggerCommandHandler>("debugger",
                "starts a web-ui which acts as an automation stub for lights, devices, etc.",
                "-d");
        }

        internal async Task<int> RunAsync(string[] args)
        {
            Console.WriteLine(Strings.Disclaimer);
            return await rootCommand.InvokeAsync(args);
        }
    }
}
