
using Hasso.Cli;
using Hasso.Cli.Split;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSplitters(this IServiceCollection that)
        {
            that.AddTransient<IScriptSplitter, YamlScriptSplitter>();
            that.AddTransient<ISceneSplitter, YamlSceneSplitter>();
            that.AddTransient<IAutomationSplitter, YamlAutomationSplitter>();


            return that;
        }
        public static IServiceCollection AddFragmentWriter(this IServiceCollection that)
        {
            that.AddTransient<IFragmentWriter, YamlFragmentWriter>();

            return that;
        }

        public static IServiceCollection AddCommandHandlers(this IServiceCollection that)
        {
            that.AddTransient<SplitCommandHandler, SplitCommandHandler>();

            return that;
        }
    }
}
