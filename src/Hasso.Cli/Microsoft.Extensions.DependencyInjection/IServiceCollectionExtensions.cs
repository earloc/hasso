
using Hasso.Cli;
using Hasso.Cli.Split;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSplitters(this IServiceCollection that)
        {
            that.AddTransient<YamlScriptSplitter, YamlScriptSplitter>();
            that.AddTransient<YamlSceneSplitter, YamlSceneSplitter>();
            that.AddTransient<YamlAutomationSplitter, YamlAutomationSplitter>();

            that.AddTransient(provider => new ISplitter[] {
                provider.GetRequiredService<YamlScriptSplitter>(),
                provider.GetRequiredService<YamlSceneSplitter>(),
                provider.GetRequiredService<YamlAutomationSplitter>()

            });

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
