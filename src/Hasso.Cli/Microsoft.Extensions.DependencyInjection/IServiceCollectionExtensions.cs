
using Hasso.Cli;
using Hasso.Cli.Split;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSplitters(this IServiceCollection that)
        {
            that.AddSingleton<IScriptSplitter, YamlScriptSplitter>();
            that.AddSingleton<ISceneSplitter, YamlSceneSplitter>();
            that.AddSingleton<IAutomationSplitter, YamlAutomationSplitter>();

            return that;
        }
        public static IServiceCollection AddFragmentWriter(this IServiceCollection that)
        {
            that.AddSingleton<IFragmentWriter, YamlFragmentWriter>();

            return that;
        }
    }
}
