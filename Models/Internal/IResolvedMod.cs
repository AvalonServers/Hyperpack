using System.Threading.Tasks;

namespace Hyperpack.Models.Internal
{
    public interface IResolvedMod
    {
        string Provider { get; }

        Task DownloadAsync();
    }
}