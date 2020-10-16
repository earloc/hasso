
using Hasso.Cli;
using Hasso.Cli.Split;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddScriptsSplitter(this IServiceCollection that)
        {
            that.AddSingleton<IScriptSplitter, YamlScriptSplitter>();
            that.AddSingleton<IFragmentWriter, YamlFragmentWriter>();
            return that;
        }

    }
}
