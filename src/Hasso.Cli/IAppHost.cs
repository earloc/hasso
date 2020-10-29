using System.Threading.Tasks;

namespace Hasso.Cli
{
    internal interface IAppHost
    {
        Task WaitForShutdownAsync();
    }
}