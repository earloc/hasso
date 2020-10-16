
using Hasso.Cli;
using Hasso.Cli.Split;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSplitters(this IServiceCollection that)
        {
            that.AddSingleton<IScriptSplitter, YamlScriptSplitter>();
            that.AddSingleton<ISceneSplitter, YamlSceneSplitter>();

            that.AddSingleton<IFragmentWriter, YamlFragmentWriter>();
            return that;
        }

    }
}
