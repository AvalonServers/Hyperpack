using Hyperpack.Models.Internal;
using Hyperpack.Models.Internal.Downloadable;

namespace Hyperpack.Models.Internal
{
    public interface IDownloadableMod : IResolvedMod, IHttpDownloadable {}
}